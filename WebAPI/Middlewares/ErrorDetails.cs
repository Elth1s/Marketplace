using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebAPI.Middlewares
{
    public class ErrorDetails
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public override string ToString()
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var json = JsonConvert.SerializeObject(this, serializerSettings);
            return json;
        }
    }
}