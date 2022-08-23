using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace EdabitChalanges
{
    public class Medium
    {
        //Create a function to check if a candidate is qualified in an imaginary coding interview of an imaginary tech startup.
        //https://edabit.com/challenge/dZeNE4BJhyNgA99Fq
        public static string Interview(int[] arr, int tot)
        {
            var timeLimits = new int[] { 5, 5, 10, 10, 15, 15, 20, 20 };
            return arr.Zip(timeLimits, (a, b) => a <= b).Count(s => s) == 8 && tot <= 120
                ? "qualified"
                : "disqualified";
        }

        //Return the Index of All Capital Letters  https://edabit.com/challenge/6qFnpAhd3kdmYcNG2
        public static int[] IndexOfCapitals(string str)
        {
            char[] array = str.ToCharArray();
            List<int> result = new List<int>();
            for (int i = 0; i < array.Length; i++)
                if (char.IsUpper(array[i]))
                    result.Add(i);

            return result.ToArray();
        }

        public static bool GreaterThanOne(string str)
        {
            string[] arr = str.Split('/');
            return int.Parse(arr[0]) > int.Parse(arr[1]); 
        }

        public static bool ValidatePIN(string pin)=>
            new Regex(@"^([0-9]{4}|[0-9]{6})$", RegexOptions.Compiled | RegexOptions.IgnoreCase).Match(pin).Success;

        public static void plusMinus(List<int> arr)
        {
            decimal positive = 0.000000m;
            decimal negative = 0.000000m;
            decimal zeroes =   0.000000m;

            foreach (int num in arr)
            {
                if (num > 0) positive++;
                else if (num < 0) negative++;
                else if (num == 0) zeroes++;
            }

            Console.WriteLine(Math.Round(positive / arr.Count(),6));
            Console.WriteLine(Math.Round(negative / arr.Count(), 6));
            Console.WriteLine(Math.Round(zeroes / arr.Count(), 6));

        }

        //From Amazon interview
        public static long getHeaviestPackageIncorrect(List<int> list)
        {
            long res = 0;
            int size = list.Count();
            int i = size - 1;
            long sum = 0;
            while (i > 0)
            {
                if (list[i] > list[i - 1])
                {
                    sum += list[i] + list[i - 1];
                    list.RemoveAt(i);
                    list.Insert(i - 1, (int)sum);
                    if (res < sum)
                    {
                        res = sum;
                    }
                }
                else
                {
                    sum = 0;
                    i--;
                }
            }
            return res;
        }
        // Find duplicates in an array  https://practice.geeksforgeeks.org/batch-problems/find-duplicates-in-an-array/0/?track=amazon-arrays&batchId=348
        public int[] FindDuplicates(int[] arr, int n)
        {
            
            IEnumerable<IGrouping<int, int>> grouped = arr.GroupBy(c => c);
            return grouped.Max(e => e.Count()) > 1? grouped.Where(e => e.Count() > 1).Select(x=> x.Key).OrderBy(c=> c).ToArray() : new int[] {-1};
        }
        //https://www.hackerrank.com/challenges/three-month-preparation-kit-breaking-best-and-worst-records/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-one
        public static List<int> BreakingRecords(List<int> scores)
        {
            var (most , least) =  (scores[0], scores[0]);
            var (countMax, countMin) = (0,0);
            foreach (var score in scores)
                if (score > most)
                {
                    most = score;
                    countMax+=1;
                }
                else if( score < least) { 
                    least = score;
                    countMin+=1;
                }
            return new List<int>{ countMax, countMin };
        }


        public static void CamelCase(string data)
        {

            List<string> inputs = new List<string>();
            //string line = "";
            //while ((line = Console.ReadLine()) != null && line != "")
            //    inputs.Add(line);

            inputs =  data.Split("\r\n").ToList();

            foreach (string input in inputs) 
            { 
                var arr = input.Split(";", StringSplitOptions.RemoveEmptyEntries);
                var (firstArg, secondArg, sentence) = (arr[0], arr[1], arr[2]);
                var response = "";

                if (firstArg == "C") 
                {
                    response = string.Join("*", sentence.Split(" "));
                    response = secondArg == "C" ? Format(response, true, true, false) : 
                               secondArg == "M" ? Format(response, true, false, false):
                               Format(response, true, false, true);
                
                }
                else if (firstArg == "S") 
                {
                    response = string.Join(" ", Regex.Split(sentence, @"(?<!^)(?=[A-Z])"));
                    response = secondArg == "C" ? Format(response, false, true, false) :
                               secondArg == "M" ? Format(response, false, false, false) :
                               Format(response, false, false, true);
                }

                Console.WriteLine(response);
            }
           
        }
        
        private static string Format(string text, bool isCombined, bool isClass, bool isVar)
        {
            List<string> arr = isCombined ? text.Split("*").ToList() : text.Split(" ").ToList();
            List<string> response = isClass ? new List<string>(): new List<string> { arr[0] };
            string chars = "[" + String.Concat(new List<char>() { '(', ')'}) + "]";

            for (int i = isClass ? 0 : 1; i < arr.Count; i++)
                if (char.IsUpper(arr[i][0]))
                   response.Add(Regex.Replace(char.ToLower(arr[i][0]) + arr[i].Substring(1), chars, string.Empty).Trim());
                else response.Add(Regex.Replace(char.ToUpper(arr[i][0]) + arr[i].Substring(1), chars, string.Empty).Trim());

            if (isCombined && !isClass && !isVar)
                response.Add("()");

            return isCombined ? string.Join("", response) : string.Join(" ", response);
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-grading/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-two
        public static List<int> GradingStudents(List<int> grades)
        {
            List<int> roundedGrades = new List<int>();
            foreach (int grade in grades)
            {
                int difference = grade % 5;
                if (grade < 38)
                    roundedGrades.Add(grade);
                else
                    roundedGrades.Add(difference >= 3 ? grade + (5- difference) : grade);
            }

            return roundedGrades;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-flipping-bits/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-two
        public static long FlippingBits(long n)
        {

            long answer = 0;
            for (int i = 0; i < 32; i++)
            {
                if ((n & 1) == 0) answer += 1L << i;
                n >>= 1;
            }
            return answer;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-diagonal-difference/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-two&h_r=next-challenge&h_v=zen
        public static int DiagonalDifference(List<List<int>> arr)
        {
            List<int> firstSum = new List<int>();
            List<int> secondSum = new List<int>();
            int index = 0;

            for (int i = 0; i < arr.Count; i++)
                firstSum.Add(arr[i][i]);

            for (int i = arr.Count - 1; i > -1; i--)
            {
                secondSum.Add(arr[index][i]);
                index++;
            }
    
            return Math.Abs( firstSum.Sum() - secondSum.Sum());
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-countingsort1/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-two&h_r=next-challenge&h_v=zen&h_r=next-challenge&h_v=zen
        public static List<int> CountingSort(List<int> arr)
        {
            var result = new int[100];
            Array.Fill(result, 0);

            for (int i = 0; i < arr.Count; i++)
                result[arr[i]]++;

            return result.ToList();
        }


    }
}
