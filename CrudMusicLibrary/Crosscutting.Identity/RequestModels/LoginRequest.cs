using System.ComponentModel.DataAnnotations;

namespace Crosscutting.Identity.RequestModels
{
    public class LoginRequest
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
