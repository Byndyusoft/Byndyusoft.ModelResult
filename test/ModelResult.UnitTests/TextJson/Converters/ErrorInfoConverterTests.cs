namespace ModelResult.UnitTests.TextJson.Converters
{
    using System.Linq;
    using System.Text.Json;
    using AutoFixture;
    using Byndyusoft.ModelResult;
    using Byndyusoft.ModelResult.Extensions;
    using NUnit.Framework;

    [TestFixture]
    public class ErrorInfoConverterTests
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
            var serialized = JsonSerializer.Serialize((ErrorInfo)null, _jsonSerializerOptions);

            Assert.That(serialized, Is.Not.Null);

            var result = JsonSerializer.Deserialize<ErrorInfo>(serialized, _jsonSerializerOptions);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void TestWithoutItems()
        {
            var errorInfo = new ErrorInfo(_fixture.Create<string>(), _fixture.Create<string>());

            var serialized = JsonSerializer.Serialize(errorInfo, _jsonSerializerOptions);

            Assert.That(serialized, Is.Not.Null);

            var result = JsonSerializer.Deserialize<ErrorInfo>(serialized, _jsonSerializerOptions);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Message, Is.EqualTo(errorInfo.Message));
            Assert.That(result.Code, Is.EqualTo(errorInfo.Code));
            Assert.That(result.Items, Is.Not.Null);
            Assert.That(result.Items, Is.Empty);
        }

        [Test]
        public void TestItems()
        {
            var items = _fixture.CreateMany<ErrorInfoItem>().ToArray();
            var errorInfo = new ErrorInfo(_fixture.Create<string>(), _fixture.Create<string>(), items);

            var serialized = JsonSerializer.Serialize(errorInfo, _jsonSerializerOptions);

            Assert.That(serialized, Is.Not.Null);

            var result = JsonSerializer.Deserialize<ErrorInfo>(serialized, _jsonSerializerOptions);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Message, Is.EqualTo(errorInfo.Message));
            Assert.That(result.Code, Is.EqualTo(errorInfo.Code));
            Assert.That(result.Items, Is.Not.Null);
            Assert.That(result.Items.Length, Is.EqualTo(items.Length));
            foreach (var expectedItem in items)
            {
                var item = result.Items.SingleOrDefault(i => i.PropertyName == expectedItem.PropertyName);
                Assert.That(item, Is.Not.Null);
                Assert.That(item.Error, Is.EqualTo(expectedItem.Error));
            }
        }
    }
}