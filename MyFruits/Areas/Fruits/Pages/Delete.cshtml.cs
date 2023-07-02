#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFruits.Data;
using MyFruits.Models;
using MyFruits.Services;

namespace MyFruits.Areas.Fruits.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext ctx;
        private ImageService imageService;

        public DeleteModel(ApplicationDbContext ctx, ImageService imageService)
        {
            this.ctx = ctx;
            this.imageService = imageService;
        }

        [BindProperty]
        public Fruit Fruit { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? hasErrorMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fruit = await ctx.Fruits
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Fruit == null)
            {
                return NotFound();
            }

            if (hasErrorMessage.GetValueOrDefault())
            {
                ErrorMessage = $"Une erreur est survenue lors de la tentative de suppression de {Fruit.Name} ({Fruit.Id})";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fruitToDelete = await ctx.Fruits
                .Include(f => f.Image)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (fruitToDelete == null)
            {
                return NotFound();
            }

            try
            {
                imageService.DeleteUploadedFile(fruitToDelete.Image);
                ctx.Fruits.Remove(fruitToDelete);
                await ctx.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception)
            {
                return RedirectToAction("./Delete", new { id, hasErrorMessage = true });
            }
        }
    }
}
