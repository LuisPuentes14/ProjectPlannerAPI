using BLL.Implementacion;
using BLL.Interfaces;
using DAL.Implementacion;
using DAL.Interfaces;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


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


            // Inyeccion de dependencias
            services.AddScoped<IServiceLogin, ServiceLogin>();
            
        }

    }
}
