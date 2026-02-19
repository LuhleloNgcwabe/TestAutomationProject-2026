using Car_Unit_tests.Model;
using System.Diagnostics.CodeAnalysis;

namespace Car_Unit_tests
{
    [TestFixture]
    public class Tests
    {
        Car ToyataSUV;
        [SetUp]
        public void Setup()
        {
            ToyataSUV = new Car("Toyota","SUV",2016);
            TestContext.Out.WriteLine("Set up");
        }
        [Test]
        public void Verify_Car_details()
        {
            TestContext.Out.WriteLine("Get car details");
            Assert.That(ToyataSUV.GetCarInfo(), Does.EndWith("SUV").IgnoreCase);
        }
        
        [Test]
        public void New_Toyota_SUV_Is_Created()
        {
            Car ToyataSUV = new Car("Toyota", "SUV", 2016);
            Assert.That(ToyataSUV.Make, Is.EqualTo("Toyota"), "Make must be Toyota"); //That(ToyataSUV.Make, "")
            Assert.That(ToyataSUV.Model, Is.EqualTo("SUV"), "Model must be SUV");
            Assert.That(ToyataSUV.Year, Is.EqualTo(2016), "year of a car must be 2016");
            TestContext.Out.WriteLine(TestContext.CurrentContext.Test.FullName + ": Is complete");
        }

        [Test]
        public void StartEngine_switches_Engine_On()
        {
            ToyataSUV.StartEngine();
            Assert.That(ToyataSUV.IsEngineRunning, Is.True);
            TestContext.Out.WriteLine(TestContext.CurrentContext.Test.FullName + ": Is complete");
        }

        [Test]
        public void StopEngine_switches_Engine_off()
        {
            ToyataSUV.StopEngine();
            Assert.That(ToyataSUV.IsEngineRunning, Is.False);
            TestContext.Out.WriteLine(TestContext.CurrentContext.Test.FullName + ": Is complete");
        }

        [Test]
        public void Car_accelerate_By_5_Speed_increase_By_5()
        {
            ToyataSUV.Accelerate(5);
            Assert.That(ToyataSUV.GetSpeed(), Is.EqualTo(5), "Speed must increase by 5");
        }
        [Test,Order(0)]
        public void Brake_ReduceSpeedBy30_speedDropsBy30()
        {
            TestContext.Out.WriteLine("Start engine and increase speed by 40");
            ToyataSUV.StartEngine();
            ToyataSUV.Accelerate(40);
            TestContext.Out.WriteLine("Applied action, Reduce Speed");
            ToyataSUV.Brake(30);
            TestContext.Out.WriteLine($"Assert action, compare expected speed to actual speed.");
            Assert.That(ToyataSUV.Speed, Is.EqualTo(10));
        }

        [Test]
        public void Speed_OfaNotMovingCar_is_Zero() 
        {
            TestContext.Out.WriteLine("Assert: Speed of not a moving car is zero");
            Assert.That(ToyataSUV.GetSpeed(), Is.EqualTo(0));
        }

        [Test]
        public void CarHonk_makes_BeepBeepSound()
        {
            //Arrange
            TestContext.Out.WriteLine("Prepare a car for a hooty sound");
            ToyataSUV.StartEngine();
            TestContext.Out.WriteLine("make a car hooty");
            Assert.That(ToyataSUV.Honk(),Does.Contain("Beep").IgnoreCase);
            
        }

    }
}
