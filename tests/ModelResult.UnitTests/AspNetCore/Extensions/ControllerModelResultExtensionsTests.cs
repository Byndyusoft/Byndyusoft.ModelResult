namespace Byndyusoft.ModelResult.AspNetCore.Extensions
{
    using AutoFixture;
    using Common;
    using Microsoft.AspNetCore.Mvc;
    using ModelResults;
    using NUnit.Framework;

    [TestFixture]
    public class ControllerModelResultExtensionsTests
    {
        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        private Fixture _fixture = default!;

        [Test]
        public void TestOk()
        {
            var value = _fixture.Create<long>();
            var modelResult = GetOkResult(value);
            var actionResult = modelResult.ToActionResult();
            Assert.That(actionResult.Value, Is.EqualTo(value));
        }

        private ModelResult<long> GetOkResult(long value)
        {
            return value;
        }

        [Test]
        public void TestError()
        {
            var value = _fixture.Create<ErrorInfo>();
            var modelResult = GetErrorResult(value);
            var actionResult = modelResult.ToActionResult();
            var badRequestObjectResult = (BadRequestObjectResult) actionResult.Result;
            Assert.That(badRequestObjectResult.Value, Is.EqualTo(value));
        }

        private ModelResult<long> GetErrorResult(ErrorInfo errorInfo)
        {
            return new ErrorModelResult(errorInfo);
        }

        [Test]
        public void TestNotFound()
        {
            var modelResult = GetNotFoundResult();
            var actionResult = modelResult.ToActionResult();
            var notFoundResult = (NotFoundResult) actionResult.Result;
            Assert.That(notFoundResult, Is.Not.Null);
        }

        private ModelResult<long> GetNotFoundResult()
        {
            return CommonErrorResult.NotFound;
        }
    }
}