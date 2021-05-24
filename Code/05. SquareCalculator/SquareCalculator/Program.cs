using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SquareCalculator
{
    class Program
    {
        static void Square(int num, out int output)
        {
            output = num * num;
        }

        static void Main(string[] args)
        {
            WriteLine("제곱 계산기");
            Write("숫자 입력: ");
            int output;
            bool result = int.TryParse(ReadLine(), out output);
            if (result && output <= 46340)
            {
                Square(output, out output);
                WriteLine("결과: " + string.Format("{0:#,###}", output));
            }
            else
            {
                WriteLine("잘못된 입력 입니다.");
            }
        }
    }
}
