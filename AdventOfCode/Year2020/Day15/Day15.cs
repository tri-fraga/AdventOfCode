using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
    public class Day15 : IDay
    {
        public void Solve(string input)
        {
            var numbers = new[] {8, 11, 0, 19, 1, 2};
            var limit = 2020;
            var spoken = PlayList(numbers, limit);
            Console.WriteLine($"The {limit}th number spoken is {spoken}");
            
            limit = 30000000;
            spoken = PlayDictionary(numbers, limit);
            Console.WriteLine($"The {limit}th number spoken is {spoken}");
        }

        public int PlayList(int[] input, int limit)
        {
            var spokenNumbers = input.ToList();

            for (int i = input.Length; i < limit; i++)
            {
                var lastSpokenNumber = spokenNumbers[i - 1];
                var lastTimeSpokenNumber = spokenNumbers.GetRange(0,i-1).LastIndexOf(lastSpokenNumber);
                var speakingNumber = 0;
                if (lastTimeSpokenNumber != -1)
                {
                    speakingNumber = i - (lastTimeSpokenNumber + 1);
                }
                spokenNumbers.Add(speakingNumber);
            }

            return spokenNumbers.Last();
        }

        public int PlayDictionary(int[] input, int limit)
        {
            var spokenNumbers = new Dictionary<int, int>();
            var lastSpokenNumber = input[input.Length - 1];

            for (var index = 0; index < input.Length - 1; index++)
            {
                spokenNumbers[input[index]] = index;
            }

            for (var index = input.Length; index < limit; index++)
            {
                var speakingNumber = 0;
                if (spokenNumbers.TryGetValue(lastSpokenNumber, out int lastTimeSpokenNumber))
                {
                    speakingNumber = index - (lastTimeSpokenNumber + 1);
                }
                spokenNumbers[lastSpokenNumber] = index-1;
                lastSpokenNumber = speakingNumber;
            }

            return lastSpokenNumber;
        }
    }
}
