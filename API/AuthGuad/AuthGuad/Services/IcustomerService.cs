using AuthGuad.Dto;
using AuthGuad.Models;

namespace AuthGuad.Services
{
    public interface IcustomerService
    {
        Task<List<CustomerDto>> Customer();
    }
}
