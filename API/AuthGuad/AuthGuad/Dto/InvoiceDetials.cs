namespace AuthGuad.Dto
{
    public class InvoiceDetials
    {
        public string InvoiceNo { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal Total { get; set; }
    }
}
