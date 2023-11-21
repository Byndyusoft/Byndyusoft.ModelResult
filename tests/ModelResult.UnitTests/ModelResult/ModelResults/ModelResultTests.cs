namespace Byndyusoft.ModelResult.ModelResult.ModelResults
{
    using AutoFixture;
    using Byndyusoft.ModelResult.ModelResults;
    using NUnit.Framework;

    [TestFixture]
    public class ModelResultTests
    {
        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        private Fixture _fixture = default!;

        [Test]
        public void TestIsOk()
        {
            var result = new OkModelResult();

            Assert.That(result.IsOk(), Is.True);
            Assert.That(result.IsError(), Is.False);
        }

        [Test]
        public void TestIsOkGeneral()
        {
            var result = new OkModelResult<int>(_fixture.Create<int>());

            Assert.That(result.IsOk(), Is.True);
            Assert.That(result.IsError(), Is.False);
        }

        [Test]
        public void TestIsError()
        {
            var result = new ErrorModelResult(_fixture.Create<string>(), _fixture.Create<string>());

            Assert.That(result.IsOk(), Is.False);
            Assert.That(result.IsError(), Is.True);
        }

        [Test]
        public void TestIsErrorGeneral()
        {
            var result = new ErrorModelResult(_fixture.Create<string>(), _fixture.Create<string>());
            var generalResult = (ErrorModelResult<int>) result;

            Assert.That(generalResult.IsOk(), Is.False);
            Assert.That(generalResult.IsError(), Is.True);
        }

        [Test]
        public void TestGetResultOk()
        {
            var resultValue = _fixture.Create<int>();
            var result = new OkModelResult<int>(resultValue);

            Assert.That(result.Result, Is.EqualTo(resultValue));
        }

        [Test]
        public void TestGetResultError()
        {
            var result = new ErrorModelResult(_fixture.Create<string>(), _fixture.Create<string>());
            var generalResult = (ErrorModelResult<int>) result;

            Assert.That(() => generalResult.Result, Throws.InvalidOperationException);
        }

        [Test]
        public void TryGetResult_ResultIsOk_ReturnsTrue_AndSetsResult()
        {
            var resultValue = _fixture.Create<int>();
            var result = (ModelResult<int>)new OkModelResult<int>(resultValue);

            var resultIsRetrieved = result.TryGetResult(out var actualResultValue);
            Assert.That(resultIsRetrieved, Is.True);
            Assert.That(actualResultValue, Is.EqualTo(resultValue));
        }

        [Test]
        public void TryGetResult_ResultIsError_ReturnsFalse()
        {
            var result = (ModelResult<int>)new ErrorModelResult(_fixture.Create<ErrorInfo>());

            var resultIsRetrieved = result.TryGetResult(out _);
            Assert.That(resultIsRetrieved, Is.False);
        }

        [Test]
        public void Ok_FirstAndSecondCall_AreEqualByReference()
        {
            var firstModelResult = ModelResult.Ok;
            var secondModelResult = ModelResult.Ok;

            Assert.That(ReferenceEquals(firstModelResult, secondModelResult), Is.True);
        }
    }
}