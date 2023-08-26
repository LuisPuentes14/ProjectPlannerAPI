using Entity;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace API.Models.RequestModels
{
    public class RequestProjectEdit
    {
        public int ProjectId { get; set; }

        public int? ProjectStatusId { get; set; }
       
        public string? ProjectTitle { get; set; }
      
        public int? CustomerId { get; set; }
       
        public int? ProjectDirectBossUserId { get; set; }
       
        public int? ProjectImmediateBossUserId { get; set; }
        public List<RequestResponsiblesProject> ResponsibleProject { get; set; } = new List<RequestResponsiblesProject> ();
       

    }

    public class RequestResponsiblesProject
    {
        public int UserId { get; set; }   

    }


}
