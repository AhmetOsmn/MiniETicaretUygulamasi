using MiniETicaretAPI.Application.Enums;

namespace MiniETicaretAPI.Application.Dtos.Configuration
{
    public class Action
    {
        public string Code { get; set; }
        public string ActionType { get; set; }
        public string Definition { get; set; }
        public string HttpType { get; set; }
    }
}
