
using System;
using System.Collections.Generic;


namespace EdabitChalanges
{
    public class LeetCode
    {
        //https://leetcode.com/problems/roman-to-integer/
        public static int RomanToInt(string s)
        {
            Dictionary<char, int> hash = new Dictionary<char, int>
            {
                {'M',1000 },
                {'D',500},
                {'C',100},
                {'L',50},
                {'X',10},
                {'V',5},
                {'I',1}
            };

            int result = 0;
            for (int i = 0; i < s.Length; i++)
            {
                char currentRomanChar = s[i];
                int num = hash[currentRomanChar];
                if (i + 1 < s.Length && hash[s[i + 1]] > hash[currentRomanChar])
                    result -= num;
                else result += num;

            }
            return result;
        }


        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        //https://leetcode.com/problems/palindrome-linked-list/submissions/
        public static bool IsPalindrome(ListNode head)
        {
            if (head == null)
                return true;
            //create a stack and place all of the values here
            Stack<int> stack = new Stack<int>();
            ListNode curr = head;
            while(curr != null)
            {
                stack.Push(curr.val);
                curr = curr.next;
            }

            curr = head;
            while(curr != null)
            {
                // check if the values in stack are the same as the val in LinkedList
                if (stack.Count == 0 || stack.Peek() != curr.val)
                    return false;

                stack.Pop();
                curr = curr.next;
            }
            return stack.Count == 0;
        }

        public static int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> hash = new Dictionary<int, int>();
            int[] response = new int[2];

            for (int i = 0; i < nums.Length; i++)
            {
                int reminder = target - nums[i];
                if (hash.ContainsKey(nums[i]))
                {
                    response[0] = hash[nums[i]]; 
                    response[1] = i;
                    i = nums.Length;
                }
                else if (!hash.ContainsKey(reminder))
                    hash.Add(reminder, i);
            }
            return response;
        }

        //https://leetcode.com/problems/power-of-three/
        public bool IsPowerOfThree(int n)
        {
            if (n <= 0) return false;
            if (n % 3 == 0)
                return IsPowerOfThree(n / 3);

            return n == 1 ? true : false;
        }

       



    }
}
