using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SnakeCase.JsonNet
{
    public class SnakeCaseJsonSerializer : JsonSerializer
    {
        public SnakeCaseJsonSerializer()
        {
            ContractResolver = new SnakeCaseContractResolver();
            Converters.Add(new StringEnumConverter
            {
                AllowIntegerValues = true,
                CamelCaseText = false
            });
        }
    }
}
