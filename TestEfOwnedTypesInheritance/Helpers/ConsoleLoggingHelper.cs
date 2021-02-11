using System;

namespace TestEfOwnedTypesInheritance.Helpers
{
    public static class ConsoleLoggingHelper
    {
        public static void WriteTestResult(string nameOfTestMethod, bool passed) =>
            Console.WriteLine($"{nameOfTestMethod} : {(passed ? "PASSED" : "FAILED")}");

        public static void WriteTestSeparator(string nameOfMethod)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"-- {nameOfMethod}");
            Console.WriteLine();
        }
    }
}
