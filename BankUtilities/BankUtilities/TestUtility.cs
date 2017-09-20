using System;
using System.Collections.Generic;

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

                        Console.WriteLine("BBAN is valid. {0}", bbanNumero);

                    } else {
                        Console.WriteLine("BBAN is valid. {0}", bbanNumero);
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

            int remainder = (int)(ibanInt % 97);

            int checkDigit = 98 - remainder;

            iban = "FI"+checkDigit+bbanString;

            Console.WriteLine("IBAN: {0} ",iban);
            // 159030-776      

            return iban;
        }

        public static string ibanValidator(string userInput) {
            string iban = userInput.Replace(" ","");
            string bicKey;
            int bicKeyI;
            var bicList = new Dictionary<int, string>
                {
                    { 405, "HELSFIHH" },
                    { 497, "HELSFIHH" },
                    { 717, "BIGKFIH1" },
                    { 470, "POPFFI22" }, //if
                    { 479, "POPFFI22" },
                    { 713, "CITIFIHX" },
                    { 8, "DABAFIHH" },
                    { 34, "DABAFIHX" },
                    { 37, "DNBAFIHX" },
                    { 31, "HANDFIHH" },
                    { 799, "HOLVFIHH" },
                    { 1, "NDEAFIHH" },
                    { 2, "NDEAFIHH" },
                    { 5, "OKOYFIHH" },
                    { 33, "ESSEFIHX" },
                    { 39, "SBANFIHH" },
                    { 36, "SBANFIHH" },
                    { 38, "SWEDFIHH" },
                    { 400, "ITELFIHH" }, //if
                    { 6, "AABAFI22" }
                };

            if (iban.Length == 18) {
                string ibanFirst = iban.Substring(0, 4);
                iban = iban.Replace(ibanFirst, "") + ibanFirst;

                string ibanTransfer = iban.Replace("FI", "1518");
                decimal ibanChecker;
                decimal.TryParse(ibanTransfer, out ibanChecker);

                if ((int)(ibanChecker % 97) == 1) { //Valid, FI3715903000000776 
                    
                    // Bic key lookup
                    if (iban[0] == '1' || iban[0] == '2' || iban[0] == '5' || iban[0] == '6' || iban[0] == '8') {
                        bicKey = iban[0].ToString();
                    }
                    else if (iban[0] == '3') {
                        bicKey = iban[0].ToString() + iban[1].ToString();
                    }
                    else if (iban[0] == '4' || iban[0] == '7') {
                        bicKey = iban[0].ToString() + iban[1].ToString() + iban[2].ToString();
                    }
                    else { //9
                        Console.WriteLine("Invalid IBAN");
                        return "0";
                    }
                    int.TryParse(bicKey, out bicKeyI);

                    if (bicKeyI >= 470 && bicKeyI <= 478) {
                        bicKeyI = 470;
                    } else if ((bicKeyI == 715 || bicKeyI == 400 || bicKeyI == 402 || bicKeyI == 403) 
                    || (bicKeyI >= 406 && bicKeyI <= 408)
                    || (bicKeyI >= 410 && bicKeyI <= 412)
                    || (bicKeyI >= 414 && bicKeyI <= 421)
                    || (bicKeyI >= 423 && bicKeyI <= 432)
                    || (bicKeyI >= 435 && bicKeyI <= 452)
                    || (bicKeyI >= 454 && bicKeyI <= 464)
                    || (bicKeyI >= 483 && bicKeyI <= 493)
                    || (bicKeyI >= 495 && bicKeyI <= 496))
                    {
                        bicKeyI = 400;
                    }
                
                    Console.WriteLine("IBAN is valid. Bic: {0}", bicList[bicKeyI]);
                } else {
                    Console.WriteLine("IBAN is invalid.");
                    Console.WriteLine((int)(ibanChecker % 97));
                }

            } else {
                Console.WriteLine("Invalid IBAN.");
            }

            return iban;
        }

        // Finnish reference number validator
        public static string finnishReferencenumberValidator(string userInput) {
            string input = userInput.Replace(" ", "");
            int iInput;
            int.TryParse(input, out iInput);

            if(iInput > 0) {
                string lastNumber = input.Substring(input.Length -1, 1);
                string refBase = input.Substring(0, input.Length - 1);

                int factor = 0;
                int sum = 0;

                for (int i = refBase.Length - 1; i >= 0; i--) {
                    factor++;
                    if (factor == 1) { // 7
                        sum += (int)Char.GetNumericValue(refBase[i]) * 7;
                    }
                    else if (factor == 2) { // 3
                        sum += (int)Char.GetNumericValue(refBase[i]) * 3;
                    }
                    else { // 1
                        sum += (int)Char.GetNumericValue(refBase[i]);
                        factor = 0;
                    }
                }

                int nextTen;
                if (sum % 10 != 0) {
                    nextTen = (sum - sum % 10) + 10;
                }
                else {
                    nextTen = sum;
                }

                int checkDigit = nextTen - sum;
                if (checkDigit == 10) {
                    checkDigit = 0;
                }

                if(checkDigit.ToString() == lastNumber) {
                    int addSpace = 0;
                    for (int i = input.Length; i >= 0; i--)
                    {
                        if (i > 0 && addSpace == 5)
                        {
                            input = input.Insert(i, " ");
                            addSpace = 0;
                        }
                        addSpace++;
                    }

                    Console.WriteLine("{0} - OK", input);
                } else {
                    Console.WriteLine("Referencenumber Incorrect.");
                }

            }

            return "";
        }

        // Finnish reference number Generator
        public static string finnishReferencenumberGenerator(string userInput, string inputCount) {
            string input = userInput.Replace(" ","").TrimStart('0');
            int iInput;
            int.TryParse(input, out iInput);
            int iCount;
            int.TryParse(inputCount, out iCount);
            string inputBase = input;

            if(iInput < 1){
                Console.WriteLine("Invalid input, numbers only.");
                return "";
            }

            for (int j = 1; j <= iCount; j++) {
                int factor = 0;
                int sum = 0;

                input = inputBase + j.ToString();
                for (int i = input.Length - 1; i >= 0; i--)
                {
                    factor++;
                    if (factor == 1) { // 7
                        sum += (int)Char.GetNumericValue(input[i]) * 7;
                    }
                    else if (factor == 2) { // 3
                        sum += (int)Char.GetNumericValue(input[i]) * 3;
                    }
                    else { // 1
                        sum += (int)Char.GetNumericValue(input[i]);
                        factor = 0;
                    }
                }

                int nextTen;
                if (sum % 10 != 0) {
                    nextTen = (sum - sum % 10) + 10;
                }
                else {
                    nextTen = sum;
                }

                int checkDigit = nextTen - sum;
                if (checkDigit == 10) {
                    checkDigit = 0;
                }
                string referenceNumber = input + checkDigit.ToString();

                int addSpace = 0;
                for (int i = referenceNumber.Length; i >= 0; i--)
                {
                    if (i > 0 && addSpace == 5)
                    {
                        referenceNumber = referenceNumber.Insert(i, " ");
                        addSpace = 0;
                    }
                    addSpace++;
                }


                Console.WriteLine(referenceNumber);
            }


            return "";
        }


     }
}
