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
        public async Task<InvoiceDetials> GetInoviceDetailsByCodeAsync(string invoiceNo)
        {
            var data = await this.dbContext.tblsalesProducts.FirstOrDefaultAsync(item => item.InvoiceNo == invoiceNo);
            if (data != null)
            {
                return this.mapper.Map<SalesProduct, InvoiceDetials>(data);
            }
            return new InvoiceDetials();
        }

        public async Task<List<InoviceHeader>> GetAllInoviceHeadersAsync()
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

        public async Task<ApiResponse> RemoveAsync(string InvoiceNo)
        {
            try
            {
                var data = await this.dbContext.tblsalesHeaders.FirstOrDefaultAsync(item => item.InvoiceNo == InvoiceNo);
                if (data != null)
                {
                    this.dbContext.tblsalesHeaders.Remove(data);
                }

                var _data = await this.dbContext.tblsalesProducts.Where(item => item.InvoiceNo == InvoiceNo).ToListAsync();
                if (_data != null && _data.Count > 0)
                {
                    this.dbContext.tblsalesProducts.RemoveRange(_data);
                }
                return new ApiResponse() { Result = "pass", kyValue = InvoiceNo };
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return new ApiResponse();

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
                                response.Result = "pass";
                                response.Result = Result;
                            }
                            else
                            {
                                await dbTransaction.RollbackAsync();
                                response.Result = "faill";
                                response.Result = string.Empty;
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
            try
            {
                SalesHeader _header = this.mapper.Map<InoviceHeader, SalesHeader>(inoviceHeader);
                var header = await this.dbContext.tblsalesHeaders.FirstOrDefaultAsync(item => item.InvoiceNo == inoviceHeader.InvoiceNo);
                if(header != null)
                {
                    header.CustomerId = inoviceHeader.CustomerId;
                    header.CustomerName = inoviceHeader.CustomerName;
                    header.DeliveryAddress = inoviceHeader.DeliveryAddress;
                    header.Total = inoviceHeader.Total;
                    header.Remarks = inoviceHeader.Remarks;
                    header.Tax = inoviceHeader.Tax;
                    header.NetTotal = inoviceHeader.NetTotal;
                    header.ModifyUser = inoviceHeader.CreateUser;
                    header.ModifyDate = DateTime.Now;

                    var _data = await this.dbContext.tblsalesProducts.Where(item => item.InvoiceNo == inoviceHeader.InvoiceNo).ToListAsync();
                    if(_data !=null && _data.Count > 0)
                    {
                        this.dbContext.tblsalesProducts.RemoveRange(_data);
                    }
                }
                else
                {
                    await this.dbContext.tblsalesHeaders.AddAsync(_header);
                }
            }
            catch
            {

            }
            return result;
        } 
       private async Task<bool> SaveDetial(InvoiceDetials invoiceDetials)
        {

            try
            {
                SalesProduct _detail = this.mapper.Map<InvoiceDetials, SalesProduct>(invoiceDetials);
                await this.dbContext.tblsalesProducts.AddAsync(_detail);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public async Task<List<InvoiceDetials>> GetInoviceDetailsAsync()
        {
            var data = await this.dbContext.tblsalesProducts.ToListAsync();
            if (data != null && data.Count > 0)
            {
                return this.mapper.Map<List<SalesProduct>, List<InvoiceDetials>>(data);
            }
            return new List<InvoiceDetials>();
        }
    }
}
