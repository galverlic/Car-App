using System.ComponentModel.DataAnnotations;

namespace Car_App.Data.Models
{
    public class Avto
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; }
        public int Power { get; set; }



    }
}
