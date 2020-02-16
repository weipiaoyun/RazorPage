using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RazorPage1.Data;
using RazorPage1.Movie;

namespace RazorPage1
{
    public class CreateModel : PageModel
    {
        private readonly RazorPage1.Data.RazorPageContext _context;

        public CreateModel(RazorPage1.Data.RazorPageContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        private readonly ILogger _logger;
        //public CreateModel(ILogger<CreateModel> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult OnGet()
        {
            string Message = $"Create page visited at {DateTime.Now.ToLongTimeString()}";
            _logger.LogInformation("********Message displayed: {Message}*******", Message);
            return Page();
        }

        [BindProperty]
        public movie movie { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.movie.Add(movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
