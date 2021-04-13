using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n곱셈을 출력하는 코드\n");


            for (int x = 1; x <= 9; x++)
            {
                for (int i = 1; i <= 9; i++)
                {
                    Console.WriteLine(x + " * " + i + " = " + x * i);
                }

                Console.WriteLine();
            }
        }
    }
}
