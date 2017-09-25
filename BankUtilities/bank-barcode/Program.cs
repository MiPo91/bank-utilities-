using System;
using eKoodi.Utilities.Test;

namespace bank_barcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Anna tilinumero!");
            string iban = Console.ReadLine(); //FI58 1017 1000 0001 22
            Console.WriteLine("Anna summa!"); //482,99
            string value = Console.ReadLine();
            Console.WriteLine("Anna viitenumero!"); //RF06 5595 8224 3294 671
            string referenceNumber = Console.ReadLine();
            Console.WriteLine("Anna eräpäivä! (dd.mm.yyyy"); //31.1.2010
            string date = Console.ReadLine();


            string validIban = TestUtility.IbanValidator(iban);

            string validReferenceNumber;
            if (referenceNumber.Substring(0, 2) == "RF") {
                validReferenceNumber = TestUtility.InternationalReferencenumberValidator(referenceNumber);
            } else {
                validReferenceNumber = TestUtility.FinnishReferencenumberValidator(referenceNumber);
            }
            
            string barcode = TestUtility.Barcode(validIban, value, validReferenceNumber, date);

            Console.WriteLine(barcode);

            Console.ReadKey();
        }
    }
}
