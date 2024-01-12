using MiniETicaretAPI.Application.Enums;

namespace MiniETicaretAPI.Application.CustomAttributes
{
    public class AuthorizeDefinitionAttribute : Attribute
    {
        public string Menu { get; set; }
        public string Definition { get; set; }
        public ActionType Action { get; set; }
    }
}
