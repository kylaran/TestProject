using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testProject.Validators
{
    public class Validate
    {
        public static bool BillsValidation(string input)
        {
            bool result = true;
            if (input.Length != 16)
                result = false;
            else
            {
                foreach (char c in input)
                {
                    if ((Char.IsNumber(c)) || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                    {
                        result = true;
                    }
                    else result = false;
                }
            }
            return result;
        }
        public static bool AmoutValidation(string input)
        {
            int count = input.Length - (input.IndexOf('.'));
            if (count >= 3)
            {
                return true;
            }
            else return false;
        }
    }
}