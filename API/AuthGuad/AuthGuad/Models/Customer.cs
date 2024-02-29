using System.ComponentModel.DataAnnotations;

namespace AuthGuad.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
        public string ModifyUser { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
