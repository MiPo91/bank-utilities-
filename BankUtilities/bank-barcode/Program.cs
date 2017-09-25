using System;
using eKoodi.Utilities.Test;

namespace bank_barcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Anna tilinumero!");
            string tilinumero = Console.ReadLine();
            Console.WriteLine("Anna summa!");
            string summa = Console.ReadLine();
            Console.WriteLine("Anna viitenumero!");
            string viite = Console.ReadLine();
            Console.WriteLine("Anna eräpäivä! (dd.mm.yyyy");
            string erapaiva = Console.ReadLine();



            string iban = "FI58 1017 1000 0001 22";
            string value = "482,99";
            string referenceNumber = "RF06 5595 8224 3294 671";
            string date = "31.1.2010";

            string validIban = TestUtility.ibanValidator(iban);

            string validReferenceNumber;
            if (referenceNumber.Substring(0, 2) == "RF") {
                validReferenceNumber = TestUtility.internationalReferencenumberValidator(referenceNumber);
            } else {
                validReferenceNumber = TestUtility.finnishReferencenumberValidator(referenceNumber);
            }
            
            string barcode = TestUtility.barcode(validIban, value, validReferenceNumber, date);

            Console.WriteLine(barcode);

            Console.ReadKey();
        }
    }
}
