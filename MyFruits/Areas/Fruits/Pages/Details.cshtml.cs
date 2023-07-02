#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFruits.Data;
using MyFruits.Models;

namespace MyFruits.Areas.Fruits.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly MyFruits.Data.ApplicationDbContext _context;

        public DetailsModel(MyFruits.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Fruit Fruit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fruit = await _context.Fruits.FirstOrDefaultAsync(m => m.Id == id);

            if (Fruit == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
