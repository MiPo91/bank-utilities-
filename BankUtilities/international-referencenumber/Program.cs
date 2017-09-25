using System;
using eKoodi.Utilities.Test;

namespace international_referencenumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 1 to Generate new reference Number.");
            Console.WriteLine("Press 2 to generate international reference number based valid finnish reference number.");
            Console.WriteLine("Press 3 to validate international reference Number.");

            string option = Console.ReadLine();

            if (option == "1") {
                Console.WriteLine("Enter new base for reference number.");
                string userInput = Console.ReadLine();

                string finnishRefNumber = TestUtility.FinnishReferencenumberGenerator(userInput, "1");
                TestUtility.InternationalReferencenumberGenerator(finnishRefNumber);
            } else if(option == "2") {
                Console.WriteLine("Enter valid finnish reference number.");
                string userInput = Console.ReadLine();

                string finnishRefNumber = TestUtility.FinnishReferencenumberValidator(userInput);
                TestUtility.InternationalReferencenumberGenerator(finnishRefNumber);
            } else {
                Console.WriteLine("Enter International referencenumber");
                string userInput = Console.ReadLine();

                TestUtility.InternationalReferencenumberValidator(userInput);
            }

            Console.ReadKey();
        }
    }
}
