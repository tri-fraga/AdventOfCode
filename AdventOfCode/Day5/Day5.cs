using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Day5
    {
        public static void Board()
        {
            var passes = LoadBoardingPasses(@"C:\dev\vs\AdventOfCode\AdventOfCode\Day5\input.txt");

            List<int> passIds = new List<int>();

            foreach (var pass in passes)
            {
                int row = ProcessPassPart(pass.Substring(0,7), 128);
                int col = ProcessPassPart(pass.Substring(7,3), 8);
                int passId = row * 8 + col;
                
                passIds.Add(passId);
                //Console.WriteLine($"ID: {passId}, ROW: {row}, COL: {col}");
            }

            var orderedPassIds = passIds.OrderBy(i => i).ToList();

            Console.WriteLine("The highest pass id is " + orderedPassIds.Last());

            var first = orderedPassIds.First();
            var last = orderedPassIds.Last();

            for (int i = 0; i < last - first; i++)
            {
                if (orderedPassIds[i] != i + first)
                {
                    Console.WriteLine($"{i + first} is free");
                    first++; // increase offset by the missing one
                }
            }
        }

        private static int ProcessPassPart(string pass, int max = 128, int bas = 0)
        {
            //Console.WriteLine($"{pass}, max: {max}, base: {bas}");

            if (string.IsNullOrEmpty(pass)) return bas;

            max >>= 1;

            // Lower half
            // if (pass[0] == 'F' || pass[0] == 'L') {}

            // Upper half
            if (pass[0] == 'B' || pass[0] == 'R') bas += max;

            return ProcessPassPart(pass.Substring(1), max, bas);
        }

        private static string[] LoadBoardingPasses(string path)
        {
            return File.ReadAllText(path).Trim().Split('\n');
        }
    }
}
