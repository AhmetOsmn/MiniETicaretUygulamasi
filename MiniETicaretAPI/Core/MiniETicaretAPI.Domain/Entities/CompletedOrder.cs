using MiniETicaretAPI.Domain.Entities.Common;

namespace MiniETicaretAPI.Domain.Entities
{
    public class CompletedOrder : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
