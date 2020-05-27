using System;
namespace Domain.Model.Options
{
    public class TestOption
    {
        public TestOption()
        {
            ExampleString = nameof(TestOption);
        }

        public string ExampleString { get; set; }
        public bool ExampleBool { get; set; }
        public int ExampleInt { get; set; } = 10;

        public bool Validate()
        {
            var isValid = ExampleString == "Asd"
                   || ExampleString == "qwe"
                   || ExampleString == "zxc";

            return isValid;
        }
    }
}
