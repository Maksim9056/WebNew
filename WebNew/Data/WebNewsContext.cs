using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebNew.Models;

namespace WebNew.Data
{
    public class WebNewsContext : DbContext
    {
        public WebNewsContext (DbContextOptions<WebNewsContext> options)
            : base(options)
        {
        }

        public DbSet<WebNew.Models.Rolles> Rolles { get; set; } = default!;
        public DbSet<WebNew.Models.User> User { get; set; } = default!;
    }
}
