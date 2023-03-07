using System.ComponentModel.DataAnnotations;

namespace Car_App.Data.Models
{
    public class Owner
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Range(3, 20, ErrorMessage = "The maximum length for a first name is 20 characters!")]
        public string FirstName { get; set; }

        [Range(3, 20, ErrorMessage = "The maximum length for a last name is 20 characters!")]
        public string LastName { get; set; }

        [Range(13, 13, ErrorMessage = "There should be 13 digits!")]
        public string Emso { get; set; }
        public virtual List<Avto> Cars { get; set; }

        [Range(9, 9, ErrorMessage = "There should be 13 digits!")]
        public string TelephoneNumber { get; set; }

    }
}
