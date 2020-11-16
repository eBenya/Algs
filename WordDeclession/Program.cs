using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordDeclession
{
    class Program
    {
		const string l = "ль";
		const string lya = "ля";
		const string ley = "лей";
		
        public static string Declession1(string pref, int num)
        {
            string s = num.ToString();
            if (s.Length>1)
            {
                if (s[s.Length - 1] == '1' && s[s.Length - 2] != '1')
                    return pref + l;
                if (s[s.Length - 1] > '1' && s[s.Length - 1] < '5' && s[s.Length - 2] != '1')
                    return pref + lya;
            }
            else
            {
                if (num == 1) return pref + l;
                if (num>1 && num < 5) return pref + lya;
            }
            return pref + ley;
        }

        public static string Declession2(string pref, int num)
        {
            var mod100 = num % 100;
            var mod10 = mod100 % 10;

            if (mod10 == 1 && mod100 != 11)
            {
                return pref + l;
            }
            if (mod10>1 && mod10<5 && (mod100<12 || mod100>15))
            {
                return pref + lya;
            }
            return pref + ley;
        }

        static void Main(string[] args)
        {
			//test
            Console.OutputEncoding = Encoding.UTF8;

            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine($"{i,3} - {Declession1("руб", i)} \t:{Declession2("руб", i)}");
            }
        }
    }
}
