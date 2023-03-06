using System.ComponentModel.DataAnnotations;

namespace Car_App.Controllers.DTOModels
{
    public class AvtoDTO
    {
        [StringLength(20, ErrorMessage = "The maximum length for the title is 20 characters!")]
        public string Title { get; init; }
        public string Make { get; init; }
        public string Model { get; init; }
        public int Year { get; init; }
        public int Mileage { get; init; }
        public string FuelType { get; init; }
        public double Power { get; init; }
        public Guid OwnerId { get; set; }


    }
}
