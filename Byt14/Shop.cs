using System;
using System.Collections.Generic;
using static Byt14.Degree;

namespace Byt14.State
{
    public class Shop
    {
        private const int EMPTY_SPACE_LENGTH = 53;
        private const ConsoleColor DEFAULT_CONSOLE_COLOR = ConsoleColor.White;
        private string _name;
        private IList<Degree> _degrees;
        private bool _allowReservation;
        private Stack<DegreeSnapshot> _degreeSnapshots;

        public Shop(string name, IList<Degree> degrees, bool allowReservation)
        {
            Console.ForegroundColor = DEFAULT_CONSOLE_COLOR;

            _name = name;
            _degrees = degrees;
            _allowReservation = allowReservation;
            _degreeSnapshots = new Stack<DegreeSnapshot>();
        }

        public void Start()
        {
            Console.WriteLine("#######################################################");
            Console.WriteLine($"# {_name}{new string(' ', EMPTY_SPACE_LENGTH - _name.Length - 1)}#");
            Console.WriteLine("#######################################################");

            ShowMenu();
        }

        private void ShowMenu()
        {
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("\n#######################################################");
                Console.WriteLine("# Wybierz operacje:                                   #");
                Console.WriteLine("#  1. Pokaz oceny                                     #");
                Console.WriteLine("#  2. Zarezerwuj ocene                                #");
                Console.WriteLine("#  3. Odrezerwuj ocene                                #");
                Console.WriteLine("#  4. Kup ocene                                       #");
                Console.WriteLine("#  5. Wycofaj operacje                                #");
                Console.WriteLine("#  6. Zamknij                                         #");
                Console.WriteLine("#######################################################");
                Console.Write("\nOperacja: ");

                key = Console.ReadKey(false);
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        DisplayStock();
                        break;
                    case ConsoleKey.D2:
                        ReserveDegree();
                        break;
                    case ConsoleKey.D3:
                        RemoveDegreeReservation();
                        break;
                    case ConsoleKey.D4:
                        PurchaseDegree();
                        break;
                    case ConsoleKey.D5:
                        CancelLastOperation();
                        break;
                    default:
                        break;
                }
            } while (key.Key != ConsoleKey.D6);
        }

        private void DisplayStock()
        {
            Console.WriteLine("\n\n#######################################################");
            Console.WriteLine("# Lista ocen:                                         #");

            for (var i = 0; i < _degrees.Count; i++)
            {
                var degreeName = _degrees[i].ToString();
                var stateName = _degrees[i].AvailabilityState.GetName();
                var remainingWhiteSpace = EMPTY_SPACE_LENGTH - degreeName.Length - stateName.Length - 8;

                Console.Write($"#  {i + 1}. {degreeName} (");
                Console.ForegroundColor = _degrees[i].AvailabilityState.GetDisplayColor();
                Console.Write($"{stateName}");
                Console.ForegroundColor = DEFAULT_CONSOLE_COLOR;
                Console.WriteLine($"){new string(' ', remainingWhiteSpace)}#");
            }

            Console.WriteLine("#######################################################");
        }

        private void ReserveDegree()
        {
            if (!_allowReservation)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nRezerwacja ocen zablokowana!");
                Console.ForegroundColor = DEFAULT_CONSOLE_COLOR;
                return;
            }

            var index = RequestValidDegreeIndex();
            var degree = _degrees[index];
            if (degree.CanReserve())
            {
                _degreeSnapshots.Push(degree.CreateSnapshot());
                degree.Reserve();
                PrintDegreeStatus(degree);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNie mozna zarezerwowac oceny!");
                Console.ForegroundColor = DEFAULT_CONSOLE_COLOR;
            }
        }

        private void RemoveDegreeReservation()
        {
            if (!_allowReservation)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nRezerwacja ocen zablokowana!");
                Console.ForegroundColor = DEFAULT_CONSOLE_COLOR;
                return;
            }

            var index = RequestValidDegreeIndex();
            var degree = _degrees[index];
            _degreeSnapshots.Push(degree.CreateSnapshot());
            degree.RemoveReservation();
            PrintDegreeStatus(degree);
        }

        private void PurchaseDegree()
        {
            var index = RequestValidDegreeIndex();
            var degree = _degrees[index];
            if (degree.CanSell())
            {
                _degreeSnapshots.Push(degree.CreateSnapshot());
                degree.SetSold();
                PrintDegreeStatus(degree);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNie mozna kupic oceny!");
                Console.ForegroundColor = DEFAULT_CONSOLE_COLOR;
            }
        }

        private void CancelLastOperation()
        {
            if (_degreeSnapshots.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nBrak operacji do wycofania!");
                Console.ForegroundColor = DEFAULT_CONSOLE_COLOR;
            }
            else
            {
                var snapshot = _degreeSnapshots.Pop();
                snapshot.Restore();
                Console.WriteLine("\nWycofano ostatnia operacje.");
            }
        }

        private int RequestValidDegreeIndex()
        {
            Console.WriteLine("");
            int index;
            do
            {
                Console.Write($"\nWybierz indeks oceny: ");
                var key = Console.ReadKey(false);

                index = int.TryParse(key.KeyChar.ToString(), out index) ? index - 1 : -1;
                if (index == -1 || index >= _degrees.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNiepoprawny indeks!");
                    Console.ForegroundColor = DEFAULT_CONSOLE_COLOR;
                }
            } while (index == -1 || index >= _degrees.Count);

            return index;
        }

        private void PrintDegreeStatus(Degree degree)
        {
            Console.Write("\nZmieniono status oceny na ");
            Console.ForegroundColor = degree.AvailabilityState.GetDisplayColor();
            Console.Write(degree.AvailabilityState.GetName());
            Console.ForegroundColor = DEFAULT_CONSOLE_COLOR;
            Console.WriteLine(".");
        }
    }
}
