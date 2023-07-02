#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFruits.Data;
using MyFruits.Models;

namespace MyFruits.Areas.Fruits.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext ctx;

        public IndexModel(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }

        public IList<Fruit> Fruit { get;set; }

        public async Task OnGetAsync()
        {
            Fruit = await ctx.Fruits.Include(f => f.Image).ToListAsync();
        }
    }
}
