using System.Runtime.Serialization;

namespace Car_App.Data.Models.Sorting
{
    public enum CarSortBy
    {
        [EnumMember(Value = "Make")]
        Make,

        [EnumMember(Value = "Model")]
        Model,

        [EnumMember(Value = "Year")]
        Year,

        [EnumMember(Value = "Distance")]
        Distance,

        [EnumMember(Value = "Power")]
        Power

    }
}




