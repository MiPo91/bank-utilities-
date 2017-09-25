using System;
using eKoodi.Utilities.Test;

namespace iban_calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = Console.ReadLine();

            if (userInput.Length == 18) {
                TestUtility.IbanValidator(userInput);
            } else {
                long bban = TestUtility.Bban(userInput);

                if (bban != 0) {
                    var testi = TestUtility.IbanTransfer(bban);
                    TestUtility.IbanValidator(testi);
                }
            }
            Console.ReadKey();
        }
    }
}
