using System;
using System.Linq;
using System.Text.RegularExpressions;
using ASSIGMENT_Danh_Ba.Interface;

namespace ASSIGMENT_Danh_Ba.Sevices
{
    public class CheckEveryThing:ICheck
    {
        public bool checkString(string input)
        {
            if (input.All(char.IsDigit))
            {
                return true;
            }

            return false;
        }

        public bool checkNumber(string input)
        {
            if (Regex.IsMatch(input,@"[^\d+$]")==true)
            {
                return true;
            }

            return false;
        }

        public bool checkNull(string text)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                return true;
            }

            return false;
        }
    }
}