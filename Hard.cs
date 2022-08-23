using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EdabitChalanges
{
    public class Hard
    {
        //Smooth Sentences  https://edabit.com/challenge/SkY5Nw3rS7WvkQmFc
        public static bool IsSmooth(string sentence)
        {
            sentence = sentence.ToUpper();
            for(int i = 0; i< sentence.Length; i++)
                if (sentence[i] == ' ')
                    if(sentence[i -1] != sentence[i + 1])
                        return false;
                
            return true;
        }

        //
        public static string Simplify(string str)
        {
            string[] arr = str.Split('/');
            int firtNumber = int.Parse(arr[0]);
            int secondNumber = int.Parse(arr[1]);
            if (firtNumber % secondNumber == 0)
                return $"{firtNumber / secondNumber}";

           int GCD = GetGCD(firtNumber, secondNumber);
           return $"{firtNumber / GCD}/{secondNumber / GCD}";  
        }
        private static int GetGCD( int first, int second)
        {
            while (first != 0 && second != 0)
            {
                if (first > second)
                    first -= second;
                else
                    second -= first;
            }
           return Math.Max(first, second);
        }
        private int ShortGCD(int first, int second) => (first == 0) ? second : ShortGCD(second % first, first);

        
        //  C*ns*r*d Str*ngs    https://edabit.com/challenge/wunaXvZw3WctYioeC
        public static string Uncensor(string txt, string vowels)
        {
            List<char> result = new List<char>();
            int count = 0;
            for (int i = 0; i < txt.Length; i++)
            {
                if (txt[i] == '*')
                {
                    result.Add(vowels[count]);
                    count++;
                }
                else result.Add(txt[i]);
            }
            return String.Join("", result.ToArray());
        }

        public static string UncensorShort(string txt, string vowels)
        {
            Queue<char> q = new Queue<char>(vowels.ToCharArray());
            return Regex.Replace(txt, @"\*", m => q.Dequeue().ToString());
        }

        //Maximum Occurrence https://edabit.com/challenge/vtdfueRCmpRGyLAGs
        public static string MaxOccur(string text)
        {
            List<int> listOfRepeatedChars = new List<int>();
            //get the number of ocuranses to a listOfRepeatedChars
            for (int i = 0; i< text.Length; i++)
                listOfRepeatedChars.Add(checkDuplicates(text[i], text));
            
            List<char> resultList = new List<char>();
            int firstMax = 0;

            //find the largest numbers and move the letters based on index to result array
            for(int i =0; i < listOfRepeatedChars.Count; i++)
            {
                int max = listOfRepeatedChars.Max();
                if (max == 0) break;
                if(max >= firstMax )
                {
                    firstMax = max;
                    int indexOfMax = listOfRepeatedChars.IndexOf(max);
                    //because we remove 1 item from listOfRepeatedChars
                    ////we neex to add the number of elements we removed in order to grab the rith letter
                    resultList.Add(text[indexOfMax + resultList.Count]);
                    listOfRepeatedChars.RemoveAt(indexOfMax);
                }
            }
            
            resultList.Sort();
            return resultList.Count > 0 ? string.Join("", string.Join(", ",resultList.Distinct())) : "No Repetition";
        }

        private static  int checkDuplicates(char letter, string text)
        {
            //because there will be at least one letter, itself
            int count = -1;
            foreach (char l in text)
                if (l == letter)
                    count++;
            return count;
        }


    }
}
