using Car_App.Data.Models;

namespace Car_App.Controllers.DTOModels
{
    public class AuthenticateResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthenticateResponseDto(Owner owner, string token)
        {
            Id = owner.Id;
            FirstName = owner.FirstName;
            LastName = owner.LastName;
            Email = owner.Email;
            Token = token;
        }
    }
}
