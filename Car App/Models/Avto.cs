using System.ComponentModel.DataAnnotations;

namespace Car_App.Models
{
    public record Avto
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Title { get; init; }
        public string Make { get; init; }
        public string Model { get; init; }
        public int Year { get; init; }
        public int Mileage { get; init; }
        public string FuelType { get; init; }
        public int Power { get; init; }



  }
}
