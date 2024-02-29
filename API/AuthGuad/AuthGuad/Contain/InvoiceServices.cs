using AuthGuad.Data;
using AuthGuad.Dto;
using AuthGuad.Helper;
using AuthGuad.Models;
using AuthGuad.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuthGuad.Contain
{
    public class InvoiceServices : IInvoiceServices
    {
        private readonly ApplicaitonDbContext dbContext;
        private readonly IMapper mapper;

        public InvoiceServices(ApplicaitonDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public Task<InvoiceDetials> GetInoviceDetailsByCodeAsync(string invoiceNo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InoviceHeader>> GetInoviceHeadersAsync()
        {
           
            var data = await this.dbContext.tblsalesHeaders.ToListAsync();
            if (data != null && data.Count > 0)
            {
                return  this.mapper.Map<List<SalesHeader>, List<InoviceHeader>>(data);
            }
            return new List<InoviceHeader>();
        }

        public async Task<InoviceHeader> GetInoviceHeadersByCodeAsync(string invoiceNo)
        {
            var data = await this.dbContext.tblsalesHeaders.FirstOrDefaultAsync(item=>item.InvoiceNo == invoiceNo);
            if (data != null)
            {
                return this.mapper.Map<SalesHeader,InoviceHeader>(data);
            }
            return new InoviceHeader();
        }

        public Task<ApiResponse> RemoveAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> SaveAsync(InvoiceEnity invoiceEnity)
        {
            string Result = string.Empty;
            int processcount = 0;
            ApiResponse response = new ApiResponse();
            if (invoiceEnity != null)
            {
                using(var dbTransaction =  await this.dbContext.Database.BeginTransactionAsync())
                {
                    if(invoiceEnity.inoviceHeader != null)
                    {
                        Result = await this.SaveHeader(invoiceEnity.inoviceHeader);
                        if (!string.IsNullOrEmpty(Result) && (invoiceEnity.invoiceDetials !=null && invoiceEnity.invoiceDetials.Count> 0))
                        {
                            invoiceEnity.invoiceDetials.ForEach(item =>
                            {
                                bool saveresult = this.SaveDetial(item).Result;
                                if (saveresult)
                                {
                                    processcount++;
                                }

                            });
                            if (invoiceEnity.invoiceDetials.Count == processcount)
                            {
                                 await this.dbContext.SaveChangesAsync();
                                await dbTransaction.CommitAsync();
                            }
                        }
                    }

                }
            }
            return response;
           
        }
        private async Task<string> SaveHeader(InoviceHeader inoviceHeader)
        {
            string result = string.Empty;
            return result;
        } 
       private async Task<bool> SaveDetial(InvoiceDetials invoiceDetials)
        {
            string result = string.Empty;
            return true;
        }
    }
}
