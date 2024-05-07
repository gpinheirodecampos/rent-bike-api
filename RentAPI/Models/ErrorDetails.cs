using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace RentAPI.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public string Trace { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
