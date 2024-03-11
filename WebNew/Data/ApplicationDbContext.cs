using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WebNew.Models;

namespace WebNew.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Rolles> Roles { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;
    }
}
