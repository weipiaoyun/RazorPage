using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPage1.Data;
using RazorPage1.Movie;

namespace RazorPage1
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPage1.Data.RazorPageContext _context;

        public DetailsModel(RazorPage1.Data.RazorPageContext context)
        {
            _context = context;
        }

        public movie movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            movie = await _context.movie.FirstOrDefaultAsync(m => m.ID == id);

            if (movie == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
