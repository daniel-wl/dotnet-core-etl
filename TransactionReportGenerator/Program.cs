using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CsvHelper;
using TransactionReportGenerator.Models;
using TransactionReportGenerator.BusinessLogic;

namespace TransactionReportGenerator
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("Usage: dotnet run PathToCsv");
                return 1;
            }

            string csvFile = Path.GetFullPath(args[0]);
            if(!File.Exists(csvFile))
            {
                Console.WriteLine($"File at {csvFile} does not exist.");
                return 1;
            }

            RunMenuLoop(csvFile);

            return 0;
        }

        public static void RunMenuLoop(string csvFile)
        {
            ConsoleKey choice;            
            PrintTitle();

            do
            {
                PrintMenu();
                choice = GetInput();
                RunBusinessLogic(choice, csvFile);
            }
            while(choice != ConsoleKey.D0);
        }

        public static void PrintTitle()
        {
            Console.WriteLine("************************************");
            Console.WriteLine("*** Transaction Report Generator ***");
            Console.WriteLine("************************************");
        }

        public static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("************ Main  Menu ************");
            Console.WriteLine("(1) Sales Summary");
            Console.WriteLine("(2) Assets Under Management Summary");
            Console.WriteLine("(3) Break Report");
            Console.WriteLine("(4) Investor Profit Report");
            Console.WriteLine("(0) Quit");
            Console.WriteLine();
        }

        public static ConsoleKey GetInput()
        {
            Console.Write("Please enter a number corresponding to your menu choice: ");
            ConsoleKey input =  Console.ReadKey().Key;
            Console.WriteLine();
            return input;
        }

        public static void RunBusinessLogic(ConsoleKey choice, string csvFile)
        {
            switch(choice)
            {
                case ConsoleKey.D1:
                    SalesSummary salesSummary = new SalesSummary(csvFile);
                    break;
                case ConsoleKey.D2:
                    break;
                case ConsoleKey.D3:
                    break;
                case ConsoleKey.D4:
                    break;
                case ConsoleKey.D0:
                    Console.WriteLine("Goodbye.");
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
    }
}
