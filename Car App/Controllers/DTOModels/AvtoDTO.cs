namespace Car_App.Controllers.DTOModels
{
    public class AvtoDTO
    {
        public string Title { get; init; }
        public string Make { get; init; }
        public string Model { get; init; }
        public int Year { get; init; }
        public int Mileage { get; init; }
        public string FuelType { get; init; }
        public int Power { get; init; }
        public Guid OwnerId { get; set; }


    }
}
