namespace AuthGuad.Models
{
    public class SalesProduct
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal Total { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
