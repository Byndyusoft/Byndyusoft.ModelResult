namespace ModelResult.UnitTests.ModelResult.Dtos
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using AutoFixture;
    using Byndyusoft.ModelResult;
    using Byndyusoft.ModelResult.Dtos;
    using NUnit.Framework;

    [TestFixture]
    public class ErrorInfoDtoTests
    {
        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        [NotNull] private Fixture? _fixture;

        [Test]
        public void TestConstructorWithoutItems()
        {
            var code = _fixture.Create<string>();
            var message = _fixture.Create<string>();
            var errorInfo = new ErrorInfo(code, message);

            var errorInfoDto = new ErrorInfoDto(errorInfo);

            Assert.That(errorInfoDto.Code, Is.EqualTo(code));
            Assert.That(errorInfoDto.Message, Is.EqualTo(message));
            Assert.That(errorInfoDto.Items, Is.Null);
        }

        [Test]
        public void TestConstructorWithItems()
        {
            var code = _fixture.Create<string>();
            var message = _fixture.Create<string>();
            var items = _fixture.CreateMany<ErrorInfoItem>().ToArray();
            var errorInfo = new ErrorInfo(code, message, items);

            var errorInfoDto = new ErrorInfoDto(errorInfo);

            Assert.That(errorInfoDto.Code, Is.EqualTo(code));
            Assert.That(errorInfoDto.Message, Is.EqualTo(message));
            Assert.That(errorInfoDto.Items, Is.Not.Null);
            Assert.That(errorInfoDto.Items!.Length, Is.EqualTo(items.Length));
            foreach (var errorInfoItem in items)
            {
                var errorInfoItemDto = errorInfoDto.Items.SingleOrDefault(i => i.PropertyName == errorInfoItem.PropertyName);
                Assert.That(errorInfoItemDto, Is.Not.Null);
                Assert.That(errorInfoItemDto!.Error, Is.EqualTo(errorInfoItem.Error));
            }
        }

        [Test]
        public void TestToErrorInfo()
        {
            var errorInfoDto = _fixture.Create<ErrorInfoDto>();

            var errorInfo = errorInfoDto.ToErrorInfo();

            Assert.That(errorInfo.Code, Is.EqualTo(errorInfoDto.Code));
            Assert.That(errorInfo.Message, Is.EqualTo(errorInfoDto.Message));
            Assert.That(errorInfo.Items, Is.Not.Null);
            Assert.That(errorInfo.Items.Length, Is.EqualTo(errorInfoDto.Items!.Length));
            foreach (var errorInfoItemDto in errorInfoDto.Items)
            {
                var errorInfoItem = errorInfo.Items.SingleOrDefault(i => i.PropertyName == errorInfoItemDto.PropertyName);
                Assert.That(errorInfoItem, Is.Not.Null);
                Assert.That(errorInfoItem!.Error, Is.EqualTo(errorInfoItemDto.Error));
            }
        }
    }
}