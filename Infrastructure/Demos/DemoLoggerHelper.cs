using Application.Interfaces.Demos;
using System;

namespace Infrastructure.Demos
{
    public class DemoLoggerHelper : IDemoLoggerHelper
    {
        public void WriteDemoSeparator(string nameOfMethod)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"-- {nameOfMethod}");
            Console.WriteLine();
        }
    }
}
