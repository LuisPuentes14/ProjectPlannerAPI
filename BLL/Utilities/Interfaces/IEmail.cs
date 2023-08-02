using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utilities.Interfaces
{
    public interface IEmail
    {
        void sendEmail(string in_email, string in_affair, string in_message);

    }
}
