using Byndyusoft.ModelResult.Extensions;
using Byndyusoft.ModelResult.ModelResults;
using NUnit.Framework;

namespace ModelResult.UnitTests.AspNetCore
{
    public class ControllerModelResultExtensionsTests
    {
        [Test]
        public void ToActionResult_Generic_OkModel()
        {
            var modelResult = new OkModelResult<long>(1);
            var actionResult = modelResult.ToActionResult();
            Assert.That(actionResult.Value, Is.EqualTo(modelResult.Result));
        }
    }
}