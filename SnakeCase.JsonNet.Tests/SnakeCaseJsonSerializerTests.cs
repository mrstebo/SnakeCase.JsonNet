﻿using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Diagnostics;

namespace SnakeCase.JsonNet.Tests
{
    [TestFixture]
    public class SnakeCaseJsonSerializerTests
    {
        private SnakeCaseJsonSerializer _serializer;

        [SetUp]
        public void SetUp()
        {
            _serializer = new SnakeCaseJsonSerializer();
        }

        [TearDown]
        public void TearDown()
        {
            _serializer = null;
        }

        [Test]
        public void Serialize_Should_Convert_Property_Names_To_Snake_Case()
        {
            var obj = new TestObject {Title = "Mr", FirstName = "John", LastName = "Smith"};
            
            var result = PerformSerialize(obj);

            Assert.That(result.Contains("title"));
            Assert.That(result.Contains("first_name"));
            Assert.That(result.Contains("last_name"));
        }

        [Test]
        public void Deserialize_Should_Conver_Property_Names_Back()
        {
            var obj = new TestObject { Title = "Mr", FirstName = "John", LastName = "Smith" };

            var result = PerformSerialize(obj);

            Assert.That(result.Contains("title"));
            Assert.That(result.Contains("first_name"));
            Assert.That(result.Contains("last_name"));

            // can we make a new test object from this?
            var jr = new JsonTextReader(new StringReader(result));
            var obj2 = _serializer.Deserialize<TestObject>(jr);

            Assert.AreEqual("Mr", obj2.Title);
            Assert.AreEqual("John", obj2.FirstName);
            Assert.AreEqual("Smith", obj2.LastName);
        }

        [Test]
        public void CanHandleAcronyms()
        {
            var obj = new
            {
                MyLLC = "Fun Corp"
            };

            var result = PerformSerialize(obj);
            Debug.WriteLine(result);

            Assert.That(result.Contains("my_llc"));
        }

        [Test]
        public void CanHandleNumber()
        {
            var obj = new
            {
                MyDog1 = "Fun Corp"
            };

            var result = PerformSerialize(obj);
            Debug.WriteLine(result);
            Assert.That(result.Contains("my_dog_1"));
        }

        [Test]
        public void CanHandleMultipleNumber()
        {
            var obj = new
            {
                MyDog131 = "Fun Corp"
            };

            var result = PerformSerialize(obj);
            Debug.WriteLine(result);
            Assert.That(result.Contains("my_dog_131"));
        }

        [Test]
        public void CanHandleNumbeAcroynmr()
        {
            var obj = new
            {
                MyLLC1 = "Fun Corp"
            };

            var result = PerformSerialize(obj);
            Debug.WriteLine(result);
            Assert.That(result.Contains("my_llc_1"));
        }


        [Test]
        public void CanHandleABunchOfWors()
        {
            var obj = new
            {
                HeLovesPotatoes = "Fun Corp"
            };

            var result = PerformSerialize(obj);
            Debug.WriteLine(result);
            Assert.That(result.Contains("he_loves_potatoes"));
        }

        [Test]
        public void CanHandleNumberInside()
        {
            var obj = new
            {
                My131Children = "Fun Corp"
            };

            var result = PerformSerialize(obj);
            Debug.WriteLine(result);
            Assert.That(result.Contains("my_131_children"));
        }

        [Test]
        public void CanHandleMultipleNumberAcronym()
        {
            var obj = new
            {
                MyLLC1231 = "Fun Corp"
            };

            var result = PerformSerialize(obj);
            Debug.WriteLine(result);
            Assert.That(result.Contains("my_llc_1231"));
        }

        private string PerformSerialize(object obj)
        {
            using (var sw = new StringWriter())
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    _serializer.Serialize(jw, obj);
                }
                return sw.ToString();
            }
        }

        class TestObject
        {
            public string Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}
