namespace Car_App.Controllers.DTOModels;

public class OwnerDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Emso { get; set; }
    public DateTime Dob { get; set; }
    public List<Guid> CarIds { get; set; }
    public string TelephoneNumber { get; set; }
}
