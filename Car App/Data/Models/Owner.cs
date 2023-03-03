using System.ComponentModel.DataAnnotations;

namespace Car_App.Data.Models
{
    public class Owner
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Emso { get; set; }
        public DateTime Dob { get; set; }
        public virtual List<Avto> Cars { get; set; }
        public string TelephoneNumber { get; set; }

    }
}
