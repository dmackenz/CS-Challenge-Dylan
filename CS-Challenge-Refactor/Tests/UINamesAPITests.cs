using NUnit.Framework;

namespace CS_Challenge_Refactor.Tests
{
    [TestFixture]
    class UINamesAPITests
    {
        [TestCase]
        public void getANameSuccessfully()
        {
            string name = UINamesAPI.getName();
            Assert.IsNotNull(name);
            Assert.IsTrue(name.Length > 0);
        }
    }
}
