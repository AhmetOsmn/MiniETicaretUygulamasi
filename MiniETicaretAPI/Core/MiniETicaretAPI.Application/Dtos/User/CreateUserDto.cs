namespace MiniETicaretAPI.Application.Dtos.User
{
    public class CreateUserDto
    {
        public string NameSurname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordRetry { get; set; }
    }
}
