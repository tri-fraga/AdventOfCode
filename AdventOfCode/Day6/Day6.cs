﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Day6
    {
        public static void GetAnswerCount()
        {
            var answers = GetAnswers(@"C:\dev\vs\AdventOfCode\AdventOfCode\Day6\input.txt");

            GetAnswerCount1(answers);

            GetAnswerCount2(answers);
        }


        private static void GetAnswerCount1(string[] answers)
        {
            var sum = 0;

            foreach (var answer in answers)
            {

                var charArray = Regex.Replace(answer, @"\s+", "").ToCharArray().Distinct().ToArray();
                sum += charArray.Length;

                //Console.WriteLine($"{new string(charArray)} - {charArray.Length}");
            }

            Console.WriteLine($"The first answer is {sum}");
        }

        private static void GetAnswerCount2(string[] answers)
        {
            var sum = 0;

            foreach (var group in answers)
            {

                var groupAnswers = Regex.Split(group, @"\s");
                var possibleAnswers = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

                foreach (var answer in groupAnswers)
                {
                    possibleAnswers = possibleAnswers.Intersect(answer).ToArray();
                }

                sum += possibleAnswers.Length;

                //Console.WriteLine($"{new string(possibleAnswers)} - {possibleAnswers.Length}");
            }

            Console.WriteLine($"The second answer is {sum}");
        }

        private static string[] GetAnswers(string path)
        {
            return Regex.Split(File.ReadAllText(path).Trim(), "\n\n");
        }
    }
}
