using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace EnterID
    {
        class Program
        {
            static void Main(string[] args)
            {
                while (true)
                {
                    Write("아이디를 입력해 주세요(2 ~ 100글자, 영어와 숫자만 사용 가능): ");
                    string str4 = ReadLine();
                    char[] arr2 = str4.ToCharArray();
                    bool b = true;

                    if (str4.Length > 100 || str4.Length < 2)
                    {
                        WriteLine("다시 입력하세요");
                        continue;
                    }

                    for (int i = 0; i < arr2.Length; i++)
                    {
                        if (!(('0' <= arr2[i] && arr2[i] <= '9') || ('a' <= arr2[i] && arr2[i] <= 'z') || ('A' <= arr2[i] && arr2[i] <= 'Z')))
                        {
                            WriteLine("다시 입력하세요");
                            b = false;
                            break;
                        }
                    }

                    if (b == true)
                    {
                        WriteLine("완료되었습니다");
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }