using AuthGuad.Data;
using AuthGuad.Migrations;
using AuthGuad.Services;
using Microsoft.EntityFrameworkCore;
using AuthGuad.Models;
using AuthGuad.Dto;
using AutoMapper;
namespace AuthGuad.Contain
{
    public class CustomerService : IcustomerService
    {
        private readonly ApplicaitonDbContext dbContext;
        private readonly IMapper mapper;

        public CustomerService(ApplicaitonDbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<CustomerDto>> Customer()
        {
            List<CustomerDto> reponse = new List<CustomerDto>();   

            var data = await this.dbContext.customers.ToListAsync();
            if(data != null)
            {
                reponse = this.mapper.Map<List<Models.Customer>,List<CustomerDto>>(data);               
            }
            return reponse;
        }
    }
}


