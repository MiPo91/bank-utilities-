using System;
using eKoodi.Utilities.Test;

namespace finnish_referencenumber
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Press 1 to Generate reference Numbers. Press 2 to validate reference Number.");
            string option = Console.ReadLine();

            if (option == "2") {
                Console.WriteLine("Input reference number for validation.");
                string userInput = Console.ReadLine();
                TestUtility.FinnishReferencenumberValidator(userInput);
            } else {
                Console.WriteLine("Input base for Finnish reference number:");
                string userInput = Console.ReadLine();
                Console.WriteLine("Input reference number count:");
                string inputCount = Console.ReadLine();

                TestUtility.FinnishReferencenumberGenerator(userInput, inputCount);
            }

            Console.ReadKey();
        }
    }
}
