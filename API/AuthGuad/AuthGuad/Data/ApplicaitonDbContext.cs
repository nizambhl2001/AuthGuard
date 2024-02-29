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
        public DbSet<Customer> customers { get; set; }
        public DbSet<ProductImage> products { get; set; }
        public DbSet<TblProduct> tblproducts { get; set; }
        public DbSet<SalesHeader> tblsalesHeaders { get; set; }
        public DbSet<SalesProduct> tblsalesProducts { get; set; }

    }
}
