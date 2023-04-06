using System.Runtime.Serialization;

namespace Car_App.Data.Models
{
    public enum CarSortBy
    {
        [EnumMember(Value = "Make ascending")]
        Make_Ascending,
        [EnumMember(Value = "Make descending")]
        Make_Descending,
        [EnumMember(Value = "Model ascending")]
        Model_Ascending,
        [EnumMember(Value = "Model descending")]
        Model_Descending,
        [EnumMember(Value = "Year ascending")]
        Year_Ascending,
        [EnumMember(Value = "Year descending")]
        Year_Descending,
        [EnumMember(Value = "Distance ascending")]
        Distance_Ascending,
        [EnumMember(Value = "Distance descending")]
        Distance_Descending,
        [EnumMember(Value = "Power ascending")]
        Power_Ascending,
        [EnumMember(Value = "Power descending")]
        Power_Descending,

    }




}