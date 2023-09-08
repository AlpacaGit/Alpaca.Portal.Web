using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Alpaca.Portal.Web.Models;

namespace Alpaca.Portal.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Alpaca.Portal.Web.Models.Notice> Notice { get; set; } = default!;
    }
}