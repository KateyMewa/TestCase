using System;

namespace DaysCount
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the start date:");
            var validator = new DateTimeValidator();
            var startDate = Console.ReadLine();
            var validationResult = validator.Validate(startDate);

            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.ValidationErrors)
                {
                    Console.WriteLine(item);
                }
                return;
            }
            if (!DateTime.TryParse(startDate, out var startDateFormated))
            {
                Console.WriteLine("You have entered an incorrect value.");
                return;
            }
            Console.WriteLine("Please enter the end date:");
            var endDate = Console.ReadLine();
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.ValidationErrors)
                {
                    Console.WriteLine(item);
                }
                return;
            }
            if (!DateTime.TryParse(endDate, out var endDateFormated))
            {
                Console.WriteLine("You have entered incorrect value.");
                return;
            }
            Console.WriteLine($"{(endDateFormated - startDateFormated).Days} days");
        }
    }
}
