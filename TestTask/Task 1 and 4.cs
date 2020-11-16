using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
			if(args.Length==1)
			{
				switch (args[0])
				{
					case "StrGen":
						Console.WriteLine("Enter count numbers in the row");
						uint.TryParse(Console.ReadLine(), out uint countStr);
						
						string[] nums = StrGenerate(countStr);

						string resStr = Transform(nums);
						Console.WriteLine($"Resault row:\n\t{resStr}");
						break;
					case "LoadComb":
						Console.WriteLine("Entering combinations of weights. Separate each mass with a space:");
						string str = Console.ReadLine();
						int[] arr = str.Split(' ').intArrToStrArr();
						Array.Sort(arr);
						
						Console.WriteLine("Enter weight of element:");
						int.TryParse(Console.ReadLine(), out int weight);
						if (weight != 0)
						{
							int c = CalculatingLoadCount(arr, arr.Length - 1, weight);
							Console.WriteLine(c);
						}
						else
						{
							Console.WriteLine("Nothing hang");
						}
						break;
					default:
						Console.WriteLine("Start program with parametr:\n" +
							"\tStrGen - to generate a string framed by breackets\n" +
							"\tLoadCombo - for calculating the number of weight combinations");
						break;
				}
			}				
			else
			{
				Console.WriteLine("Start program with parametr:\n" +
							"\tStrGen - to generate a string framed by breackets\n" +
							"\tLoadCombo - for calculating the number of weight combinations");				
			}          
    }
        /**
		*** Task4.
		*** Написать функцию, возвращающую количество комбинаций
		*** заданного набора весов грузиков для заданного веса.
		**/		
        internal static int CalculatingLoadCount(int[] arr, int lastElem, int weight)
        {
            if (weight == 0 || lastElem < 1)
            {
                return 1;
            }

            int countComb = 0;

            for (int i = lastElem; i >= 0; i--)
            {
                int w = weight;
                if (w >= arr[i])
                {
                    w -= arr[i];
                    countComb += CalculatingLoadCount(arr, i, w);
                }
                if (lastElem < 1) countComb++;
            }

            return countComb;
        }


        
		/**
		*** Task1. 
		*** Дана последовательность ("1", "2", ... "n"). 
		*** Нужно получить из нее строку вида "{1{2{..{n}..}}}", не используя циклы.
		**/
        static string[] StrGenerate(uint count)
        {
            string[] str = new string[count];
            
            for(int i = 0; i < count; i++)
            {
                str[i] = i.ToString();
            }

            return str;
        }
        public static string Transform(string[] arr)
        {
            if (arr != null)
            {
                int len = 0;
                string outStr = "";
                return Transform(arr, ref outStr, ref len);
            }
            else
            {
                throw new NullReferenceException("Your array is invalid.");
            }
        }
        private static string Transform(string[] arr, ref string outStr, ref int len)
        {
            if (arr.Length == 0)
            {
                throw new ArgumentException("Marray must be > 0");
            }            
            
            if (arr.Length - 1 > len)
            {
                outStr += "{" + arr[len++].ToString();
                arr[arr.Length-1] += "}";
                Transform(arr, ref outStr, ref len);
            }
            else
            {

                outStr += "{" + arr[len].ToString() + "}";
            }
            return outStr;
        }
    }
	
    static class Extension
    {
        public static int[] intArrToStrArr(this string[] sArr)
        {
            int[] iArr = new int[sArr.Length];
            for (int i = 0; i < sArr.Length; i++)
            {
                int.TryParse(sArr[i], out iArr[i]);
            }
            return iArr;
        }
    }
}
