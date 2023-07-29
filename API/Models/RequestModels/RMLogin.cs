using System.ComponentModel.DataAnnotations;

namespace API.Models.RequestModels
{
    public class RMLogin
    {
        [Required]
        public string UserEmail { get; set; } = null!;

        [Required]
        public string UserPassword { get; set; } = null!; 
        


    }
}
