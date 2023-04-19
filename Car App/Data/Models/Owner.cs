using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Car_App.Data.Models
{
    public class Owner
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(20, MinimumLength = 3, ErrorMessage = "The first name should be between 3 and 20 characters!")]
        public string FirstName { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "The last name should be between 3 and 20 characters!")]
        public string LastName { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "The username should be between 3 and 20 characters!")]
        public string UserName { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [StringLength(13, MinimumLength = 13, ErrorMessage = "There should be 13 digits!")]
        public string Emso { get; set; }
        [JsonIgnore]
        public virtual List<Car> Cars { get; set; } = new List<Car>();

        [StringLength(9, MinimumLength = 9, ErrorMessage = "There should be 9 digits!")]
        public string TelephoneNumber { get; set; }

        public Owner() { }


    }
}
