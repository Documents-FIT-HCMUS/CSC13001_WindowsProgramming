using CarLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FactoryDemo
{
    class CarFactory
    {
        List<Car> _prototypes;
        private static CarFactory instance;

        // Singleton design pattern
        public static CarFactory getInstance()
        {
            if (null == instance)
                return new CarFactory();
            return instance;
        }

        private CarFactory()
        {
            _prototypes = new List<Car>();

            // Load .dll files
            string exePath = Assembly.GetExecutingAssembly().Location;
            string folder = Path.GetDirectoryName(exePath);
            var fis = new DirectoryInfo(folder).GetFiles("*.dll");

            // Traverse through all loaded files
            foreach (var f in fis) 
            {
                var assembly = Assembly.LoadFile(f.FullName);
                var types = assembly.GetTypes();

                foreach (var t in types)
                {
                    if (t.IsClass && typeof(Car).IsAssignableFrom(t))
                    {
                        Car c = (Car)Activator.CreateInstance(t);
                        _prototypes.Add(c);
                    }
                }
            }
        }

        public Car Create(int choice)
        {
            // Boundary check
            if (choice > _prototypes.Count || choice <= 0)
                return null;
            Car result = _prototypes[choice - 1].Clone();
            return result;
        }

        public void DisplayMenu()
        {
            for (int i = 0; i < _prototypes.Count; i++)
            {
                Car car = _prototypes[i];
                Console.WriteLine($"{i + 1}. {car.Name}");
            }
        }
    }
    class Program
    {
        static void Main()
        {
            // Show the menu
            Console.WriteLine("Which car do you want to create?");
            CarFactory.getInstance().DisplayMenu();

            // Get choice
            int choice = int.Parse(Console.ReadLine());

            // Create the car
            Car car = CarFactory.getInstance().Create(choice);
            if (null == car)
                Console.WriteLine("No such car");
            else
                car.Depart();

            Console.ReadLine();
        }
    }
}
