using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuclidAlg
{
    class Program
    {
        static void Main(string[] args)
        {
            //Алгоритм Евклида по поиску НОД
            int num1 = 489, num2 = 45, r;
            if (num1 > num2)
            {
                num1 ^= num2;
                num2 ^= num1;
                num1 ^= num2;
            }
            while ((num2 %= num1) != 0)
            {
                num1 ^= num2;
                num2 ^= num1;
                num1 ^= num2;
            }
            Console.WriteLine($"NOD = {num1}");
        }
    }
}
