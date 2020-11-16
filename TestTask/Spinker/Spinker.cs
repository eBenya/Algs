/*
   Задача на программирование:
   На лужайке стоит поливалка. Мы знаем её координаты и возможный угол разбрызгивание воды.
   На полянке есть цветы, у каждого цветка есть координаты и название сорта.  
   К сожалению, поливалка не может крутится на все 360 градусов, а может 
   поливать только сектор с заданным углом.
   Необходимо выбрать такое направление для поливалки, чтобы поливалось максимальное 
   количество разных сортов цветов. Не важно сколько всего цветов будет полито, важно 
   разнообразие сортов.
 */

using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Linq;

namespace Tests
{
    public class Task
    {
        public static void Main2()
        {
            List<Flower> flowers = new List<Flower>()
            { new Flower("a", new Point(1, 1)),
              new Flower("a", new Point(2, -1)),
              new Flower("a", new Point(1, -4)),
              new Flower("a", new Point(-1, 3)),
              new Flower("a", new Point(-1, -1)),
              new Flower("b", new Point(7, 1)),
              new Flower("b", new Point(-1, 7)),
              new Flower("b", new Point(-3, 5)),
              new Flower("b", new Point(5, 5)),
              new Flower("c", new Point(-3, 2)),
              new Flower("c", new Point(-3, -3))
            };
            Spinker spinker = new Spinker(60, new Point(3, 4));

            Console.WriteLine(spinker.CalculateAngle(flowers));
        }
    }
    class Spinker
    {
        int angle;
        Point poss;

        public Spinker(int angle, Point poss)
        {
            this.angle = angle;
            this.poss = poss;
        }

        public int CalculateAngle(IEnumerable<Flower> flowers)
        {
            int maxCount = 0;   //Число различных цветов
            int resAngle = 0;   //Угол где число различных цветов максимально
            for (int i = 0; i < 360; i++)
            {
                //Список различных растений, что попали под разбрызгиватель, и их количество
                Dictionary<string, int> selecter = new Dictionary<string, int>();
                
                double aL = AngleToRad(i - angle / 2);  //Левая граница полива в градусах
                double aR = AngleToRad(i + angle / 2);  //Правая граница полива в градусах

                double kleft = Math.Tan(aL);            //Левая граница полива в радианах
                double kright = Math.Tan(aR);           //Правая граница полива в радианах

                int quarter_left = QuarterIdent(i - angle / 2);     //Четверть левой границы полива
                int quarter_right = QuarterIdent(i + angle / 2);    //Четверть правой границы полива
                
                //Нужно определить в одной четверти или нет.
                //Исходя из ответа выбираем нижнию или верхнюю часть сектора
                //if ()

                foreach (var flower in flowers)
                {
                    //попадался ли уже подобный цветок?                    
                    bool isExist = selecter.TryGetValue(flower.Name, out int c);
                    if (!isExist || c == 0) //c==0 избыточно
                    {   
                        /*
                         * Проверяем на нахождение выше или ниже прямой
                         *      y=k(x-poss.X) + poss.Y;
                        */  
                        //Если угол в одной четверти
                        if(quarter_left == quarter_right)   
                        {
                            //fl(x)<=0  and fr(x)>=0  в 1 и 4 четвертях
                            if (quarter_left == 1 || quarter_left == 4)
                            {
                                //Цветок попал в сектор полива?
                                if (InSector(flower, x => IsAbove(kright, x) &&     //kright * (flower.Location.X - poss.X) + poss.Y - flower.Location.Y >= 0 &&
                                                          IsBelow(kleft, x)))       //kleft * (flower.Location.X - poss.X) + poss.Y - flower.Location.Y >= 0)
                                {   //встречался ли этот цветок ранее?
                                    AddInDict(selecter, flower.Name);
                                }

                            }
                            //fl(x)>=0  and fr(x)<=0  в 2 и 3 четвертях
                            else
                            {
                                if (InSector(flower, x => IsBelow(kright, x) &&
                                                          IsAbove(kleft, x)    )
                                   )
                                {
                                    AddInDict(selecter, flower.Name);
                                }
                            }  
                        }
                        //Угол в разных четвертях.(С поправкой на то, что угол полива не может быть больше 180 градусов)
                        else    
                        {
                            switch(quarter_right)
                            {
                                case 1:
                                    if(quarter_left == 2 || quarter_left == 3)
                                    {
                                        if (InSector(flower, x=> IsAbove(kright, x) &&
                                                                 IsAbove(kleft, x)) )
                                        {
                                            AddInDict(selecter, flower.Name);
                                        }
                                    }
                                    break;

                                case 2:
                                    if (quarter_left == 3)
                                    {
                                        if (InSector(flower, x => IsBelow(kright, x) &&
                                                                  IsAbove(kleft, x)))
                                        {
                                            AddInDict(selecter, flower.Name);
                                        }
                                    }
                                    else if (quarter_left == 4)
                                    {
                                        if (InSector(flower, x => IsBelow(kright, x) &&
                                                                  IsBelow(kleft, x)))
                                        {
                                            AddInDict(selecter, flower.Name);
                                        }
                                    }
                                    break;

                                case 3:
                                    if (quarter_left == 4 || quarter_left == 1)
                                    {
                                        if (InSector(flower, x => IsBelow(kright, x) &&
                                                                  IsBelow(kleft, x)))
                                        {
                                            AddInDict(selecter, flower.Name);
                                        }
                                    }
                                    break;

                                case 4:
                                    if (quarter_left == 1)
                                    {
                                        if (InSector(flower, x => IsAbove(kright, x) &&
                                                                  IsBelow(kleft, x)))
                                        {
                                            AddInDict(selecter, flower.Name);
                                        }
                                    }
                                    else if (quarter_left == 2)
                                    {
                                        if (InSector(flower, x => IsAbove(kright, x) &&
                                                                  IsAbove(kleft, x)))
                                        {
                                            AddInDict(selecter, flower.Name);
                                        }
                                    }
                                    break;

                                default:
                                    break;

                            }
                        }
                    }
                }
                var p = selecter.Select(w => w).Where((w) => w.Value > 0);
                if (maxCount < p.Count())
                {
                    maxCount = p.Count();
                    resAngle = i;
                }
            }
            return resAngle;
        }
        public bool InSector(Obj o, Predicate<Point> predicate) => predicate(o.Location);
        private bool IsAbove(double k, Point p)
        {
            return k * (p.X - poss.X) + poss.Y - p.Y >= 0;
        }
        private bool IsBelow(double k, Point p)
        {
            return k * (poss.X - poss.X) + poss.Y - poss.Y <= 0;
        }

        private void AddInDict<S>(Dictionary<S, int> d, S key, int value = 1, bool isExist = false)
        {
            if (isExist)
                d[key] += 1;
            else
                d.Add(key, value);
        }

        private int QuarterIdent(int t)
        {
            int t1 = t % 360;
            if (t1 >= 0 && t1 < 90) return 1;
            else if (t1 >= 90 && t1 < 180) return 2;
            else if (t1 >= 180 && t1 < 270) return 3;
            else return 4;
        }

        private double AngleToRad(int angle) => Math.PI * angle / 180;
    }
    class Flower : Obj
    {
        public Flower(string name, Point poss) : base(name, poss) {}
    }
    abstract class Obj 
    {
        protected Point poss;
        protected string name;
        public string Name { get => this.name; }
        public Point Location { get => this.poss; }
        protected Obj(string name, Point poss)
        {
            this.poss = poss;
            this.name = name;
        }
    }
}
