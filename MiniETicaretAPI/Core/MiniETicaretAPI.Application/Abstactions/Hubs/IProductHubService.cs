namespace MiniETicaretAPI.Application.Abstactions.Hubs
{
    public interface IProductHubService
    {
        Task ProductAddedMessageAsync(string message);
    }
}
