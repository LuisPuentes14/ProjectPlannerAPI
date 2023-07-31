using Entity;
using BLL.Interfaces;

namespace BLL.Implementacion
{
    public class ServiceLogin : IServiceLogin
    {

        private readonly ProjectPlannerContext _dbContext;

        public ServiceLogin(ProjectPlannerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Login(User in_user, out List<string> Out_userProfile)
        {
            User user = new User();

            user = _dbContext.Users.Where(u => u.UserEmail == in_user.UserEmail).FirstOrDefault();


            if (user == null)
            {
                throw new Exception("Usuario no econtrado.");
            }

            user = _dbContext.Users.Where(u => u.UserEmail == in_user.UserEmail && u.UserPassword == in_user.UserPassword).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("Contraseña incorrecta.");
            }

            List<int?> userProfileIds = _dbContext.UserProfiles.Where(p => p.UserId == user.UserId).Select(p => p.ProfileId).ToList();

            if (userProfileIds.Count == 0)
            {
                throw new Exception("Perfiles no encontrados para este usuario.");
            }

            List<string> profiles = _dbContext.Profiles.Where(p => userProfileIds.Contains(p.ProfileId)).Select(p => p.ProfileName).ToList();

            Out_userProfile = profiles;

            return user;

        }




    }
}
