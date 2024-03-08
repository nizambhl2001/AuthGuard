using AuthGuad.Data;
using AuthGuad.Dto;
using AuthGuad.Models;
using AuthGuad.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuthGuad.Contain
{
    public class ProductService : IProductService
    {

        private readonly ApplicaitonDbContext container;
        private readonly IMapper mapper;
        public ProductService(ApplicaitonDbContext container, IMapper mapper)
        {

    
            this.container = container;
            this.mapper = mapper;
        }

        public async Task<List<ProductEntity>> Getall()
        {
            var customerdata = await this.container.tblproducts.ToListAsync();
            if (customerdata != null && customerdata.Count > 0)
            {
                // we need use automapper

                return this.mapper.Map<List<TblProduct>, List<ProductEntity>>(customerdata);
            }
            return new List<ProductEntity>();

        }

        public async Task<ProductEntity> Getbycode(string code)
        {
            var customerdata = await this.container.tblproducts.FirstOrDefaultAsync(item => item.PCode == code);
            if (customerdata != null)
            {
                var _proddata = this.mapper.Map<TblProduct, ProductEntity>(customerdata);
                return _proddata;
            }
            return new ProductEntity();

        }
    }
}
