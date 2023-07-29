using DAL.Interfaces;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;

namespace BLL.Implementacion
{
    public class ServiceLogin: IServiceLogin
    {
        private readonly IGenericRepository<User> _repositorio;


        public ServiceLogin(IGenericRepository<User> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<User> Login(User in_user)
        {
            try
            {
                User user = await _repositorio.Obtener(u => u.UserEmail == in_user.UserEmail && u.UserPassword == in_user.UserPassword);
                return user;

            }
            catch (Exception ex)
            {
                var jj = ex.Message;
                throw;
            }
           
        }


    }
}
