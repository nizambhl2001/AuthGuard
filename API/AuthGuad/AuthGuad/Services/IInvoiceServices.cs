using AuthGuad.Dto;
using AuthGuad.Helper;

namespace AuthGuad.Services
{
    public interface IInvoiceServices
    {
        Task<List<InoviceHeader>> GetAllInoviceHeadersAsync();
        Task<InoviceHeader> GetInoviceHeadersByCodeAsync(string invoiceNo);
        Task<List<InvoiceDetials>> GetInoviceDetailsAsync();
        Task<InvoiceDetials> GetInoviceDetailsByCodeAsync(string invoiceNo);
        Task<ApiResponse> SaveAsync(InvoiceEnity invoiceEnity);
        Task<ApiResponse> RemoveAsync(string InvoiceNo);

    }
}
