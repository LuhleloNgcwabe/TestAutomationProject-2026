namespace UserAccountService.Tests
{
    [TestFixture,Explicit("Runs tests that are integrated with extent report only")]
    public class UserAccountServiceTest
    {
        private UserService service;
        [SetUp]
        public void Setup()
        {
            service = new UserService();
            service.Register("admin", "admin123");
        }


        [Test]
        public void Register_ValidUser_Success()
        {
            service.Register("user1", "password");
            Assert.Pass();
        }

        [TestCase("admin", "admin123", true)]
        [TestCase("admin", "wrong", false)]
        public void LoginTests(string user, string pass, bool expected)
        {
            Assert.That(expected, Is.EqualTo(service.Login(user, pass)));
        }

        [Test]
        public void Login_UnregisteredUser_ThrowsException()
        {
            Assert.Throws<Exception>(() => service.Login("x", "y"));
        }


    }
}
