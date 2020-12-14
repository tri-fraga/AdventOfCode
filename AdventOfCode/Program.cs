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
    class Program
    {
        private const string DefaultYear = "2020";
        private const string DefaultDay = "08";
        private const string BaseDir = @"C:\dev\vs\AdventOfCode\AdventOfCode\";
        private const string DefaultFile = "input.txt";

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
            var validationMessage = "";
            var year = "Year";
            var day = "Day";
            var dir = "";
            var fileName = "";
            var filePath = "";

            // Check Arguments
            if(args.Length == 4)
            {
                if (!int.TryParse(args[0], out var intYear)) 
                    validationMessage = "Year must be a valid integer";
                else if (!int.TryParse(args[1], out var intDay)) 
                    validationMessage = "Day must be a valid integer";
                else
                {
                    year += intYear;
                    day += intDay.ToString().PadLeft(2, '0');
                    dir = args[2];
                    fileName = args[3];
                }
            } 
            // Or use default values
            else if (args.Length == 0)
            {
                year += DefaultYear;
                day += DefaultDay;
                dir = BaseDir;
                fileName = DefaultFile;
            }
            // Or log error 
            else
            {
                validationMessage = "Pass no args for default params, pass 'year day baseDir filename' to specify params";
            }

            // Build File and check
            dir = dir.EndsWith("\\") ? dir : dir + "\\";
            filePath = $"{dir}{year}\\{day}\\{fileName}";

            if (!File.Exists(filePath))
                validationMessage = @"Valid base dir and filename must be given (e.g. C:\dev\vs\AdventOfCode\AdventOfCode\ input.txt)";

            // Instantiate Day
            IDay dayInstance = Activator.CreateInstance(Type.GetType($"AdventOfCode.{year}.{day}")) as IDay;

            if (dayInstance == null)
                validationMessage = "AdventOfCode solving could not be initialized with the given parameters";

            // Output error and Return
            if (!string.IsNullOrEmpty(validationMessage))
            {
                Console.WriteLine(validationMessage);
                Console.ReadLine();
                return;
            }

            // Or Solve
            Console.WriteLine($"Solving {year} {day} with input '{filePath}'");
            dayInstance.Solve(filePath);

            Console.ReadLine();
        }

    }
}
