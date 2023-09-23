﻿using MiniETicaretAPI.Domain.Entities.Common;

namespace MiniETicaretAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        // Eger CustomerId'yi burada belirtmezsek, ef kendisi database tarafında otomatik olarak olusturacak.
        // Burada kendimiz olusturursak, bizim olusturdugumuz ile ilişkilendirecek.
        public Guid CustomerId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        public ICollection<Product> Products { get; set; }
        public Customer Customer { get; set; }
    }
}
