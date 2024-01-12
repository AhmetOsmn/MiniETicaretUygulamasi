namespace MiniETicaretAPI.Application.Dtos.Order
{
    public class SingleOrder
    {
        public string Id { get; set; } = null!;
        public string Address { get; set; }  = null!;
        public object BasketItems { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; } = null!;
        public string OrderCode { get; set; } = null!;  
        public bool Completed { get; set; }
    }
}
