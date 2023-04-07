using System.Runtime.Serialization;

namespace Car_App.Data.Models.Sorting
{
    public enum OwnerSortBy
    {
        [EnumMember(Value = "FirstName")]
        FirstName,

        [EnumMember(Value = "LastName")]
        LastName,

        [EnumMember(Value = "Emso")]
        Emso,

        [EnumMember(Value = "TelephoneNumber")]
        TelephoneNumber


    }
}
