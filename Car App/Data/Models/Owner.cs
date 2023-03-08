using System.ComponentModel.DataAnnotations;

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

        [StringLength(13, MinimumLength = 13, ErrorMessage = "There should be 13 digits!")]
        public string Emso { get; set; }

        public virtual List<Avto> Cars { get; set; } = new List<Avto>();

        [StringLength(9, MinimumLength = 9, ErrorMessage = "There should be 9 digits!")]
        public string TelephoneNumber { get; set; }

        public Owner() { }

        public Owner(string firstName, string lastName, string emso, string telephoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Emso = emso;
            TelephoneNumber = telephoneNumber;
        }
    }
}
