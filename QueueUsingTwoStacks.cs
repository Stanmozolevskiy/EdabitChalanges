using System.Collections.Generic;

namespace EdabitChalanges
{
    public partial class HackerRank
    {
        //https://www.hackerrank.com/challenges/three-month-preparation-kit-queue-using-two-stacks/problem?isFullScreen=true&h_l=interview&playlist_slugs%5B%5D=preparation-kits&playlist_slugs%5B%5D=three-month-preparation-kit&playlist_slugs%5B%5D=three-month-week-eight&h_r=next-challenge&h_v=zen&h_r=next-challenge&h_v=zen
        public class QueueUsingTwoStacks
        {
            private List<int> s1;
            private List<int> s2;
            public QueueUsingTwoStacks()
            {
                s1 = new List<int>();
                s2 = new List<int>();
            }

            public void Enqueue(int val)
            {
                s1.Add(val);
            }

            public void Dequeue()
            {
                shift();
                s2.Remove(s2.Count - 1);
            }

            public int Peak()
            {
                shift();
                return s2[s2.Count - 1];
            }

            private void shift()
            {
                if (s2.Count == 0)
                foreach(int item in s1)
                {
                    s2.Add(item);
                    s1.Remove(item);
                }
            }
        }



    }
}


