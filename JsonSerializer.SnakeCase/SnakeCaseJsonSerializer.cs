using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace JsonSerializer.SnakeCase
{
    public class SnakeCaseJsonSerializer : Newtonsoft.Json.JsonSerializer
    {
        public override IContractResolver ContractResolver
        {
            get { return new SnakeCaseContractResolver(); }
        }
        
        public SnakeCaseJsonSerializer()
        {
            Converters.Add(new StringEnumConverter
            {
                AllowIntegerValues = true,
                CamelCaseText = false
            });
        }
    }
}
