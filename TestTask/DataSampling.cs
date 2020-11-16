/***
 * Сделать из этого ["Кот", "фыаыв", "кто", "аы", "ток", "рот"]
 * это  [ ["Кот", "кто", "ток"],
 *        ["фыаыв"],
 *        ["аы"],
 *        ["рот"]
 *      ]
 ***/

using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    static class Program
    {
        static bool CompareStr(this string str1, string str2)
        {
            return string.Concat(str1.OrderBy(x => x).ToArray())
                .Equals(string.Concat(str2.OrderBy(x => x).ToArray()),
                        StringComparison.OrdinalIgnoreCase);
        }
        static void Main(string[] args)
        {
            string[] inS = { "Кот", "фыаыв", "кто", "аы", "ток", "рот" };
            
            //Fast crutch to avoid exception
            string s4 = "";
            List<List<string>> outS = new List<List<string>>();

            List<string> ss = new List<string>();
            ss.Add(s4);
            outS.Add(ss);
            //End crutch

            foreach (var in1 in inS)
            {
                //calc last element
                int last = outS.Count;
                for (int i = 0; i < last; i++)
                {
                    //Compare string, if equal, added to this box
                    if (in1.CompareStr(outS[i][0]))
                    {
                        outS[i].Add(in1);
                        break;
                    }
                    else
                    {
                        //If don`t find a match until the end of the array, add new lex
                        if (i == last - 1)
                        {
                            var ls = new List<string>();
                            ls.Add(in1);
                            outS.Add(ls);
                            break;
                        }

                    }
                }
            }
            //Print test case
            foreach (var item in outS)
            {
                foreach (var it2 in item)
                {
                    Console.WriteLine(it2);
                }
                Console.WriteLine();
            }
        }
    }
}