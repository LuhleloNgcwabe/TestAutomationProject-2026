using Car_Unit_tests.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Car_Unit_tests
{
    [TestFixture,Category("CarFeature")]
    public class ParameterizedCarTest_TCSource
    {


        [SetUp]
        public void SetUp()
        {
            TestContext.Out.WriteLine("Start parameteirsed test using testcaseSource");
        }

        [TestCaseSource(nameof(CarTestCases))]
        public void VerifyCarDetails(string Make, string Model, int Year)
        {
            TestContext.Out.WriteLine("Arrange: Create new car object");
            Car Car = new Car(Make, Model, Year);
            TestContext.Out.WriteLine("Action: There is not particular action\n");
            TestContext.Out.WriteLine($"Assert: Verify that a {Make} car is created");
            Assert.Multiple(() =>
            {
                Assert.That(Car.Make, Is.EqualTo(Make), "Car make does not match");
                Assert.That(Car.Model, Is.EqualTo(Model), "Car model does not match");
                Assert.That(Car.Year, Is.EqualTo(Year), "Year of the car does not match");
            });

        }

        [TestCaseSource(nameof(engineTestCases), new object [] {true}),Category("sanity")]
        public void Verify_EngineIsOn(Car car)
        {
            TestContext.Out.WriteLine("First 2 stept \"Arrange and Action\" are already done by the test source");
            TestContext.Out.WriteLine("Assert that engine is switched on");
            Assert.That(car.IsEngineRunning,Is.True);
        }

        [TestCaseSource(nameof(engineTestCases),new object[] {false})]
        public void Verify_EngineIsOff(Car car)
        {
            TestContext.Out.WriteLine("First 2 stept \"Arrange and Action\" are already done by the test source");
            TestContext.Out.WriteLine("Assert that engine is switched on");
            Assert.That(car.IsEngineRunning, Is.False);
        }

        [TestCaseSource(typeof(CarTestCasesData), nameof(CarTestCasesData.CarTestCases))]
        public void Verify_GetCarInformation_WrttenInYearMakeModel(string Make,string Model,int Year)
        {
            TestContext.Out.WriteLine("Arrange: Create new car object");
            Car Car = new Car(Make, Model, Year);
            TestContext.Out.WriteLine("Action: There is not particular action\n");
            TestContext.Out.WriteLine($"Assert: Verify that  {Year} {Make} {Model} is created");
            Assert.That(Car.GetCarInfo(), Does.Contain($"{Make} {Model}").IgnoreCase);
        }

        //[TestCaseSource(typeof(CarTestCasesData),nameof(CarTestCasesData.SpeedtestData), new object[] { 10,2,3})]
        //public void testSpeed(Car car,double exp, double a,double s)
        //{

        //}

        //Internal data sources
        public static object[] CarTestCases =
        {
            new object[] { "Totota","Yaris",2013},
            new object[] {"VW","Polo TSI",2022},
            new object[] {"Hundai","i20", 2023 },
            new object[] {"Mazda", "c2",2019}
        };
        static IEnumerable<Car> engineTestCases(bool EngineIsRuning)
        {
            Car Car = new Car("Mazda", "c2", 2019);
            Car Car2 = new Car("VW", "Polo TSI", 2022);
            Car Car3 = new Car("Hundai", "i20", 2023);
            Car Car4 = new Car("Mazda", "c2", 2019);

            if (EngineIsRuning)
            {
                Car.StartEngine();
                Car2.StartEngine();
                Car3.StartEngine();
                Car4.StartEngine();

                yield return Car;
                yield return Car2;
                yield return Car3;
                yield return Car4;

            }
            else
            {
                yield return Car;
                yield return Car2;
                yield return Car3;
                yield return Car4;
            }
        }
    }

    public class CarTestCasesData
    {
        public static object[] CarTestCases =
        {
            new object[] { "Totota","Yaris",2013},
            new object[] {"VW","Polo TSI",2022},
            new object[] {"Hundai","i20", 2023 },
            new object[] {"Mazda", "c2",2019}
        };

       //public static IEnumerable<(Car,double,double)> SpeedtestData(bool Speed,double Initialspeed, double Accelerate)
       // {
       //     Car Car = new Car("Mazda", "c2", 2019);
       //     Car Car2 = new Car("VW", "Polo TSI", 2022);
       //     Car Car3 = new Car("Hundai", "i20", 2023);
       //     Car Car4 = new Car("Mazda", "c2", 2019);

       //     if (Speed)
       //     {
       //         Car.StartEngine();
       //         Car.Speed = Initialspeed;
       //         Car.Accelerate(Accelerate);

       //         Car2.StartEngine();
       //         Car2.Speed = Initialspeed;
       //         Car2.Accelerate(Accelerate);

       //         Car3.StartEngine();
       //         Car3.Speed = Initialspeed;
       //         Car3.Accelerate(Accelerate);

       //         Car4.StartEngine();
       //         Car4.Speed = Initialspeed;
       //         Car4.Accelerate(Accelerate);


       //         yield return (Car,5);
       //         yield return (Car2,5);
       //         yield return (Car3,6);
       //         yield return (Car4,90);

       //     }
       //     else
       //     {
       //         Car.StartEngine();
       //         Car.Speed = Initialspeed;
       //         Car.Brake(Accelerate);

       //         Car2.StartEngine();
       //         Car2.Speed = Initialspeed;
       //         Car2.Brake(Accelerate);

       //         Car3.StartEngine();
       //         Car3.Speed = Initialspeed;
       //         Car3.Brake(Accelerate);

       //         Car4.StartEngine();
       //         Car4.Speed = Initialspeed;
       //         Car4.Brake(Accelerate);

       //         yield return (Car,0);
       //         yield return (Car2,9);
       //         yield return (Car3,10);
       //         yield return (Car4,11);
       //     }
       // }
    }
}
