using System;
using System.Collections.Generic;
using System.Text;
using Car_Unit_tests.Model;

namespace Car_Unit_tests
{
    [TestFixture,Category("Regression")]
    public class ParameterisedCarTests
    {
        [SetUp]
        public void SetUp()
        {
            TestContext.Out.WriteLine("Set up: Start parameterised tesst");
        }

        [TestCase("VW", "T-cross", 2019)]
        [TestCase("Toyata", "Yaris", 2016)]
        [TestCase("Handai","alentra",2013)]
        public void Verify_Car_Details(string make,string model, int year)
        {
            //Arrange and Action
            TestContext.Out.WriteLine("Arrange and Action");
            Car Car = new Car(make,model,year);

            TestContext.Out.WriteLine("Assert: Verify car details");
            Assert.That(Car, Is.Not.Null);
            Assert.That(Car.Model, Is.EqualTo(model));
            Assert.That(Car.Make, Is.EqualTo(make));
            Assert.That (Car.Year, Is.EqualTo(year));
        }

        [TestCase(5,ExpectedResult =5)]
        [TestCase(50, ExpectedResult = 50)]
        [TestCase(45, ExpectedResult = 45)]
        public double Verify_Accelaration(double increment)
        {
            TestContext.Out.WriteLine("Arrange:Create Car object");
            Car Car = new Car("Handai","Elentra",2012);
            TestContext.Out.WriteLine("Action: Accelarate car");
            Car.Accelerate(increment);
            TestContext.Out.WriteLine("Assert: Verify accelaration speed");
            //Assert.That(Car.GetSpeed(),Is.EqualTo(increment),$"Speed must be equal to {increment}");
            return Car.GetSpeed();

        }

        [TestCase(70,60,ExpectedResult =10)]
        [TestCase(120, 60, ExpectedResult = 60)]
        [TestCase(89, 90, ExpectedResult = 0)]
        public double verify_braking_reduce_speed(double currentSpeed,double decrement) {

            TestContext.Out.WriteLine("Arrang: Create a new car and put it in motion");
            Car Car = new Car("Toyota", "yaris", 2015);

            TestContext.Out.WriteLine("Action: make the car move and apply brake");
            Car.Accelerate(currentSpeed);
            Car.Brake(decrement);

            TestContext.Out.WriteLine("Assert: compare results");
            return Car.GetSpeed();
        }

        [Test]
        public void Verify_ToggleEngine_FromOffState()
        {
            //Arrange
            TestContext.Out.WriteLine("Arrange: Create a car with engine turned off");
            Car Car = new Car("Toyota", "Etios", 2015);
            TestContext.Out.WriteLine("Action: Turn the engine on");
            Car.ToggleEngine();
            TestContext.Out.WriteLine("Asset: Engine is turn on");
            Assert.That(Car.IsEngineRunning, Is.True,"Engine must be running, this must be true");
        }

        [Test]
        public void Verify_ToggleEngine_FromOnState()
        {
            //Arrange
            TestContext.Out.WriteLine("Arrange: Create a car with engine turned on");
            Car Car = new Car("Toyota", "Etios", 2015);
            Car.ToggleEngine();
            TestContext.Out.WriteLine("Action: Turn the engine off");
            Car.ToggleEngine();
            TestContext.Out.WriteLine("Asset: Engine is turn on");
            Assert.That(Car.IsEngineRunning, Is.False, "Engine must be off, this must be false");
        }

        [TestCase(50,7)]
        [TestCase(40, 7)]
        [TestCase(20, 80)]
        public void GetSpeed_returns_CorrectSpeed(double initialSpeed,double acceleration)
        {
            double totSpeed = initialSpeed + acceleration;
            TestContext.Out.WriteLine($"Arrange: Create a car with initial speed of {initialSpeed}");
            Car Car = new Car("Toyata", "Etios", 2017);
            Car.Speed =initialSpeed;
            TestContext.Out.WriteLine("Action: apply Acceleration");
            Car.Accelerate(acceleration);
            TestContext.Out.WriteLine("Assert: Compare results");
            Assert.That(Car.GetSpeed(), Is.EqualTo(totSpeed));
        }


    }
}
