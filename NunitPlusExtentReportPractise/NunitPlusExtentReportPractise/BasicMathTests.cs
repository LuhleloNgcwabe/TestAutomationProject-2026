namespace NunitPlusExtentReportPractise
{
    [TestFixture]
    public class BasicMathTests
    {
        int a, b, answer;
        [SetUp]
        public void Setup()
        {
            // Arrange
            a = 2;
            b = 3;
            answer = 5;
            TestContext.Out.WriteLine("Test set up is finished");
        }

        [Test]
        public void Add_TwoNumbers_ReturnsCorrectSum()
        {
            // Act
            TestContext.Out.WriteLine($"Add {a} and {b}");
            int result = a + b;
            // Assert
            TestContext.Out.WriteLine($"Verify that answer is {answer}!!");
            Assert.That(answer,Is.EqualTo( result));
        }

        [TearDown]
        public void TearDown()
        {
            a = b = answer = 0;
            TestContext.Out.WriteLine("Test is completed");
        }
    }
}
