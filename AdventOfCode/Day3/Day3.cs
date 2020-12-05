using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Day3
    {
        public static void StartEncountering()
        {
            var map = Load2DFile(@"C:\dev\vs\AdventOfCode\AdventOfCode\Day3\day3.txt");

            Print2DArray(map);
            Console.ReadLine();

            var encounters = EncounterTrees(map);

            Console.WriteLine("The first answer is " + encounters);
            Console.ReadLine();

            var encounters2 = EncounterTrees(map, 1, 1) *
                              EncounterTrees(map, 1, 3) *
                              EncounterTrees(map, 1, 5) *
                              EncounterTrees(map, 1, 7) *
                              EncounterTrees(map, 2, 1);

            Console.WriteLine("The second answer is " + encounters2);
        }

        private static long EncounterTrees(char[,] map, int rStep = 1, int cStep = 3, int r = 0, int c = 0, int encounters = 0 )
        {
            
            var active = map[r, c];
            var hit = active == '#';

            encounters += hit ? 1 : 0;
            //map[r, c] = hit ? 'X' : 'O';

            r += rStep;
            c += cStep;

            if (c >= map.GetLength(1))
            {
                c %= map.GetLength(1);
            }

            if (r < map.GetLength(0))
            {
                return EncounterTrees(map, rStep, cStep, r, c, encounters);
            }

            return encounters;
        }

        public static char[,] Load2DFile(string path)
        {
            var file = File.ReadAllText(path);
            var fileRows = file.Trim().Split('\n');

            var twodeefile = new char[fileRows.Length, fileRows[0].Trim().Length];
            for (int r = 0; r < fileRows.Length; r++)
            {
                var fileColumns = fileRows[r].Trim().ToCharArray();
                for (int c = 0; c < fileColumns.Length; c++)
                {
                    twodeefile[r, c] = fileColumns[c];
                }
            }

            return twodeefile;
        }

        public static void Print2DArray(char[,] arr)
        {
            Console.Clear();
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Console.Write(arr[r, c] + "");
                }
                Console.WriteLine();
            }
        }
    }
}
