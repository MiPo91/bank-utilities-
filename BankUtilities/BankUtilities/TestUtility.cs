using System;

namespace eKoodi.Utilities.Test
{
    public static class TestUtility
    {
        //BBAN Validator
        public static long bban(string input) {
            string[] inputSplit = input.Split('-');
            
            string userInput = input.Replace("-", "");
            string bbanFirst = "";
            string bbanMid = "";
            string bbanLast = "";
            string bbanComputer;
            int addZerosAt;

            // checking if char "-" is at right spot, if it exists
            if (inputSplit[0].Length != 6) {
                Console.WriteLine("Invalid BBAN number.");
                return 0;
            }

            // checking first 2 letters to know bank
            if (userInput[0] == '1' || userInput[0] == '2' || userInput[0] == '3' || userInput[0] == '6' || userInput[0] == '8') {
                if(userInput[0] == '3') {
                    if (userInput[1] == '1' || userInput[1] == '3' || userInput[1] == '4' || userInput[1] == '6' || userInput[1] == '7' || userInput[1] == '8' || userInput[1] == '9') {
                        addZerosAt = 6;
                    } else {
                        Console.WriteLine("Invalid BBAN number.");
                        return 0;
                    }
                } else {
                    addZerosAt = 6;
                }
            }
            else if(userInput[0] == '4' || userInput[0] == '5') {
                addZerosAt = 7;
            } else {
                Console.WriteLine("Invalid BBAN number.");
                return 0;
            }

            long bbanNumero;
            long.TryParse(userInput, out bbanNumero);

            int bbanLength = bbanNumero.ToString().Length;

            if (bbanNumero > 0) { // is input number
                if (bbanLength >= 8 && bbanLength < 15) { // is length between 8-14

                    if (bbanLength < 14) { // adding zeros after 6th or 7th character depending on bank
                        for(int i = 0; i < 14; i++) {
                            if(i < addZerosAt) {
                                bbanFirst += userInput[i];
                            } else if (i < bbanLength) {
                                bbanLast += userInput[i];
                            } else {
                                bbanMid += "0";
                            }
                        }

                        bbanComputer = bbanFirst + bbanMid + bbanLast; // compining the parts, BBAN is ready.
                        long.TryParse(bbanComputer, out bbanNumero);

                        Console.WriteLine(bbanNumero);
                        Console.WriteLine("BBAN is valid.");

                    } else {
                        Console.WriteLine(bbanNumero);
                        Console.WriteLine("BBAN is valid.");
                    }
                } else {
                    Console.WriteLine("Invalid BBAN number.");
                    bbanNumero = 0;
                }
            } else {
                Console.WriteLine("Invalid BBAN number.");
                bbanNumero = 0;
            }


            return bbanNumero;
        }

        // IBAN Muunnin
        public static string ibanTransfer(long bban) {
            string iban = "";
            string bbanString = bban.ToString();

            string ibanString = bbanString + "151800"; //F=15 I=18
            decimal  ibanInt;
            decimal.TryParse(ibanString, out ibanInt);

            ibanInt = ibanInt / 97;
            string remainder = (Math.Floor(ibanInt * 100) / 100).ToString().Split(',')[1]; // jako jäännös 1 liian iso ?


            int remainderInt;
            int.TryParse(remainder, out remainderInt);

            int checkDigit = 98 - remainderInt;

            iban = "FI"+checkDigit+bbanString;

            Console.WriteLine("IBAN: {0}, BIC: ",iban);
            // 159030-776
            // tulostaa FI3615903000000776
            // pitäisi  FI3715903000000776


            return iban;
        }

        public static string ibanValidator(string userInput) {
            string iban = userInput.Replace(" ","");

            if (iban.Length == 18) {
                string ibanReplace = iban.Substring(0, 4);
                iban = iban.Replace(ibanReplace, "");
                
                string ibanTransfer = iban + "1518"; //.Replace("FI","1518")
                decimal ibanChecker;
                decimal.TryParse(ibanTransfer, out ibanChecker);

                if(ibanChecker / 97 == 1) {
                    Console.WriteLine("IBAN is valid.");
                } else {
                    Console.WriteLine("IBAN is invalid.");
                    Console.WriteLine(ibanChecker / 97);
                }

                Console.WriteLine(ibanTransfer);

            } else {
                Console.WriteLine("Invalid IBAN.");
            }

            


            return iban;
        }

    
    }
}
