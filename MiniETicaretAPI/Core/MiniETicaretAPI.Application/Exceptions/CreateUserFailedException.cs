namespace MiniETicaretAPI.Application.Exceptions
{
    public class CreateUserFailedException : Exception
    {
        public CreateUserFailedException() : base("Kullanıcı oluşturulurken beklenmeyen bir hata ile karşılaşıldı!")
        {
        }

        public CreateUserFailedException(string? message) : base(message)
        {
        }

        public CreateUserFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
