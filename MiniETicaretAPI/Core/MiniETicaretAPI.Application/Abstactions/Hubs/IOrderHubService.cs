namespace MiniETicaretAPI.Application.Abstactions.Hubs
{
    public interface IOrderHubService
    {
        Task OrderAddedMessageAsync(string message);

    }
}
