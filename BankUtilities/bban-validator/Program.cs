﻿using System;
using eKoodi.Utilities.Test;

namespace bban_validator
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = Console.ReadLine();
            TestUtility.Bban(userInput);

            Console.ReadKey();

        }
    }
}
