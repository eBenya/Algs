using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * Наткнулся на задачку о закольцованном поезде. Условие:
 * Поезд закольцован(голвной вагон соеденен с последним).
 * Вы находитесь внутри, что происходит снаружи мы не видим.
 * Мы можем только включать или выключать свет в том или ином вагоне.
 * Изначально свет во всех вагонах имеется в случайном порядке.
 * Нужно узнать сколько всего вагонов в поезде
 **/
namespace TrainTask
{
    class Train
    {
        public int CountCarriage { get; private set; }
        private List<bool> LightInCarriage;
        private int currentCarriage;

        public Train(int countCarriage)
        {
            LightInCarriage = new List<bool>();
            /*LightInCarriage.Capacity = */
            CountCarriage = countCarriage;
            for (int i = 0; i < countCarriage; i++)
            {
                LightInCarriage.Add(false);
            }
            currentCarriage = 0;
        }
        public Train(int countCarriage, List<bool> lightInCarriage) : this(countCarriage)
        {
            LightInCarriage = lightInCarriage;
        }

        public void RandomLightCarriege()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < CountCarriage; i++)
            {
                if(random.NextDouble() >= 0.5)
                        LightInCarriage[i] = true;
                else LightInCarriage[i] = false;
            }
        }

        public bool Current()
        {
            return LightInCarriage[currentCarriage];
        }
        public bool Next()
        {
            if ((++currentCarriage) == CountCarriage) currentCarriage = 0;

            return LightInCarriage[currentCarriage];
        }
        public bool Previous()
        {
            if ((--currentCarriage) < 0) currentCarriage = CountCarriage-1;

            return LightInCarriage[currentCarriage];
        }

        public void OnLight(){ LightInCarriage[currentCarriage] = true; }
        public void OffLight() { LightInCarriage[currentCarriage] = false; }

        public void CarriageLight(bool state) { LightInCarriage[currentCarriage] = state; }
        

        //solution for the task
        public int HitherAndThitherCounting()
        {
            int allCarriage = 0, buffer = 1;
            bool condition = !Current();
            OnLight();
            int round = 0;
            while (allCarriage == 0)
            {
                while (!Next())
                {
                    buffer++;
                }
                OffLight();
                for (int i = 0; i < buffer; i++)
                {
                    Previous();
                }
                if (Current() == false)
                {
                    allCarriage = buffer;
                    break;
                }
                else buffer = 1;
            }

            return allCarriage;
        }
    }
}
