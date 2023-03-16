using System.ComponentModel.DataAnnotations;

namespace Car_App.Data.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [StringLength(35, ErrorMessage = "The maximum length for the title is 20 characters!")]

        public string Title { get; set; }

        [StringLength(15, ErrorMessage = "The maximum length for the make is 20 characters!")]
        public string Make { get; set; }

        [StringLength(20, ErrorMessage = "The maximum length for the model is 20 characters!")]
        public string Model { get; set; }

        [Range(1950, 2023, ErrorMessage = "The year must be between 1950 and 2023.")]
        public int Year { get; set; }

        [Range(0, 1000000, ErrorMessage = "The mileage should be between 0 and 1000000")]
        public int Distance { get; set; }

        [RegularExpression("^(diesel|gasoline|hybrid|electric)$", ErrorMessage = "Fuel type must be diesel, gasoline, hybrid, or electric.")]
        public string FuelType { get; set; }

        [Range(0, 1000, ErrorMessage = "The power should be between 0 and 1000 kW")]
        public double Power { get; set; }

        public virtual Owner Owner { get; set; }
        public Guid OwnerId { get; set; }
    }
}
