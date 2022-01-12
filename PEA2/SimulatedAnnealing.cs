using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEA2
{
    class SimulatedAnnealing
    {
        private int timeStop;
        private int[,] numArray = new int[,] { };
        private int[] currentPath = new int[] { };
        private int[] possiblePath = new int[] { };
        private int[] bestPath = new int[] { };
        private int city = 0;
        private double T ;
        private double factor = 0.99;
        private int when_decrease = 200;
        private Random rnd = new Random();
        private int samePath = 0;
        public SimulatedAnnealing(int timeStop, int[,] numArray, int city)
        {
            this.timeStop = timeStop;
            this.numArray = (int[,])numArray.Clone();
            this.city = city;
            this.T = city * city;
        }


        public void GeneratePath()
        {
            //generowanie poczatkowej sciezki
            currentPath = new int[city + 1];
            for (int i = 0; i < currentPath.Length - 1; i++)
            {
                currentPath[i] = i;
            }
            int j;
            for (int i = 1; i < currentPath.Length - 1; i++)
            {
                j = rnd.Next(1, currentPath.Length - 1);
                (currentPath[i], currentPath[j]) = (currentPath[j], currentPath[i]);
            }
            currentPath[city] = 0;
        }

        //zwrocenie dlugosci drogi
        public int GetDisance(int[] table)
        {
            int dist = 0;
            for (int i = 0; i < table.Length - 1; i++)
            {
                dist += numArray[table[i], table[i + 1]];
            }


            return dist;
        }

        //funkcja ktora zamienia miejscami dwa losowe miasta
        public void Swap()
        {

            int firstIndex = rnd.Next(1, city);
            int secondIndex;
            do
            {
                secondIndex = rnd.Next(1, city);
            } while (firstIndex == secondIndex);

            (possiblePath[firstIndex], possiblePath[secondIndex]) = (possiblePath[secondIndex], possiblePath[firstIndex]);

        }

        //zamiana aktualnej sciezki 
        public void SwapPath()
        {
            double b = rnd.NextDouble();
            double a = Math.Exp(-(GetDisance(possiblePath) - GetDisance(currentPath)) / T);
            
            if (GetDisance(possiblePath) <= GetDisance(currentPath))
            {
                currentPath = (int[])possiblePath.Clone();
                bestPath = (int[])possiblePath.Clone();
                samePath = 0;
               
            }
            else if (a  > b)
            {
                
                currentPath = (int[])possiblePath.Clone();
                samePath = 0;
                
            }
            else
            {
                samePath++;
               
            }



        }
        //glowna petla algorytmu
        public void Loop()
        {
            GeneratePath();
            int ready = 0;
            Stopwatch stopWatch = new Stopwatch();
            do
            {
                stopWatch.Start();
                ready++;
                if (ready == when_decrease) {
                    T = T * factor;
                    ready = 0;
                }
                
                possiblePath = (int[])currentPath.Clone();
                
                Swap();
                SwapPath();            
                if (samePath == 10000000) { break; }
                stopWatch.Stop();
            }
            while (stopWatch.Elapsed.TotalMilliseconds < timeStop);
            Console.WriteLine("Czas: " + Math.Floor(stopWatch.Elapsed.TotalMilliseconds / 1000) +"[s]" + "/" + Math.Round(stopWatch.Elapsed.TotalMilliseconds) + "[ms]");
            
            
            Console.WriteLine("Dystans: " + GetDisance(bestPath));
            //for (int i = 0; i < bestPath.Length - 1; i++)
            //{
            //    Console.Write(bestPath[i] + "-");
            //}
            //Console.Write("0");
            Console.WriteLine();



        }



    }
}
