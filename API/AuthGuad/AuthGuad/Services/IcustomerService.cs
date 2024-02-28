using AuthGuad.Dto;
using AuthGuad.Helper;
using AuthGuad.Models;

namespace AuthGuad.Services
{
    public interface IcustomerService
    {
        Task<List<CustomerDto>> GetCustomer();
        Task<CustomerDto> GetByCode(string code);
        Task<ApiResponse> Remove(string code);
        Task<ApiResponse> Create(CustomerDto data);
        Task<ApiResponse> Update(CustomerDto data,string code);
        Task<ProductImage> GetDbSingleImage(string code);

    }
}
