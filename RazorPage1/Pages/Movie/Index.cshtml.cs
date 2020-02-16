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
    public class IndexModel : PageModel
    {
        private readonly RazorPage1.Data.RazorPageContext _context;

        public IndexModel(RazorPage1.Data.RazorPageContext context)
        {
            _context = context;
        }

        public IList<movie> movie { get;set; }

        public async Task OnGetAsync()
        {
            movie = await _context.movie.ToListAsync();
        }
    }
}
