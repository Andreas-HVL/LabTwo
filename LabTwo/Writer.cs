using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabTwo
{
    public class Writer
    {
       public string input {  get; set; }
        public void SlowWrite(string input)
        {
            foreach (char c in input)
            {
                Console.Write(c);
                Thread.Sleep(50);
            }
        }
        public void FastWrite(string input)
        {
            foreach (char c in input)
            {
                Console.Write(c);
                Thread.Sleep(10);
            }
        }

    }
}
