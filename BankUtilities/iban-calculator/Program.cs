using System;
using eKoodi.Utilities.Test;

namespace iban_calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = Console.ReadLine();
            // long bban = TestUtility.bban(userInput);

            // if (bban != 0) {
            //     var testi = TestUtility.ibanTransfer(bban);
            // }

            TestUtility.ibanValidator(userInput);


            Console.ReadKey();
        }
    }
}
