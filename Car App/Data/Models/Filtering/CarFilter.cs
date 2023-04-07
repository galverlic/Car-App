namespace Car_App.Data.Models.Filtering
{
    public class CarFilter
    {
        public Guid? Id { get; set; }
        public string? Make { get; set; }
        public int? Year { get; set; }
        public double? Distance { get; set; }
        public string? FuelType { get; set; }
        public double? Power { get; set; }
    }
}
