//using System;
//using TestEfOwnedTypesInheritance.Helpers;

//namespace TestEfOwnedTypesInheritance.LanguageFeatureTests
//{
//    public static class CSharpNineDemo
//    {

//        public static void Demo()
//        {
//            RecordTypesDemo();
//        }

//        private static void RecordTypesDemo()
//        {
//            ConsoleLoggingHelper.WriteTestSeparator(nameof(RecordTypesDemo));

//            var personClassA = new PersonClass { Name = "Bob" };
//            var personClassB = new PersonClass { Name = "Bob" };

//            Console.WriteLine(personClassA == personClassB ? "PersonClassA is equal to PersonClassB" : "PersonClassA is NOT equal to PersonClassB");

//            var personRecordA = new PersonRecord { Name = "Bob" };
//            //newRecord.MyProperty = 47;
//            var personRecordB = new PersonRecord { Name = "Bob" };

//            Console.WriteLine(personRecordA == personRecordB ? "PersonRecordA is equal to PersonRecordB" : "PersonRecordA is NOT equal to PersonRecordB");

//        }

//        public record PersonRecord
//        {
//            public int MyProperty { get; init; }

//            public string Name { get; init; } = string.Empty;

//        }

//        class PersonClass
//        {
//            public int MyProperty { get; init; }

//            public string Name { get; init; } = string.Empty;
//        }

//    }
//}
