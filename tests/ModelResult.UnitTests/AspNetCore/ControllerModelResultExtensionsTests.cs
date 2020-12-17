namespace Byndyusoft.ModelResult.AspNetCore
{
    using Extensions;
    using ModelResults;
    using NUnit.Framework;

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