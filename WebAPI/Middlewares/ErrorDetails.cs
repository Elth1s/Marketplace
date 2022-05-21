using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebAPI.Middlewares
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public override string ToString()
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var json = JsonConvert.SerializeObject(this, serializerSettings);
            return json;
        }
    }
}