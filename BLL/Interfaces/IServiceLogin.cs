using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IServiceLogin
    {
        User Login(User user, out List<string> Out_userProfile);
    }
}
