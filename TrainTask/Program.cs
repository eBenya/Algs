using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTask
{
    class Program
    {
        static void Main(string[] args)
        {
            const int maxIteration = 20;

            Console.WriteLine("Enter count railway carriage");
            if (int.TryParse(Console.ReadLine(), out int count))
            {
                //Число проходов для проверки
                //int iteration = maxIteration - Convert.ToInt32(Math.Log(count, 1.4));
                //if (iteration <= 0) iteration = 1;

                Train train = new Train(count);
                train.RandomLightCarriege();

                Console.WriteLine(train.HitherAndThitherCounting());    //Подсчет методом "челночного бега"
            }
        }
    }
}
