using System.ComponentModel.DataAnnotations;

namespace Car_App.Controllers.DTOModels
{
    public class AuthenticateRequestDto
    {
        [Required]

        public string Username { get; set; }
        [Required]

        public string Password { get; set; }
    }
}
