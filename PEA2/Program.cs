
using System;
using System.IO;

namespace PEA2
{
    class Program
    {

        static void Main(string[] args)
        {
            int timeStop = 1000;
            int[,] numArray = new int[,] { };
            int city = 0;
            string choice = "";
            string filename = "UWAGA BRAK PLIKU ! ";
            do
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("1. Wybierz macierz z pliku" + "                             ||aktualny plik: " + filename+"||");
                Console.WriteLine("2. Wprowadzenie kryterium stopu" + "                        ||aktualny próg stopu: " + timeStop / 1000 + "[s]" + "/" + timeStop + "[ms]||");
                Console.WriteLine("3. Uruchomienie algorytmu oraz wyświetlenie wyników");
                Console.WriteLine("4. Wyjście");
                Console.Write("\r\nWybor: ");
                choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":

                        Console.Clear();
                        bool fileFound = false;
                        do
                        {
                            try
                            {
                                Console.WriteLine("---------------------------");
                                Console.WriteLine("\r\nPodaj nazwe pliku");
                                Console.Write("\r\nNazwa: ");
                                filename = Console.ReadLine();
                                using (StreamReader file = new StreamReader(filename + ".txt"))
                                {

                                    string ln;
                                    city = Int32.Parse(file.ReadLine());
                                    int ft = 0; int st = 0;
                                    numArray = new int[city, city];
                                    while ((ln = file.ReadLine()) != null)
                                    {
                                        char[] separators = new char[] { ' ', '.' };
                                        string[] subs = ln.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                                        foreach (var single_number in subs)
                                        {

                                            numArray[ft, st] = Int32.Parse(single_number);
                                            st++;
                                        }
                                       
                                            st = 0;
                                            ft++;
                                        
                                        
                                    }
                                    //dla miast tych samych wpisujemy droge -1
                                    for (int i = 0; i < city; i++)
                                    {
                                        numArray[i, i] = -1;
                                    }
                                    file.Close();
                                    fileFound = true;
                                }
                            }
                            catch (FileNotFoundException e) { Console.Clear(); Console.Write("Plik nie istnieje\n"); }
                        }
                        while (!fileFound);
                        break;
                    case "2":
                        int s_choice;
                        Console.Clear();
                        Console.WriteLine("---------------------------");
                        Console.WriteLine("1. Podaj czas w [s]");
                        Console.WriteLine("2. Podaj czas w [ms]");
                        Console.Write("\r\nWybor: ");
                        s_choice = Int32.Parse(Console.ReadLine());
                        if (s_choice == 1)
                        {
                            Console.WriteLine("\r\nPodaj czas w [s]");
                            timeStop = Int32.Parse(Console.ReadLine()) * 1000;
                            
                        }
                        else if (s_choice == 2)
                        {
                            Console.WriteLine("\r\nPodaj czas w [ms]");
                            timeStop = Int32.Parse(Console.ReadLine());
                        }
   
                        break;
                        

                    case "3":
                        for (int i = 0; i < 5; i++)
                        {
                            SimulatedAnnealing SA = new SimulatedAnnealing(timeStop, numArray, city);
                            SA.Loop();
                        }
                        break;

                    case "10":
                        randommatrix(55);
                        break;
                    default:

                        break;



                }
            } while (choice != "4");

        }




        static void randommatrix(int city)
        {
            int[,] numArray = new int[,] { };
            numArray = new int[city, city];
            // przypisujemy losowe wartosci dla macierzy
            Random rnd = new Random();
            for (int i = 0; i < numArray.GetLength(0); i++)
            {
                for (int j = 0; j < numArray.GetLength(1); j++)
                {
                    numArray[i, j] = rnd.Next(1, 88);
                    
                }
                
            }
            //dla miast tych samych wpisujemy droge -1
            for (int i = 0; i < city; i++)
            {
                numArray[i, i] = -1;
            }
            for (int i = 0; i < numArray.GetLength(0); i++)
            {
                for (int j = 0; j < numArray.GetLength(1); j++)
                {
                    
                    Console.Write(numArray[i, j] + " ");
                }
                Console.WriteLine();
            }

        }


    }



}
