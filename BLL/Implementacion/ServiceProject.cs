﻿using BLL.Interfaces;
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

            var pagina = 2; // Página que queremos mostrar
            var elementosPorPagina = 10; // Cantidad de elementos por página

            IQueryable<Project> iProyect = await _IGenericRepository.Consultar();         

            return await iProyect.Include(p => p.ProjectStatus).Include(p => p.ProjectsUsers).ThenInclude(pu => pu.User)
                .Include(p => p.Customer)
                .Include(p => p.ProjectDirectBossUser)
                .Include(p => p.ProjectImmediateBossUser)
                .OrderBy(p => p.ProjectId)
              //  .Skip((pagina - 1) * elementosPorPagina) // inicio de index
              //  .Take(elementosPorPagina) // cuantos resultados mostrar
                .ToListAsync(); 
        }



    }
}
