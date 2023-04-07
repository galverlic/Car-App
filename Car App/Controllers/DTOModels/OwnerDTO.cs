using System.ComponentModel.DataAnnotations;

namespace Car_App.Controllers.DTOModels;

public class OwnerDto
{

    [StringLength(20, MinimumLength = 3, ErrorMessage = "The first name should be between 3 and 20 characters!")]
    public string FirstName { get; set; }

    [StringLength(20, MinimumLength = 3, ErrorMessage = "The last name should be between 3 and 20 characters!")]
    public string LastName { get; set; }

    [StringLength(13, MinimumLength = 13, ErrorMessage = "There should be 13 digits!")]
    public string Emso { get; set; }

    public List<Guid> CarIds { get; set; }

    [StringLength(9, MinimumLength = 9, ErrorMessage = "There should be 9 digits!")]
    public string TelephoneNumber { get; set; }
}
