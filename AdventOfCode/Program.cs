using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Year2020;

namespace AdventOfCode
{
    public static class Program
    {
        private const string DefaultYear = "2020";
        private const string DefaultDay = "15";
        private const string BaseDir = @"C:\dev\vs\AdventOfCode\AdventOfCode\";
        private const string DefaultFile = "";

        private const string Header = @"
                  ___       _                     _    _____   __  _____             _       
                 / _ \     | |                   | |  |  _  | / _|/  __ \           | |      
                / /_\ \  __| |__   __ ___  _ __  | |_ | | | || |_ | /  \/  ___    __| |  ___ 
                |  _  | / _` |\ \ / // _ \| '_ \ | __|| | | ||  _|| |     / _ \  / _` | / _ \
                | | | || (_| | \ V /|  __/| | | || |_ \ \_/ /| |  | \__/\| (_) || (_| ||  __/
                \_| |_/ \__,_|  \_/  \___||_| |_| \__| \___/ |_|   \____/ \___/  \__,_| \___|
                                                                                             
                                                                                             
        ";

        private static readonly string Seperator = new string(Enumerable.Repeat('-', 120).ToArray());


        /// <summary>
        /// [0] - [year]
        /// [1] - [day]
        /// [2] - [baseDir]
        /// [3] - [fileName]
        /// e.g. 2020 3 C:\dev\vs\AdventOfCode\AdventOfCode\ day3.txt
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IDay dayInstance;
            var validationMessage = string.Empty;
            var year = "Year" + DefaultYear;
            var day = "Day" + DefaultDay;
            var dir = BaseDir;
            var fileName = DefaultFile;
            var filePath = string.Empty;

            Console.ForegroundColor = ConsoleColor.Red;

            // Check Arguments
            if (args.Length >= 2)
            {
                if (!int.TryParse(args[0], out var intYear)) 
                    validationMessage = "Year must be a valid integer";
                else if (!int.TryParse(args[1], out var intDay)) 
                    validationMessage = "Day must be a valid integer";
                else
                {
                    year = "Year" + intYear;
                    day = "Day" + intDay.ToString().PadLeft(2, '0');
                }
            }

            if (args.Length == 4)
            {
                dir = args[2];
                fileName = args[3];
            }

            if(args.Length > 4)
            {
                validationMessage = "Pass no args for default params, pass 'year day baseDir filename' to specify params";
            }

            // Build File and check
            if (!string.IsNullOrEmpty(dir) && !string.IsNullOrEmpty(filePath))
            {
                dir = dir.EndsWith("\\") ? dir : dir + "\\";
                filePath = $"{dir}{year}\\{day}\\{fileName}";

                if (!File.Exists(filePath))
                    validationMessage = @"Valid base dir and filename must be given (e.g. C:\dev\vs\AdventOfCode\AdventOfCode\ input.txt)";

            }

            // Instantiate Day
            try
            {
                dayInstance = Activator.CreateInstance(Type.GetType($"AdventOfCode.{year}.{day}")) as IDay;
            }
            catch
            {
                dayInstance = null;
            }

            if (dayInstance == null)
                validationMessage = "AdventOfCode solving could not be initialized with the given parameters";

            // ASCII Header
            Console.WriteLine(Header);
            Console.WriteLine($"Solving {year} {day} with input '{filePath}'\n");

            Console.WriteLine($"\n{Seperator}\n");

            // Output error and Return
            if (!string.IsNullOrEmpty(validationMessage))
            {
                Console.WriteLine(validationMessage);
                Console.ReadLine();
                return;
            }

            // Or Solve
            dayInstance.Solve(filePath);

            Console.ReadLine();
        }

    }
}
