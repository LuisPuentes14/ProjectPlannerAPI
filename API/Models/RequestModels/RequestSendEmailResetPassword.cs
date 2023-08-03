using System.ComponentModel.DataAnnotations;

namespace API.Models.RequestModels
{
    public class RequestSendEmailResetPassword
    {
        [Required]
        public string UserEmail { get; set; } = string.Empty;
    }
}
