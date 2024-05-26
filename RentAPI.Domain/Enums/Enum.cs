using System.ComponentModel;

namespace Rents.Domain.Enums
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
