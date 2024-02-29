using AuthGuad.Dto;
using AuthGuad.Helper;

namespace AuthGuad.Services
{
    public interface IInvoiceServices
    {
        Task<List<InoviceHeader>> GetInoviceHeadersAsync();
        Task<InoviceHeader> GetInoviceHeadersByCodeAsync(string invoiceNo);
        Task<InvoiceDetials> GetInoviceDetailsByCodeAsync(string invoiceNo);
        Task<ApiResponse> SaveAsync(InvoiceEnity invoiceEnity);
        Task<ApiResponse> RemoveAsync();

    }
}
