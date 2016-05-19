using Newtonsoft.Json.Converters;

namespace JsonSerializer.SnakeCase
{
    public class SnakeCaseJsonSerializer : Newtonsoft.Json.JsonSerializer
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
