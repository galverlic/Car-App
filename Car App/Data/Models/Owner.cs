using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

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

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [StringLength(13, MinimumLength = 13, ErrorMessage = "There should be 13 digits!")]
        public string Emso { get; set; }

        public virtual List<Car> Cars { get; set; } = new List<Car>();

        [StringLength(9, MinimumLength = 9, ErrorMessage = "There should be 9 digits!")]
        public string TelephoneNumber { get; set; }

        public Owner() { }

        public void SetPassword(string password)
        {
            using var hmac = new HMACSHA512();

            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPassword(string password)
        {
            using var hmac = new HMACSHA512(PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != PasswordHash[i]) return false;
            }

            return true;
        }
    }
}
