using System.ComponentModel.DataAnnotations;

namespace AuthGuad.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Code { get; set; }    
        public string Name { get; set; }    
        public string Email { get; set; }    
        public string Phone { get; set; }    
        public string Creditlimit { get; set; }    
        public string IsActive { get; set; }    
        public string TaxCode { get; set; }    
    }
}
