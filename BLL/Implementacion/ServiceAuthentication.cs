using Entity;
using BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BLL.ModelsAppsettings;
using Microsoft.Extensions.Options;
using BLL.Utilities.Implementacion;
using BLL.Utilities.Interfaces;
using DAL.Interfaces;
using DAL.Implementacion;

namespace BLL.Implementacion
{
    public class ServiceAuthentication : IServiceAuthentication
    {
    
       
        private readonly BLL.ModelsAppsettings.Login _login;
        private readonly ResetPassword _resetPassword;
        private readonly IEmail _IEmail;
        private readonly IJWT _IJWT;
        private readonly IRepositoryLogin _IRepositoryLogin;
        private readonly IGenericRepository<User> _IGenericRepository;



        public ServiceAuthentication(
            ProjectPlannerContext dbContext,           
            IEmail IEmail,
            IOptions<AppSettings> appSettings,
            IOptions<BLL.ModelsAppsettings.Login> login, 
            IOptions<ResetPassword> resetPassword,
        IJWT IJWT,
            IRepositoryLogin IRepositoryLogin, IGenericRepository<User> IGenericRepository)
        {  
            _login = login.Value;
            _resetPassword = resetPassword.Value;
            _IEmail = IEmail;
            _IJWT = IJWT;
            _IRepositoryLogin = IRepositoryLogin;
            _IGenericRepository = IGenericRepository;
        }

        public async Task<string> Login(User in_user)
        {

            User? user;

            user = await _IRepositoryLogin.GetUserEmail(in_user.UserEmail);

            if (user == null)
            {
                throw new Exception("Usuario no econtrado.");
            }

            in_user.UserPassword = Encrypt.GetSHA256(in_user.UserPassword);

            user = await _IRepositoryLogin.GetUserEmailPassword(in_user.UserEmail, in_user.UserPassword);

            if (user == null)
            {
                throw new Exception("Contraseña incorrecta.");
            }

            List<int?> userProfileIds = await _IRepositoryLogin.GetUserProfile(user);

            if (userProfileIds.Count == 0)
            {
                throw new Exception("Perfiles no encontrados para este usuario.");
            }

            List<string?> profiles = await _IRepositoryLogin.GetProfile(userProfileIds);


            return _IJWT.generateToken(
                user,
                profiles,              
                _login.TokenDurationMinutes);

        }

        public async Task<bool> SendEmailResetPassword(User in_user)
        {
            User? user;

            user = await _IRepositoryLogin.GetUserEmail(in_user.UserEmail);

            if (user == null)
            {
                throw new Exception("Correo no valido.");
            }

            List<int?> userProfileIds =await _IRepositoryLogin.GetUserProfile(user);

            if (userProfileIds.Count == 0)
            {
                throw new Exception("Perfiles no encontrados para este usuario.");
            }

            List<string?> profiles = await _IRepositoryLogin.GetProfile(userProfileIds);


            string affair = "Recuperar contraseña.";

            string token = _IJWT.generateToken(
                user,
                profiles,               
                _resetPassword.TokenDurationMinutes); 

            string message = $"<p>Hola: {user.UserName},Has solcitado Restablecer tu Password.</p>\r\n    " +
                "          <p>Sigue el siguiente enlace para generar un nuevo Password:\r\n           " +
                $"         <a href=\"http://localhost:4200/change-password?token={token}\" > Restablecer Password</a></p>\r\n\r\n  ";


            _IEmail.sendEmail(
                in_user.UserEmail, 
                affair,
                message);

            return true;

        }


        public async Task<bool> ResetPassword(string in_email, string in_password)
        {
           User user= await  _IGenericRepository.Obtener(u => u.UserEmail == in_email);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            user.UserPassword = Encrypt.GetSHA256(in_password);

            await _IGenericRepository.Editar(user);           

            return true;

        }

    }
}
