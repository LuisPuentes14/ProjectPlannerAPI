using Entity;
using BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Api.Utilities.Encrypt;
using BLL.Utilities;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BLL.Implementacion
{
    public class ServiceLogin : IServiceLogin
    {

        private readonly ProjectPlannerContext _dbContext;
        private readonly IConfiguration _configuration;

        public ServiceLogin(ProjectPlannerContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<string> Login(User in_user)
        {

            User user;

            user = await _dbContext.Users.Where(u => u.UserEmail == in_user.UserEmail).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Usuario no econtrado.");
            }

            in_user.UserPassword = Encrypt.GetSHA256(in_user.UserPassword);

            user = await  _dbContext.Users.Where(u => u.UserEmail == in_user.UserEmail && u.UserPassword == in_user.UserPassword).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Contraseña incorrecta.");
            }

            List<int?> userProfileIds = await _dbContext.UserProfiles.Where(p => p.UserId == user.UserId).Select(p => p.ProfileId).ToListAsync();

            if (userProfileIds.Count == 0)
            {
                throw new Exception("Perfiles no encontrados para este usuario.");
            }

            List<string> profiles = await _dbContext.Profiles.Where(p => userProfileIds.Contains(p.ProfileId)).Select(p => p.ProfileName).ToListAsync();          

            var Issuer = _configuration.GetSection("AppSettings:Issuer").Value; 
            var Audience = _configuration.GetSection("AppSettings:Audience").Value; 
            var  SecretKey = _configuration.GetSection("AppSettings:Secret").Value;
            var timeMinutes = _configuration.GetSection("Login:TokenDurationMinutes").Value;            

            return JWT.generateToken(user, profiles, SecretKey, Audience, Issuer, Convert.ToInt32(timeMinutes));

        }
    }
}
