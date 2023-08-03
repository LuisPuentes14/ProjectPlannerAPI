using Entity;
using BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BLL.ModelsAppsettings;
using Microsoft.Extensions.Options;
using BLL.Utilities.Implementacion;
using BLL.Utilities.Interfaces;

namespace BLL.Implementacion
{
    public class ServiceAuthentication : IServiceAuthentication
    {

        private readonly ProjectPlannerContext _dbContext;       
        private readonly AppSettings _appSettings;
        private readonly BLL.ModelsAppsettings.Login _login;
        private readonly ResetPassword _resetPassword;
        private readonly IEmail _IEmail;
        private readonly IJWT _IJWT;


        public ServiceAuthentication(
            ProjectPlannerContext dbContext,
            IConfiguration configuration,
            IEmail IEmail,
            IOptions<AppSettings> appSettings,
            IOptions<BLL.ModelsAppsettings.Login> login, 
            IOptions<ResetPassword> resetPassword, IJWT IJWT)
        {
            _dbContext = dbContext;           
            _appSettings = appSettings.Value;
            _login = login.Value;
            _resetPassword = resetPassword.Value;
            _IEmail = IEmail;
            _IJWT = IJWT;
        }

        public async Task<string> Login(User in_user)
        {

            User? user;

            user = await _dbContext.Users.Where(
                u => u.UserEmail == in_user.UserEmail).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Usuario no econtrado.");
            }

            in_user.UserPassword = Encrypt.GetSHA256(in_user.UserPassword);

            user = await  _dbContext.Users.Where(
                u => u.UserEmail == in_user.UserEmail 
                && u.UserPassword == in_user.UserPassword).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Contraseña incorrecta.");
            }

            List<int?> userProfileIds = await _dbContext.UserProfiles.Where(
                p => p.UserId == user.UserId).Select(p => p.ProfileId).ToListAsync();

            if (userProfileIds.Count == 0)
            {
                throw new Exception("Perfiles no encontrados para este usuario.");
            }

            List<string?> profiles = await _dbContext.Profiles.Where(
                p => userProfileIds.Contains(p.ProfileId)).Select(p => p.ProfileName).ToListAsync();
                 

            return _IJWT.generateToken(
                user,
                profiles,              
                _login.TokenDurationMinutes);

        }

        public async Task<bool> SendEmailResetPassword(User in_user)
        {
            User? user;

            user = await _dbContext.Users.Where(
                u => u.UserEmail == in_user.UserEmail).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Correo no valido.");
            }

            List<int?> userProfileIds = await _dbContext.UserProfiles.Where(
                p => p.UserId == user.UserId).Select(p => p.ProfileId).ToListAsync();

            if (userProfileIds.Count == 0)
            {
                throw new Exception("Perfiles no encontrados para este usuario.");
            }

            List<string?> profiles = await _dbContext.Profiles.Where(
                p => userProfileIds.Contains(p.ProfileId)).Select(p => p.ProfileName).ToListAsync();           


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


    }
}
