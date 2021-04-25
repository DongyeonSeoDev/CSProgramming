using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LookAndSaySequence
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("읽고 말하기 수열\n");

            string start = "1";
            int count = 0;
            string end = "";
            char same = ' ';

            for (int i = 0; i < 15; i++)
            {
                WriteLine($"{i + 1}번째 수열: {start}");
                count = 0;
                same = start[0];
                end = "";

                for (int j = 0; j < start.Length; j++)
                {
                    if (start[j] == same)
                    {
                        count++;
                    }
                    else
                    {
                        end = end + same + count;
                        same = start[j];
                        count = 1;
                    }
                }

                end = end + same + count;
                start = end;
            }

            WriteLine();
        }
    }
}
