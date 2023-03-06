using System.ComponentModel.DataAnnotations;

namespace Car_App.Data.Models
{
    public class Avto
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [StringLength(20, ErrorMessage = "The maximum length for the title is 20 characters!")]
        public string Title { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; }
        public double Power { get; set; }

        public virtual Owner Owner { get; set; }
        public Guid OwnerId { get; set; }
    }
}
