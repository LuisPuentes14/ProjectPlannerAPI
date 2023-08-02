using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utilities.Interfaces
{
    public interface IJWT
    {
        string generateToken(User in_user, List<string> in_userProfiles, int in_timeLifeMinutes)
    }
}
