using CarLibrary;
using System;

namespace Cabriolet
{
    public class Cabriolet : Car
    {
        public string Name { get => "Cabriolet"; }

        public void Depart()
        {
            Console.WriteLine("Removing the hood...");
            Console.WriteLine("Checking gasoline level...");
            Console.WriteLine("Cabriolet is starting");
        }

        public Car Clone()
        {
            return new Cabriolet();
        }
    }
}
