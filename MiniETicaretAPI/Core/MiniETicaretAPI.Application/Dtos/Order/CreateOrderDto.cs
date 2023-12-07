namespace MiniETicaretAPI.Application.Dtos.Order
{
    public class CreateOrderDto
    {
        public string? BasketId { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
