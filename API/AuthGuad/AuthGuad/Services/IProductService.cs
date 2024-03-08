using AuthGuad.Dto;

namespace AuthGuad.Services
{
    public interface IProductService
    {
        Task<List<ProductEntity>> Getall();
        Task<ProductEntity> Getbycode(string code);
    }
}
