using AuthGuad.Data;
using AuthGuad.Migrations;
using AuthGuad.Services;
using Microsoft.EntityFrameworkCore;
using AuthGuad.Models;
namespace AuthGuad.Contain
{
    public class CustomerService : IcustomerService
    {
        private readonly ApplicaitonDbContext dbContext;

        public CustomerService(ApplicaitonDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Models.Customer>> Customer()
        {
           return await this.dbContext.customers.ToListAsync();
        }
    }
}


