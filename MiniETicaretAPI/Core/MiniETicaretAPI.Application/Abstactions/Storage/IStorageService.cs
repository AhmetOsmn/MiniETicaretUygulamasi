namespace MiniETicaretAPI.Application.Abstactions.Storage
{
    public interface IStorageService : IStorage
    {
        public string StorageName { get; }
    }
}
