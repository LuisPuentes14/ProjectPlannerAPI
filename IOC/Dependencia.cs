using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IOC
{
    public static class Dependencia    {


        public static void InyectarDependencia(this IServiceCollection services, IConfiguration Configuration)
        {
            //services.AddDbContext<PolarisServerContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("CadenaSQL"));
            //});

            //services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            //// polaris Service
            //services.AddScoped<ICPUService, CPUService>();
            //services.AddScoped<ICPURepositoy, CPURepository>();
            //services.AddScoped<IHardDiskRepository, HardDiskRepository>();
            //services.AddScoped<IHardDiskService, HardDiskService>();
            //services.AddScoped<IRamRepository, RamRepository>();
            //services.AddScoped<IRamService, RamService>();
            //services.AddScoped<IServerRepository, ServerRepository>();
            //services.AddScoped<IServerService, ServerService>();
            //services.AddScoped<IClienteService, ClienteService>();
            //services.AddScoped<ILogClienteService, LogClienteService>();
            //services.AddScoped<ITipoLogService, TipoLogService>();
            //services.AddScoped<IDataBaseService, DataBaseService>();
            //services.AddScoped<IIndexRepository, IndexRepository>();
            //services.AddScoped<IIndexService, IndexService>();
            //services.AddScoped<IFileDataBaseRepository, FilesDataBaseRepository>();
            //services.AddScoped<IFilesDataBaseService, FilesDataBaseService>();
            //services.AddScoped<ITablesRepository, TablesRepository>();
            //services.AddScoped<ITablesService, TablesService>();
            //services.AddScoped<IQueryRepository, QueryRepository>();
            //services.AddScoped<IQueryService, QueryService>();
            //services.AddScoped<IPerfilService, PerfilService>();
            //services.AddScoped<IUsuarioService, UsuarioService>();
            //services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            //services.AddScoped<IModulosWebService, ModulosWebService>();
            //services.AddScoped<ITipoModuloService, TipoModuloService>();
            //services.AddScoped<IModulosWebRepository, ModulosWebRepository>();
            //services.AddScoped<IPerfilRepository, PerfilRepository>();
            //services.AddScoped<IPermisosPerfilModuloService, PermisosPerfilModuloService>();
            //services.AddScoped<IMenuService, MenuService>();
        }

    }
}
