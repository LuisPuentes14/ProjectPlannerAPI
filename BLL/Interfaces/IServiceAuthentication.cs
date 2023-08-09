using Entity;

namespace BLL.Interfaces
{
    public interface IServiceAuthentication
    {
        Task<string> Login(User user);
        Task<bool> SendEmailResetPassword(User in_user);
        Task<bool> ResetPassword(string in_email, string in_password);
    }
}
