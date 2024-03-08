using System.ComponentModel.DataAnnotations.Schema;

namespace AuthGuad.Models
{
    public class TblProduct
    {
        public int Id { get; set; } = 0;
        public string PCode { get; set; }
        public string PName { get; set; }
        public decimal Price { get; set; }
        public string PImage { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile formFile { get; set; }
    }
}
