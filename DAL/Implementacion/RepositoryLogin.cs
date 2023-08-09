using DAL.Interfaces;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementacion
{
    public class RepositoryLogin: IRepositoryLogin
    {
        private readonly ProjectPlannerContext _dbContext;

        public RepositoryLogin(ProjectPlannerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserEmail(string in_email)
        {
            return await _dbContext.Users.Where(
                u => u.UserEmail == in_email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserEmailPassword(string in_email, string in_password )
        {
            return await _dbContext.Users.Where(
                u => u.UserEmail == in_email 
                && u.UserPassword == in_password).FirstOrDefaultAsync();
        }

        public async Task<List<int?>> GetUserProfile(User in_user) { 
            
            return await _dbContext.UserProfiles.Where(
                p => p.UserId == in_user.UserId).Select(p => p.ProfileId).ToListAsync();
        }

        public async Task<List<string?>> GetProfile(List<int?> in_userProfilesId)
        {
            return await _dbContext.Profiles.Where(
                p => in_userProfilesId.Contains(p.ProfileId)).Select(p => p.ProfileName).ToListAsync();
        }

    }
}
