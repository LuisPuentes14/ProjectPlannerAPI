using Entity;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace API.Models.RequestModels
{
    public class RequestProjectDelete
    {
        [Required]
        public int ProjectId { get; set; }  

    }

   


}
