using AuthGuad.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthGuad.Data
{
    public class ApplicaitonDbContext:DbContext
    {
        public ApplicaitonDbContext(DbContextOptions<ApplicaitonDbContext> options) : base(options)
        {
        }

        public DbSet<User> users { get; set; }




    }
}
