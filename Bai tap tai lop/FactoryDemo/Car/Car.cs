using System;

namespace CarLibrary
{
    public interface Car
    {
        string Name { get; }
        void Depart();
        Car Clone();
    }
}
