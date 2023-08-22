
using API.Models.RequestModels;
using API.Models.ResponseModels;
using AutoMapper;
using Entity;
using System.Globalization;
using System.Net.Sockets;

namespace Api.Utilidades._AutoMapper
{
    public class AutoMapperProfile : AutoMapper.Profile
    {

        public AutoMapperProfile()
        {

            #region project
            //Objeto origen /Objeto Destino  
            CreateMap<Project, ResponseProject>().ForMember(
                output => output.ProjectStatus,
                input => input.MapFrom(origin => origin.ProjectStatus.ProjectStatusDescripcion)
                ).ForMember(
                output => output.ProjectCustomer,
                input => input.MapFrom(origin => origin.Customer.CustomerName)
                ).ForMember(
                output => output.ProjectDirectBoss,
                input => input.MapFrom(origin => origin.ProjectDirectBossUser.UserName)
                ).ForMember(
                output => output.ProjectImmediateBoss,
                input => input.MapFrom(origin => origin.ProjectImmediateBossUser.UserName)
                ).ForMember(
                output => output.ResponsibleProject,
                input => input.MapFrom(origin => origin.ResponsiblesUsersProjects.Select(pu => new
                {
                    UserId = pu.User.UserId,
                    UserName = pu.User.UserName
                }).ToList())
                );

            //Objeto origen /Objeto Destino  
            CreateMap<RequestProject, Project>()
           .ForMember(
                output => output.ResponsiblesUsersProjects,
                input => input.MapFrom(origin => origin.ResponsibleProject.Select(pu => new ResponsiblesUsersProject
                {
                    ProjectId = origin.ProjectId, // Usamos el ProjectId de RequestProject
                    UserId = pu.UserId
                }))
                );

            #endregion

            #region ResetPassword
            //Objeto origen /Objeto Destino  
            CreateMap<RequestSendEmailResetPassword, User>();
            //CreateMap<Usuario, VMUsuarioLogin>();

            #endregion

            #region Login
            //Objeto origen /Objeto Destino  
            CreateMap<RequestLogin, User>();
            //CreateMap<Usuario, VMUsuarioLogin>();

            #endregion




        }
    }

}
