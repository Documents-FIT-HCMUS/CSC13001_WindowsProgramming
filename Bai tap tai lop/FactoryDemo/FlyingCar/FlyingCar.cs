using CarLibrary;
using System;

namespace FlyingCar
{
    public class FlyingCar : Car
    {
        public string Name { get => "Flying"; }

        public Car Clone()
        {
            return new FlyingCar();
        }

        public void Depart()
        {
            Console.WriteLine("Checking fuel level...");
            Console.WriteLine("Checking wind direction...");
            Console.WriteLine("FlyingCar is starting");
        }
    }
}
