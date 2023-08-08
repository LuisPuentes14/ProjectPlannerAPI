using System.ComponentModel.DataAnnotations;

namespace API.Models.RequestModels
{
    public class RequestResetPassword
    {
        [Required]
        public string UserPassword { get; set; } = null!;

    }
}
