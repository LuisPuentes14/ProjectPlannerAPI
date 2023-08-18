using BLL.Interfaces;
using DAL.Interfaces;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Implementacion
{
    public class ServiceProject : IServiceProject
    {
        private readonly IGenericRepository<Project> _IGenericRepository;

        public ServiceProject(IGenericRepository<Project> IGenericRepository) {
            _IGenericRepository= IGenericRepository;
        }

        public async Task<List<Project>> GetAll() {

            IQueryable<Project> iProyect = await _IGenericRepository.Consultar();         

            return await iProyect.Include(p => p.ProjectStatus).Include(p => p.ProjectsUsers).ThenInclude(pu => pu.User)
                .Include(p => p.Customer)
                .Include(p => p.ProjectDirectBossUser)
                .Include(p => p.ProjectImmediateBossUser).ToListAsync();
        }



    }
}
