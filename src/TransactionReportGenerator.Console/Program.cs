using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;
using TransactionReportGenerator.Core;
using TransactionReportGenerator.Core.Models;
using TransactionReportGenerator.Core.Reports;

namespace TransactionReportGenerator.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o => Run(o.PathToCsv));
        }

        public static void Run(string csvFile)
        {
            if (!File.Exists(csvFile))
            {
                System.Console.WriteLine($"File at {csvFile} does not exist.");
            }
            else
            {
                RunMenuLoop(TransactionLoader.LoadTransactions(csvFile));
            }
        }

        public static void RunMenuLoop(IEnumerable<Transaction> transactions)
        {
            ConsoleKey choice;
            PrintTitle();

            do
            {
                PrintMenu();
                choice = GetInput();
                RunBusinessLogic(choice, transactions);
            }
            while (choice != ConsoleKey.D0);
        }

        public static void PrintTitle()
        {
            System.Console.WriteLine("************************************");
            System.Console.WriteLine("*** Transaction Report Generator ***");
            System.Console.WriteLine("************************************");
        }

        public static void PrintMenu()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("************ Main  Menu ************");
            System.Console.WriteLine("(1) Sales Summary");
            System.Console.WriteLine("(2) Assets Under Management Summary");
            System.Console.WriteLine("(3) Break Report");
            System.Console.WriteLine("(4) Investor Profit Report");
            System.Console.WriteLine("(0) Quit");
            System.Console.WriteLine();
        }

        public static ConsoleKey GetInput()
        {
            System.Console.Write("Please enter a number corresponding to your menu choice: ");
            var input = System.Console.ReadKey().Key;
            System.Console.WriteLine();
            System.Console.WriteLine();
            return input;
        }

        public static void RunBusinessLogic(ConsoleKey choice, IEnumerable<Transaction> transactions)
        {
            switch (choice)
            {
                case ConsoleKey.D1:
                    PrintReport(new SalesReport(transactions));
                    break;
                case ConsoleKey.D2:
                    PrintReport(new AssetReport(transactions));
                    break;
                case ConsoleKey.D3:
                    PrintReport(new BreakReport(transactions));
                    break;
                case ConsoleKey.D4:
                    PrintReport(new ProfitReport(transactions));
                    break;
                case ConsoleKey.D0:
                    System.Console.WriteLine("Goodbye.");
                    break;
                default:
                    System.Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }

        public static void PrintReport(ReportBase report)
        {
            System.Console.WriteLine(report.PrintToString());
            System.Console.WriteLine("Press any key to continue.");
            System.Console.ReadKey();
        }

        public static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            System.Console.WriteLine("Fatal error. Aborting.");
            System.Console.WriteLine($"Reason: {((Exception)args.ExceptionObject).Message}");
            Environment.Exit(-1);
        }
    }
}
