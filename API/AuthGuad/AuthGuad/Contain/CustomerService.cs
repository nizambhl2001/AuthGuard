using AuthGuad.Data;
using AuthGuad.Migrations;
using AuthGuad.Services;
using Microsoft.EntityFrameworkCore;
using AuthGuad.Models;
using AuthGuad.Dto;
using AutoMapper;
using AuthGuad.Helper;
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

        public async Task<List<CustomerDto>> GetCustomer()
        {
            List<CustomerDto> reponse = new List<CustomerDto>();   

            var data = await this.dbContext.customers.ToListAsync();
            if(data != null)
            {
                reponse = this.mapper.Map<List<Models.Customer>,List<CustomerDto>>(data);               
            }
            return reponse;
        } 
        
        public async Task<CustomerDto> GetByCode(string code)
        {
            CustomerDto reponse = new CustomerDto();

            //var data = await this.dbContext.customers.FindAsync(code);
            var data = await this.dbContext.customers.FirstOrDefaultAsync(c => c.Code == code);
            if (data != null)
            {
                reponse = this.mapper.Map<Models.Customer,CustomerDto>(data);               
            }
            return reponse;
        }

        public async Task<ApiResponse> Create(CustomerDto data)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                Models.Customer customer = this.mapper.Map<CustomerDto, Models.Customer>(data);
                await this.dbContext.customers.AddAsync(customer);
                await dbContext.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result = data.Code;

            }
            catch (Exception ex)
            {
                response.ResponseCode = 401;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse> Remove(string code)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var customer = await this.dbContext.customers.FirstOrDefaultAsync(c => c.Code == code);
                if (customer !=null)
                {
                    this.dbContext.customers.Remove(customer);
                    await dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = customer.Code;
                }
               
                response.ResponseCode = 201;
                response.Result = customer.Code;

            }
            catch (Exception ex)
            {
                response.ResponseCode = 401;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse> Update(CustomerDto data, string code)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                //var customer = await this.dbContext.customers.FindAsync(code);
                var customer = await this.dbContext.customers.FirstOrDefaultAsync(c => c.Code == code);
                if (customer != null)
                {
                    customer.Name = data.Name;
                    customer.Phone = data.Phone;
                    customer.Email = data.Email;
                    customer.IsActive = data.IsActive;
                    customer.Creditlimit = data.Creditlimit;
                    this.dbContext.customers.Remove(customer);
                    await dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = customer.Code;
                }

                response.ResponseCode = 201;
                response.Result = customer.Code;

            }
            catch (Exception ex)
            {
                response.ResponseCode = 401;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<ProductImage> GetDbSingleImage(string icode)
        {
            try
            {
                // Retrieve the image from the database based on the provided code
                var productImageEntity = await this.dbContext.products.FirstOrDefaultAsync(item => item.PCode == icode);

                if (productImageEntity != null)
                {
                   
                    var productImage = new ProductImage
                    {
                        Id = productImageEntity.Id,
                        PCode = productImageEntity.PCode,
                        PImage = productImageEntity.PImage 
                    };

                    return productImage;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine(ex.Message);
                throw; // rethrow the exception
            }
        }

    }
}


