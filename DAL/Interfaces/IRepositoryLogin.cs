using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryLogin
    {
        Task<User?> GetUserEmail(string in_email);
        Task<User?> GetUserEmailPassword(string in_email, string in_password);
        Task<List<int?>> GetUserProfile(User in_user);
        Task<List<string?>> GetProfile(List<int?> in_userProfilesId);
    }
}
