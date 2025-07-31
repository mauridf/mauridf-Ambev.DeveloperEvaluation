namespace Ambev.DeveloperEvaluation.Application.Users.GetUser
{
    /// <summary>
    /// Request DTO para autenticar um usuário
    /// </summary>
    public class AuthenticateUserRequest
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
