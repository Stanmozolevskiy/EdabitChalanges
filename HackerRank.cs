using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EdabitChalanges
{
    public partial class HackerRank
    {
        //https://www.hackerrank.com/challenges/three-month-preparation-kit-mini-max-sum/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-one
        public static string miniMaxSum(List<int> arr)
        {
            //this is done for overflow
            long sum = 0;
            foreach (long num in arr)
                sum += num;

            long smallest = sum - arr.Max();
            long largest = sum - arr.Min();
            return $"{smallest} {largest}";
        }

        //  https://www.hackerrank.com/challenges/three-month-preparation-kit-time-conversion/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-one
        public static string timeConversion(string time)
        {
            string[] arr = time.Split(":");
            string[] response = string.Join("", time.Take(8)).Split(":");
            string hours = arr[0];
            string letters = string.Join("", arr[2].Skip(2).Take(2));

            if (letters == "PM")

                if (hours == "12") return string.Join(":", response);
                else
                {
                    response[0] = (int.Parse(response[0]) + 12).ToString();
                    return string.Join(":", response);
                }

            else
            {
                if (hours == "12")
                {
                    response[0] = "00";
                    return string.Join(":", response);
                }
            }
            return string.Join(":", response);
        }


        //https://www.hackerrank.com/challenges/three-month-preparation-kit-divisible-sum-pairs/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-one
        public static int DivisibleSumPairs(int n, int k, List<int> ar)
        {
            int count = 0;
            for (int i = 0; i < ar.Count; i++)
                for (int j = 0; j < ar.Count; j++)
                    if (ar[i] != ar[j] && (ar[i] + ar[j]) % k == 0)
                        count++;

            return count;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-divisible-sum-pairs/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-one
        public static int DivisibleSumPairs(int k, List<int> arr)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            int numberOfPairs = 0;

            for (int i = 0; i < arr.Count; i++)
            {
                int remainder = arr[i] % k;
                int difference = k - remainder;
                // this means that arr[i] is divisible
                if (difference == k)
                    difference = 0;

                if (map.ContainsKey(difference))
                    numberOfPairs += map[difference];

                if (map.ContainsKey(remainder))
                {
                    var count = map[remainder];
                    map.Remove(remainder);
                    map.Add(remainder, count + 1);
                }
                else map.Add(remainder, 1);
            }
            return numberOfPairs;

        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-sparse-arrays/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-one&h_r=next-challenge&h_v=zen
        public static List<int> MatchingStrings(List<string> strings, List<string> queries)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            List<int> stringCount = new List<int>();

            for (int i = 0; i < strings.Count; i++)
                if (dictionary.ContainsKey(strings[i]))
                    dictionary[strings[i]]++;
                else dictionary.Add(strings[i], 1);

            for (int i = 0; i < queries.Count; i++)
                if (dictionary.ContainsKey(queries[i]))
                    stringCount.Add(dictionary[queries[i]]);
                else stringCount.Add(0);


            return stringCount;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-counting-valleys/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-two&h_r=next-challenge&h_v=zen&h_r=next-challenge&h_v=zen&h_r=next-challenge&h_v=zen
        public static int CountingValleys(int steps, string path)
        {
            List<char> arr = new List<char>();
            var (down, up) = (0, 0);

            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == 'D')
                {
                    down++;
                    up--;
                    arr.Add(path[i]);
                }
                else
                {
                    up++;
                    down--;
                    arr.Add(path[i]);
                }
                if (down == 0 && up == 0)
                    arr.Add(',');
            }

            return string.Join("", arr).Split(",").Where(x => x.StartsWith("D")).Count();
        }

        public static string Pangrams(string s)
        {
            Dictionary<char, int> hash = new Dictionary<char, int>();
            char[] lettersArray = s.ToLower().ToCharArray();
            for (int i = 0; i < lettersArray.Length; i++)
            {
                if (hash.ContainsKey(lettersArray[i]))
                    hash[lettersArray[i]]++;
                else if (!char.IsWhiteSpace(lettersArray[i]))
                    hash.Add(lettersArray[i], 0);
            }
            return hash.Keys.Count() >= 26 ? "pangram" : "not pangram";
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-mars-exploration/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-two
        public static int MarsExploration(string s)
        {
            char[] charArr = s.ToCharArray();
            int numberOfSos = charArr.Length / 3;
            char[] rithtOrder = string.Join("", Enumerable.Repeat("SOS", numberOfSos)).ToCharArray();
            int lettersAffected = 0;
            for (int i = 0; i < charArr.Length; i++)
                if (charArr[i] != rithtOrder[i])
                    lettersAffected++;
            return lettersAffected;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-the-birthday-bar/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-three&h_r=next-challenge&h_v=zen
        public static int Birthday(List<int> s, int d, int m)
        {
            int index = 0;
            int res = 0;
            int listSize = s.Count;
            int sum = 0;

            while ((m + index) <= listSize)
            {
                sum = index == 0 ? s.GetRange(0, m).Sum() :
                        sum + (s[index + (m - 1)]) - s[index - 1];
                index++;
                if (d == sum)
                    res++;
            }
            return res;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-sock-merchant/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-three
        public static int SockMerchant(int n, List<int> ar)
        {
            Dictionary<int, int> hash = new Dictionary<int, int>();
            foreach (int item in ar)
                if (!hash.ContainsKey(item))
                    hash.Add(item, 1);
                else hash[item]++;

            return hash.Values.Where(x => x > 1).Select(x => x / 2).ToList().Sum();
        }


        //https://www.hackerrank.com/challenges/three-month-preparation-kit-maximum-perimeter-triangle/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-three
        public static List<int> MaximumPerimeterTriangle(List<int> sticks)
        {
            sticks.Sort();
            var triangles = new List<List<int>>();
            for (int i = sticks.Count - 3; i >= 0; i--)
                if (sticks[i] + sticks[i + 1] > sticks[i + 2])
                    triangles.Add(new List<int> { sticks[i], sticks[i + 1], sticks[i + 2] });

            return triangles.Count == 0 ? new List<int> { -1 } : triangles[0];
        }


        //https://www.hackerrank.com/challenges/three-month-preparation-kit-picking-numbers/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-four
        public static int PickingNumbers(List<int> a)
        {
            a.Sort();
            var (len, start, num) = (0, 0, 1);
            for (int i = 1; i < a.Count; i++)
                if (a[i] - a[start] >= 2)
                {
                    num = 1;
                    len = Math.Max(len, i - start);
                    start = i;
                }
                else num++;
            return Math.Max(len, num);
        }

        public static int PageCount(int n, int p)
        {
            // number of the pages in total
            var pages = (n + 1) % 2 == 0 ? (n + 1) / 2 : (n + 1) / 2 + 1;
            int response = 0;
            if (pages > p)
                response = (p / 2);
            else response = n % 2 != 0 ? (n - p) / 2 : ((n - p + 1) / 2);
            return response;
        }

        public static string MaxOccurShort(string txt)
        {
            //sorted and grouped letters
            var groups = txt.ToCharArray().OrderBy(c => c).GroupBy(c => c);

            // find the max in a group
            var max = groups.Max(g => g.Count());
            // find the letters for the max occurances and make a string
            return max > 1 ? string.Join(", ", groups.Where(g => g.Count() == max).Select(g => g.Key.ToString()).ToArray()) : "No Repetition";
        }

        public static bool Cons(int[] arr)
        {
            Array.Sort(arr);
            return Math.Abs(arr[arr.Length - 1] - (arr.Length - 1)) - Math.Abs(arr[0]) == 0;

        }
        public static bool ConsShort(int[] arr) => arr.Max() + 1 - arr.Min() == arr.Length;

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-kangaroo/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-four
        public static string Kangaroo(int x1, int v1, int x2, int v2)
        {
            int distanceDifference = x2 - x1;
            int speedDifference = v1 - v2;
            if (v1 > v2 && distanceDifference % speedDifference == 0)
                return "YES";
            // in case if second Kangaroo starts behind
            if (v2 > v1 && x1 - x2 % v2 - v1 == 0)
                return "YES";

            return "NO";
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-separate-the-numbers/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-four&h_r=next-challenge&h_v=zen
        public static void SeparateNumbers(string s)
        {
            string substring = string.Empty;
            bool isValid = false;
            for (int i = 1; i <= s.Length/2; i++)
            {
                substring = s.Substring(0, i);
                long num = long.Parse(substring);
                string validString = substring;
                while (validString.Length < s.Length)
                    validString += (++num).ToString();
                if (s.Equals(validString))
                {
                    isValid = true;
                    break;
                }
            }
            Console.WriteLine(isValid ? "YES " + substring : "NO");

        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-closest-numbers/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-four&h_r=next-challenge&h_v=zen&h_r=next-challenge&h_v=zen
        public static List<int> ClosestNumbers(List<int> arr)
        {
            List<int> sorted  = quickSort(arr, 0, arr.Count-1);
            Dictionary<int, List<int>> hash = new Dictionary<int, List<int>>();
            for (int i = 0; i < sorted.Count-1; i++)
            {
                int tempSum = Math.Abs(sorted[i] - sorted[i + 1]);
                if (!hash.ContainsKey(tempSum))
                    hash.Add(tempSum, new List<int> { sorted[i], sorted[i + 1]});
                else
                {
                    List<int> tempVal = hash[tempSum];
                    hash.Remove(tempSum);
                    tempVal.Add(sorted[i]);
                    tempVal.Add(sorted[i+1]);
                    hash.Add(tempSum, tempVal);
                }
            }

            return  hash[hash.Keys.Min()];
        }
        private static List<int> quickSort(List<int> array, int leftIndex, int rightIndex)
        {
            int i = leftIndex;
            int j = rightIndex;
            int pivot = array[(leftIndex + rightIndex) / 2];
            while (i <= j)
            {
                while (array[i] < pivot)
                    i++;
                while (array[j] > pivot)
                    j--;
               
                if (i <= j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;

                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                quickSort(array, leftIndex, j);
            if (i < rightIndex)
                quickSort(array, i, rightIndex);
            return array;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-tower-breakers-1/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-four
        // more like a math problem, should not focuse on this
        public static int TowerBreakers(int n, int m)
        {
            if (m == 1 || n % 2 == 0) return 2;
            else return 1;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-minimum-absolute-difference-in-an-array/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-four&h_r=next-challenge&h_v=zen
        public static int MinimumAbsoluteDifference(List<int> arr)
        {
            List<int> sorted = quickSort(arr, 0, arr.Count -1);
            int response = int.MaxValue;
            for(int i = 0; i< sorted.Count-1; i++)
            {
                int temp = Math.Abs(sorted[i] - sorted[i + 1]);
                if (temp < response)
                    response = temp;
            }
            return response;
        }

        public static string CaesarCipher(string s, int k)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            string response = string.Empty;
            for(int i = 0; i< s.Length; i++)
                if (Regex.Match(s[i].ToString(), "[a-zA-Z]").Success)
                {
                    char letter = s[i];
                    int originalIndex = alphabet.IndexOf(char.ToLower(letter));
                    response = char.IsUpper(letter) ?
                        response + char.ToUpper(alphabet[(originalIndex+ k)%26]) :
                        response + alphabet[(originalIndex + k)% 26];
                }
                else response = response + s[i];
            return response;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-angry-children/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-five
        public static int MaxMin(int k, List<int> arr)
        {
            List<int> sorted = quickSort(arr, 0, arr.Count-1);
            int response = sorted[k-1] - sorted[0];
            for(int i = 1; i <= arr.Count-k ; i++)
            {
                var difference = sorted[i+k-1] - sorted[i]; 
                if (difference < response) response = difference;
            }
            return response;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-strong-password/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-five
        public static int minimumNumber(int n, string password)
        {
            int lackingElements = password.Length >= 6 ? 0 : 6-password.Length;
            int response = 0;
            if (!Regex.Match(password, "[a-z]").Success)
                response++;
            if (!Regex.Match(password, "[A-Z]").Success)
                response++;
            if (!Regex.Match(password, "[0-9]").Success)
                response++;
            if (!Regex.Match(password, "[!@#$%^&*()\\-+]").Success)
                response++;


            return response >= lackingElements ? response : lackingElements;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-missing-numbers/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-five&h_r=next-challenge&h_v=zen
        public static List<int> MissingNumbers(List<int> arr, List<int> brr)
        {
            Dictionary<int, int> hash = new Dictionary<int, int>();
            for (int i = 0; i < brr.Count; i++)
                if (hash.ContainsKey(brr[i]))
                    hash[brr[i]]++;
                else hash.Add(brr[i], 1);
            for (int i = 0; i < arr.Count; i++)
                if (hash.ContainsKey(arr[i]))
                        hash[arr[i]]--;
            List<int> response = hash.Where(x => x.Value > 0)
                                     .Select(x=> x.Key).ToList();
            response.Sort();

            return response;
        }
        //https://www.hackerrank.com/challenges/three-month-preparation-kit-countingsort4/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-five
        public static void CountSort(List<List<string>> arr)
        {
            for (int i = 0; i < arr.Count / 2; i++)
                arr[i][1] = "-";

            List<List<string>> result = new List<List<string>>();
            for (int i = 0; i < arr.Count; i++)
                result.Add(new List<string>());

            for (int i = 0; i < arr.Count; i++)
                result[int.Parse(arr[i][0])].Add(arr[i][1]);

            List<string> response = new List<string>();
            for (int i = 0; i < result.Count; i++)
                if(result[i].Count > 0)
                  response.Add(string.Join(" ", result[i]));
           

             Console.WriteLine(response.Aggregate((a, b) => a + " " + b));
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-grid-challenge/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-five
        public static string GridChallenge(List<string> grid)
        {
            List<string> result = new List<string>();
            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < grid.Count; i++)
                result.Add(string.Concat(grid[i].OrderBy(c => c)));

            for(int j = 0; j< result.Count-1; j++)
                for(int i = 0; i<result[j].Length-1; i++)
                {
                    if (alphabet.IndexOf(result[j][i]) >
                        alphabet.IndexOf(result[j+1][i]))
                        return "NO";
                }

            return "YES";
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-sansa-and-xor/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-five&h_r=next-challenge&h_v=zen
        public static int SansaXor(List<int> arr)
        {
            if (arr.Count % 2 == 0)
                return 0;

            int result = 0;
            for (int i = 0; i < arr.Count; i= i +2)
                result ^= arr[i];
            return result;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-dynamic-array/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-five
        public static List<int> DynamicArray(int n, List<List<int>> queries)
        {
            List<List<int>> arr = new List<List<int>>();
            List<int> answers = new List<int>();
            for (int i = 0; i < queries.Count; i++)
                arr.Add(new List<int>());
            
            int lastAnswer = 0;
            for(int i = 0; i< queries.Count; i++)
            {
                List<int> tempArr = queries[i];
                int y = tempArr[2];
                List<int> temp = arr[(tempArr[1] ^ lastAnswer) % n];

                if (tempArr.First() == 1)
                    temp.Add(y);
                else
                {
                    int v = y % temp.Count;
                    lastAnswer = temp[v];
                    answers.Add(lastAnswer);
                }
            }
            return answers;
        }


        public static SinglyLinkedListNode reverse(SinglyLinkedListNode llist)
        {
            SinglyLinkedListNode prev = null;
            SinglyLinkedListNode next = null;
            SinglyLinkedListNode current = llist;

            while(current != null)
            {
                next = current;
                current.next = prev;
                prev = current;
                current = next;
            }
            current = prev;
            return current;
        }

        public static string BalancedSums(List<int> arr)
        {
            if (arr.Count == 1) return "YES";
            int totalSum = arr.Aggregate((a,b) => a+b);
            int rightSum = 0;

            for (int i = arr.Count - 1; i >= 0; i--)
            {
                rightSum += arr[i];
                if (i != 0 && (totalSum - arr[i - 1] - rightSum) == rightSum) return "YES";
            }
            return "NO";
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-misere-nim-1/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-six&h_r=next-challenge&h_v=zen
        public static String misereNim(List<int> s)
        {
            bool allIsOne = true;
            foreach (int i in s)
                if (i != 1)
                {
                    allIsOne = false;
                    break;
                }
            
            int res = s.Aggregate((x, y)=> (x ^ y));

            if (allIsOne)
            {
                if (s.Count % 2 == 0) return "First";
                else return "Second";
            }
           
            else if (res == 0) return "Second";
            else return "First";
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-an-interesting-game-1/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-six
        public static string GamingArray(List<int> arr)
        {
            int peaks = 1;
            int lastPeakHigh = arr[0];

            for (int i = 1; i < arr.Count; i++)
                if (arr[i] > lastPeakHigh)
                {
                    lastPeakHigh = arr[i];
                    peaks++;
                }

            return peaks % 2 == 0 ? "ANDY" : "BOB";
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-magic-square-forming/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-six
        public static int FormingMagicSquare(List<List<int>> s)
        {
            List<List<int>> magic = new List<List<int>> {
                new List<int> {8, 1, 6, 3, 5, 7, 4, 9, 2},
                new List<int> {6, 1, 8, 7, 5, 3, 2, 9, 4},
                new List<int> {4, 9, 2, 3, 5, 7, 8, 1, 6},
                new List<int> {2, 9, 4, 7, 5, 3, 6, 1, 8},
                new List<int> {8, 3, 4, 1, 5, 9, 6, 7, 2},
                new List<int> {4, 3, 8, 9, 5, 1, 2, 7, 6},
                new List<int> {2, 7, 6, 9, 5, 1, 4, 3, 8},
                new List<int> {6, 7, 2, 1, 5, 9, 8, 3, 4}};


            List<int> flatList = new List<int>();
            foreach (var oneList in s)
                foreach (int num in oneList)
                    flatList.Add(num);

            int response = int.MaxValue;

            foreach (var term in magic)
            {
                int temp = getCost(term, flatList);
                if (response > temp)
                    response = temp;
            }
            return response;
        }

        private static int getCost(List<int> magic, List<int> data)
        {
            int sum = 0;
            for (int i = 0; i < data.Count; i++)
                if (data[i] != magic[i])
                    sum += Math.Abs(data[i] - magic[i]);
            return sum;
        }

        public static int SuperDigit(string n, int k)
        {
            long sum = (long)n.Select(char.GetNumericValue).Sum() * k;

            while (sum > 10)
                sum = (long)sum.ToString().Select(char.GetNumericValue).Sum();

            return (int)sum;
        }

        public static string CounterGame(long n)
        {
           
            return Convert.ToString(n - 1, 2).Select(x => x == '1').Count() % 2 == 0 ? "Louise" : "Richard";
        }

        public static long SumXor(long n)
        {
            if (n == 0) return 1;

            int zeroCount = 0;
            string binary = Convert.ToString(n, 2);
            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[i] == '0')
                    zeroCount++;
                
            }
            return (long)Math.Pow(2, zeroCount);
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-new-year-chaos/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-seven
        public static void MinimumBribes(List<int> q)
        {
            int bribes = 0;
            var (p1, p2, p3) = (1, 2, 3);
            for(int i = 0; i< q.Count; i++)
            {
                if (q[i] == p1)
                {
                    p1 = p2;
                    p2 = p3;
                    p3++;
                }
                else if (q[i] == p2)
                {
                    bribes++;
                    p2 = p3;
                    p3++;
                }
                else if (q[i] == p3)
                {
                    bribes += 2;
                    p3++;
                }
                else
                {
                    Console.WriteLine("Too chaotic");
                    return;
                }
            }
            Console.WriteLine(bribes);
        }


        //
        public static int Pylons(int k, List<int> arr)
        {
            int diameter = (k - 1) * 2 + 1;
            int plants = 0;
            int i = k - 1;
            while (true)
            {
                while (arr[i] == 0)
                {
                    i--;
                    if (i < 0 || arr[i] == 2) return -1;
                }
                arr[i] = 2;
                plants++;
                if (i + k > arr.Count - 1) return plants;
                i = Math.Min(arr.Count - 1, i + diameter);
            }
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-sherlock-and-valid-string/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-seven&h_r=next-challenge&h_v=zen
        public static string IsValid(string s)
        {
            Dictionary<char, int> hash = new Dictionary<char, int>();
           
            foreach (char letter in s.ToCharArray())
                if (hash.ContainsKey(letter))
                    hash[letter]++;
                else hash.Add(letter, 1);


            List<int> values = hash.Values.ToList();
            values.Sort();

            if(values.Count == 1)
                return "YES";

            int first = values[0];
            int second = values[1];
            int secondLast = values[values.Count -2];
            int last = values[values.Count -1];

            if (first == last) return "YES";

            if (first == 1 && second == last) return "YES";

            if (first == second && second == secondLast && secondLast == (last - 1)) return "YES";

            return "NO";
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-climbing-the-leaderboard/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-seven&h_r=next-challenge&h_v=zen&h_r=next-challenge&h_v=zen
        public static List<int> ClimbingLeaderboard(List<int> ranked, List<int> player)
        {
            List<int> distinct = ranked.Distinct().ToList();
            distinct.Sort();
            int i = 0;
            int n = distinct.Count;
            List<int> results = new List<int>();
           
            foreach(int score in player)
            {
                while (i<n && distinct[i] <= score)
                    i = i + 1;
                results.Add(n - i + 1);
            }

            return results;
        }
       

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-reverse-a-doubly-linked-list/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-seven
        public static DoublyLinkedListNode Reverse(DoublyLinkedListNode llist)
        {
            DoublyLinkedListNode prev = null;
            DoublyLinkedListNode next = null;
            DoublyLinkedListNode current = llist;

            while(current != null)
            {
                next = current.next;
                current.next = prev;
                current.prev = next;
                prev = current;
                current = next;
            }
            current = prev;
            return current;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-insert-a-node-at-a-specific-position-in-a-linked-list/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-seven
        public static SinglyLinkedListNode insertNodeAtPosition(SinglyLinkedListNode llist, int data, int position)
        {
            SinglyLinkedListNode newNode = new SinglyLinkedListNode()
            {
                data = data,
                next = null
            };

            SinglyLinkedListNode current = llist;
            int count = 0;
            while(position != count){
                current = current.next;
                count ++;
                }
            var holder = current.next;
            current.next = newNode;
                newNode.next = holder;
        
            return current;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-merge-two-sorted-linked-lists/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-eight
        static SinglyLinkedListNode mergeLists(SinglyLinkedListNode head1, SinglyLinkedListNode head2)
        {

            // create dummy node to avoid edge case of inserting into empty list;
            SinglyLinkedListNode dummy = new SinglyLinkedListNode();
            SinglyLinkedListNode tail = dummy;

            // check each node and compare the values and store them in a new llist
            while(head1 != null && head2 != null)
            {
                if (head1.data < head2.data)
                {
                    tail.next = head1;
                    head1 = head1.next;
                }
                else
                {
                    tail.next = head2;
                    head2 = head2.next;
                }
                tail = tail.next;
            }
            //if one of the list have remaining elements add those up
            if (head1 != null)
                tail.next = head1;
            else if (head2 != null)
                tail.next = head2;

            return dummy.next;
        }
      
        private static SinglyLinkedListNode breakLlist(SinglyLinkedListNode head, int val)
        {
            var firstPart = head;
            while (firstPart.next != null && firstPart.next.data < val)
                firstPart = firstPart.next;

            return firstPart;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-detect-whether-a-linked-list-contains-a-cycle/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-eight&h_r=next-challenge&h_v=zen
        static bool hasCycle(SinglyLinkedListNode head)
        {
            Dictionary<SinglyLinkedListNode, bool> hash = new Dictionary<SinglyLinkedListNode, bool>();
            SinglyLinkedListNode cur = head;
            while (cur.next != null)
            {
                if (hash.ContainsKey(cur))
                    return true;

                hash.Add(cur, false);
                cur = cur.next;
            }

            return false;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-icecream-parlor/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-eight
        public static List<int> IcecreamParlor(int m, List<int> arr)
         {
            var hash = new Dictionary<int, int>();

            for (int i = 0; i < arr.Count; i++)
            {
                var dif = m - arr[i];
                if (hash.ContainsKey(arr[i]))
                    return new List<int> { hash[arr[i]] + 1, i + 1 };
                else hash.Add(dif, i);
            }

            return new List<int> { 1 };
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-insert-a-node-into-a-sorted-doubly-linked-list/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-eight&h_r=next-challenge&h_v=zen
        public static DoublyLinkedListNode sortedInsert(DoublyLinkedListNode llist, int data)
        {
            DoublyLinkedListNode newNode = new DoublyLinkedListNode(data);
            if (llist.next == null) return llist;

            if (llist.data >= data)
            {
                llist.prev = newNode;
                newNode.next = llist;
                return newNode;
            }

            DoublyLinkedListNode cur = llist;
            while (llist != null)
            {
                if (cur.data >= data)
                {
                    cur.prev.next = newNode;
                    newNode.prev = cur.prev;
                    cur.prev = newNode;
                    newNode.next = cur;
                    break;
                }
                else if (cur.next == null)
                {
                    cur.next = newNode;
                    newNode.prev = cur;
                    break;
                }
                cur = cur.next;
            }
            return llist;
        }

        //https://www.hackerrank.com/challenges/three-month-preparation-kit-sherlock-and-anagrams/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-eight
        public static int SherlockAndAnagrams(string s)
        {
            Dictionary<string, int> hash = new Dictionary<string, int>();
            int length = 1;

            for (int i = 0; +i < s.Length; i++)
            {
                for (int j = 0; (j + i + 1) <= s.Length; j++)
                {
                    string pattern = string.Join("", s.Substring(j, i + 1)
                                            .ToCharArray()
                                            .OrderBy(x => x));
                    if (hash.ContainsKey(pattern))
                        hash[pattern]++;
                    else hash.Add(pattern, 1);
                }
                length++;
            }

            int anagramParirs = 0;
            foreach(var k in hash)
            {
                int v = k.Value;
                anagramParirs += (v * (v - 1)) / 2;
            }

            return anagramParirs;
        }

        public static string SuperReducedString(string s)
        {
            if (s.Length <= 1) return s;
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
                if (stack.Count == 0)
                    stack.Push(s[i]);

                else if(stack.Peek() == s[i])
                    stack.Pop();

                else stack.Push(s[i]);
          
            return stack.Count == 0 ? "Empty String" : string.Join("", stack.Reverse().Select(x => x));
        }
    }
}


