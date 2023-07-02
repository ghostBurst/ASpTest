#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFruits.Data;
using MyFruits.Models;
using MyFruits.Services;

namespace MyFruits.Areas.Fruits.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext ctx;
        private readonly ImageService imageService;

        public EditModel(ApplicationDbContext ctx, ImageService imageService)
        {
            this.ctx = ctx;
            this.imageService = imageService;
        }

        [BindProperty]
        public Fruit Fruit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fruit = await ctx.Fruits
                .Include(f => f.Image)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Fruit == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var fruitToUpdate = await ctx.Fruits
                .Include(f => f.Image)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (fruitToUpdate == null)
                return NotFound();

            var uploadedImage = Fruit.Image;

            if (null != uploadedImage)
            {
                uploadedImage = await imageService.UploadAsync(uploadedImage);

                if (fruitToUpdate.Image != null)
                {
                    imageService.DeleteUploadedFile(fruitToUpdate.Image);
                    fruitToUpdate.Image.Name = uploadedImage.Name;
                    fruitToUpdate.Image.Path = uploadedImage.Path;
                }
                else
                    fruitToUpdate.Image = uploadedImage;
            }

            if (await TryUpdateModelAsync(fruitToUpdate, "fruit", f => f.Name, f => f.Description, f => f.Price))
            {
                await ctx.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool FruitExists(int id)
        {
            return ctx.Fruits.Any(e => e.Id == id);
        }
    }
}
