namespace NunitPlusExtentReportPractise
{
    [TestFixture]
    public class BasicMathTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Add_TwoNumbers_ReturnsCorrectSum()
        {
            // Arrange
            int a = 2;
            int b = 3;
            int answer = 5;

            // Act
            int result = a + b;


            // Assert
            Assert.That(answer,Is.EqualTo( result));
        }
    }
}
