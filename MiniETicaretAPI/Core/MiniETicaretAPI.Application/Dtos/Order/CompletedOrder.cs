namespace MiniETicaretAPI.Application.Dtos.Order
{
    public class CompletedOrder
    {        
        public DateTime OrderDate { get; set; }
        public string OrderCode { get; set; } = null!;
        public string NameSurname { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}

