using System.ComponentModel;

namespace RentAPI.Enums
{
    public class Enum
    {
        public enum TypeBike
        {
            [Description("New")]
            New = 1,

            [Description("Used")]
            Used = 2
        }
    }
}
