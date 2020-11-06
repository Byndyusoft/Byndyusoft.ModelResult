namespace ModelResult.UnitTests.TextJson.Converters
{
    using System.Text.Json;
    using AutoFixture;
    using Byndyusoft.ModelResult;
    using Byndyusoft.ModelResult.Extensions;
    using NUnit.Framework;

    [TestFixture]
    public class ErrorInfoItemConverterTests
    {
        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();

            _jsonSerializerOptions = new JsonSerializerOptions();
            _jsonSerializerOptions.AddErrorInfoConverter();
        }

        private Fixture _fixture;
        private JsonSerializerOptions _jsonSerializerOptions;

        [Test]
        public void TestNull()
        {
            var serialized = JsonSerializer.Serialize((ErrorInfoItem)null, _jsonSerializerOptions);

            Assert.That(serialized, Is.Not.Null);

            var result = JsonSerializer.Deserialize<ErrorInfoItem>(serialized, _jsonSerializerOptions);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Test()
        {
            var errorInfoItem = _fixture.Create<ErrorInfoItem>();

            var serialized = JsonSerializer.Serialize(errorInfoItem, _jsonSerializerOptions);

            Assert.That(serialized, Is.Not.Null);

            var result = JsonSerializer.Deserialize<ErrorInfoItem>(serialized, _jsonSerializerOptions);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PropertyName, Is.EqualTo(errorInfoItem.PropertyName));
            Assert.That(result.Error, Is.EqualTo(errorInfoItem.Error));
        }
    }
}