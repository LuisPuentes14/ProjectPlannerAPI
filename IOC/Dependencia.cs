using BLL.Implementacion;
using BLL.Interfaces;
using BLL.Utilities.Implementacion;
using BLL.Utilities.Interfaces;
using DAL.Implementacion;
using DAL.Interfaces;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace IOC
{
    public static class Dependencia    {


        public static void InyectarDependencia(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ProjectPlannerContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("PostgreSQLConnection"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            // Segregación de interfaces           
            services.AddScoped<IServiceAuthentication, ServiceAuthentication>(); 
            services.AddScoped<IEmail, Email>(); 
            services.AddScoped<IJWT, JWT>();
            services.AddScoped<IRepositoryLogin, RepositoryLogin>();
            services.AddScoped<IServiceProject, ServiceProject>();


        }

    }
}
