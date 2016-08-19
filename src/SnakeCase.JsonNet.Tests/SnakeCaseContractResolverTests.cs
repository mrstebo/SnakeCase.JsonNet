using System.Collections.Generic;
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
        public void Should_Convert_Property_Names_To_Snake_Case()
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

        [Test]
        public void Should_Handle_Special_Symbols()
        {
            var obj = new Dictionary<string, string>
            {
                {"test[user]", "Test"},
                {"test-user", "Test"},
                {"Test[Password]", "Test"},
                {"Test-Password", "Test"}
            };

            var result = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = _contractResolver
            });

            Assert.That(result.Contains("test[user]"));
            Assert.That(result.Contains("test_user"));
            Assert.That(result.Contains("test[password]"));
            Assert.That(result.Contains("test_password"));
        }

        [Test]
        public void Should_Handle_Consecutive_Uppercase_Characters()
        {
            var obj = new Dictionary<string, string>
            {
                {"TESTPrefix", "Test"},
                {"TestSUFFIX", "Test"}
            };

            var result = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = _contractResolver
            });

            Assert.That(result.Contains("test_prefix"));
            Assert.That(result.Contains("test_suffix"));
        }

        [Test]
        public void Should_Handle_Digits()
        {
            var obj = new Dictionary<string, string>
            {
                {"Test123", "Test"},
                {"123Test", "Test"}
            };

            var result = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = _contractResolver
            });

            Assert.That(result.Contains("test123"));
            Assert.That(result.Contains("123_test"));
        }

        private class TestObject
        {
            public string Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}
