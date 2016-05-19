using Newtonsoft.Json;
using NUnit.Framework;

namespace SnakeCase.JsonNet.Tests
{
    [TestFixture]
    public class SnakeCaseContractResolverTests
    {
        private SnakeCaseContractResolver _contractResolver;

        [SetUp]
        public void SetUp()
        {
            _contractResolver = new SnakeCaseContractResolver();
        }

        [TearDown]
        public void TearDown()
        {
            _contractResolver = null;
        }

        [Test]
        public void SerializeObject_With_SnakeCaseContractResolver_Should_Convert_Property_Names_To_Snake_Case()
        {
            var obj = new TestObject { Title = "Mr", FirstName = "John", LastName = "Smith" };

            var result = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = _contractResolver
            });

            Assert.That(result.Contains("title"));
            Assert.That(result.Contains("first_name"));
            Assert.That(result.Contains("last_name"));
        }

        class TestObject
        {
            public string Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}
