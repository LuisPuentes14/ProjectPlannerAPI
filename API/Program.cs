using Api.Utilidades._AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IOC;
using BLL.ModelsAppsettings;
using Microsoft.Extensions.Configuration;
using BLL.Utilities.Implementacion;
using Serilog;
using Serilog.Events;
using API.Controllers;

var builder = WebApplication.CreateBuilder(args);

string MiCors = "MiCors";

// Add services to the container.

builder.Services.AddControllers();

//Configuracion de log

Log.Logger = new LoggerConfiguration()
           // .MinimumLevel.Debug() // Establecer el nivel mínimo de registro (puedes ajustarlo según tus necesidades)          
            .MinimumLevel.Error() // Establecer el nivel mínimo de registro (puedes ajustarlo según tus necesidades)          
            // .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Ajustar niveles específicos
            //.MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning) // Ajustar niveles específicos
            .MinimumLevel.Override("API.Controllers", LogEventLevel.Information) // Ajustar niveles específicos
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day) // Aquí especificamos la ruta y el nombre del archivo de registro
            .CreateLogger();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog(dispose: true); // Agregar Serilog como el proveedor de registro
});




//Se asigna parametros del appsettings.json en la clases de la
//paca negocio para obtener los parametros
//-------------------------------------------------------------------

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<BLL.ModelsAppsettings.Login>(builder.Configuration.GetSection("Login"));
builder.Services.Configure<ResetPassword>(builder.Configuration.GetSection("ResetPassword"));
builder.Services.Configure<SMTP>(builder.Configuration.GetSection("SMTP"));



//se configura autenticacion por JWT
//-------------------------------------------------------------------
var Issuer = builder.Configuration["AppSettings:Issuer"];
var Audience = builder.Configuration["AppSettings:Audience"];
var SecretKey = Encoding.ASCII.GetBytes(builder.Configuration["AppSettings:Secret"]);

builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(d =>
{

    //d.RequireHttpsMetadata = false;
    //d.SaveToken = false;
    d.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
        ValidateIssuer = true,
        ValidIssuer = Issuer,
        ValidateAudience = true,
        ValidAudience = Audience,
        // ValidateLifetime = true,
    };
});


builder.Services.InyectarDependencia(builder.Configuration);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//implementacion de cors
builder.Services.AddCors(options =>
{

    options.AddPolicy(
        name: MiCors,
        builder =>
        {
            builder.WithHeaders("*");
            builder.WithOrigins("*");
            builder.WithMethods("*");
        });

});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MiCors);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
