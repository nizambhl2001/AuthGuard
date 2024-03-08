namespace AuthGuad.Dto
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string PCode { get; set; } = null!;
        public string? PName { get; set; }
        public decimal? Price { get; set; }
        public string? productImage { get; set; }
       
    }
}
