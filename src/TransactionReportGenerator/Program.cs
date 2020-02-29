﻿using System;
using System.Collections.Generic;
using System.IO;
using TransactionReportGenerator.Models;
using TransactionReportGenerator.Reports;

namespace TransactionReportGenerator
{
    public class Program
    {
        public static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            if (args.Length != 1)
            {
                Console.WriteLine("Missing Argument. Must provide path to CSV.");
                return 1;
            }

            string csvFile = Path.GetFullPath(args[0]);
            if (!File.Exists(csvFile))
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
            List<Transaction> transactions = TransactionLoader.LoadTransactions(csvFile);

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
            ConsoleKey input = Console.ReadKey().Key;
            Console.WriteLine();
            Console.WriteLine();
            return input;
        }

        public static void RunBusinessLogic(ConsoleKey choice, List<Transaction> transactions)
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
                    Console.WriteLine("Goodbye.");
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }

        public static void PrintReport(ReportBase report)
        {
            Console.WriteLine(report.PrintToString());
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        public static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Console.WriteLine("Fatal error. Aborting.");
            Console.WriteLine($"Reason: {((Exception)args.ExceptionObject).Message}");
            Environment.Exit(-1);
        }
    }
}
