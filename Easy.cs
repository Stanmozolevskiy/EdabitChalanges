

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EdabitChalanges
{
    public class Easy
    {
        //Find Bomb in a string
        public static string Bomb(string txt)
        {
            return new Regex(@"\W*((?i)bomb(?-i))\W*", RegexOptions.Compiled | RegexOptions.IgnoreCase)
                      .Match(txt).Success ? "Duck!!!" : "There is no bomb, relax.";
        }

        //https://edabit.com/challenge/GvGSPC9wiY4bS9AMg
        public static string FormatNum(int num)
        {
            if (num < 999) return num.ToString();
            
            string arr = num.ToString();
            List<char> resp = new List<char>();
            int index = 0;
            for(int i = arr.Length -1; i>-1; i--)
            {
                if (index % 3 == 0 && index != 0)
                    resp.Add(',');

                resp.Add(arr[i]);
                index++;
            }
            resp.Reverse();

            return string.Join("", resp);
        }
    }
}
