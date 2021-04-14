using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyramidOutput
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = 10;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length - i - 1; j++)
                {
                    Console.Write(" ");
                }

                for (int j = 0; j < i * 2 + 1; j++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }
        }
    }
}
