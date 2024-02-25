using AuthGuad.Models;

namespace AuthGuad.Services
{
    public interface IcustomerService
    {
        Task<List<Customer>> Customer();
    }
}
