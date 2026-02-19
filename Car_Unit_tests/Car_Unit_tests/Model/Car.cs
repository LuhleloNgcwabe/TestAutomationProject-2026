using System;
using System.Collections.Generic;
using System.Text;

namespace Car_Unit_tests.Model
{
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double Speed { get; private set; }
        public bool IsEngineRunning { get; private set; }

        public bool TrunkIsOpen { get; set; }

        public Car(string make, string model, int year)
        {
            Make = make;
            Model = model;
            Year = year;
            Speed = 0;
        }

        public void Accelerate(double increment)
        {
            Speed += increment;
        }

        public void Brake(double decrement)
        {
            Speed = Math.Max(0, Speed - decrement);
        }

        public double GetSpeed()
        {
            return Speed;
        }

        public void StartEngine()
        {
            Console.WriteLine("Engine started.");
            IsEngineRunning = true;
            //ToggleEngine();
        }

        public void StopEngine()
        {
            Console.WriteLine("Engine stopped.");
            IsEngineRunning = false;
            //ToggleEngine() ;
        }

        public string Honk()
        {
            return "Beep beep!";
        }

        public void OpenTrunk()
        {
            Console.WriteLine("Trunk opened.");
            TrunkIsOpen = true;
        }

        public void CloseTrunk()
        {
            Console.WriteLine("Trunk closed.");
            TrunkIsOpen = false;
        }

        public string GetCarInfo()
        {
            return $"{Year} {Make} {Model}";
        }
        public void ToggleEngine()
        {
            IsEngineRunning = !IsEngineRunning;
            if (IsEngineRunning)
            {
                StartEngine();
            }
            else
            {
                StopEngine();
            }
        }
    }
}
