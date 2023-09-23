using MiniETicaretAPI.Domain.Entities.Common;

namespace MiniETicaretAPI.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
