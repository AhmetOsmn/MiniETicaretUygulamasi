namespace MiniETicaretAPI.Application.Dtos.Configuration
{
    public class Menu
    {
        public string Name { get; set; }
        public List<Action> Actions { get; set; } = new();
    }
}
