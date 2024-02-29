namespace AuthGuad.Dto
{
    public class InoviceHeader
    {
      
        public string InvoiceNo { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public string Remarks { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal NetTotal { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
