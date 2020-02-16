using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPage1.Movie;

namespace RazorPage1.Data
{
    public class RazorPageContext : DbContext
    {
        public RazorPageContext (DbContextOptions<RazorPageContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPage1.Movie.movie> movie { get; set; }
    }
}
