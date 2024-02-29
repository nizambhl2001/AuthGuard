namespace AuthGuad.Dto
{
    public class InvoiceEnity
    {
        public InoviceHeader inoviceHeader { get; set; }
        public List<InvoiceDetials> invoiceDetials { get; set; }
    }
}
