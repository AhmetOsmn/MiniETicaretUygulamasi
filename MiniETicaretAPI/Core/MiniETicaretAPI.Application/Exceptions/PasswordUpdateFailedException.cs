namespace MiniETicaretAPI.Application.Exceptions
{
    public class PasswordUpdateFailedException : Exception
    {
        public PasswordUpdateFailedException() : base("Şifre güncellenirken bir hata oluştu.")
        {
        }

        public PasswordUpdateFailedException(string? message) : base(message)
        {
        }

        public PasswordUpdateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
