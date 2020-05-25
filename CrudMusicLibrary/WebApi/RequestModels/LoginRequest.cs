using System.ComponentModel.DataAnnotations;

namespace WebApi.RequestModels
{
    public class LoginRequest
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
