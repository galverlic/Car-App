using System.ComponentModel.DataAnnotations;

namespace Car_App.Controllers.DTOModels
{
    public class CarDTO
    {
        [StringLength(35, ErrorMessage = "The maximum length for the title is 35 characters!")]
        public string Title { get; init; }

        [StringLength(15, ErrorMessage = "The maximum length for the make is 20 characters!")]
        public string Make { get; init; }

        [StringLength(20, ErrorMessage = "The maximum length for the model is 20 characters!")]
        public string Model { get; init; }

        [Range(1950, 2023, ErrorMessage = "The year must be between 1950 and 2023.")]

        public int Year { get; init; }

        [Range(0, 1000000, ErrorMessage = "The distance driven should be between 0 and 1000000")]
        public double Distance { get; init; }

        [RegularExpression("^(diesel|gasoline|hybrid|electric)$", ErrorMessage = "Fuel type must be diesel, gasoline, hybrid, or electric.")]
        public string FuelType { get; init; }

        [Range(0, 1000, ErrorMessage = "The power should be between 0 and 1000 kW")]
        public double Power { get; init; }

        public Guid OwnerId { get; set; }


    }
}
