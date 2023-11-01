namespace MiniETicaretAPI.Domain.Entities
{
    public class ProductImageFile : File
    {
        public ICollection<Product> Products { get; set; }
        public bool Showcase { get; set; }
    }
}
