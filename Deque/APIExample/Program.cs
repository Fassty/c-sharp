using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var dq = new Deque<int>();
            dq.Add(1);
            dq.Insert(0, 5);
            dq.Insert(0, 5);
            dq.Insert(0, 5);
            dq.Insert(0, 5);
            dq.Insert(0, 5);
            dq.Insert(0, 5);
            dq.Insert(0, 5);
            dq.Insert(0, 5);
            dq.Insert(0, 5);
            dq.Insert(0, 5);
            dq.Insert(0, 5);
            dq.Insert(0, 5);
            dq.RemoveAt(0);
            dq.Add(9);
            dq.Add(11);
            dq.Insert(0, 23);
            foreach(var v in dq)
            {
                Console.WriteLine(v);
            }
        }
    }
}
