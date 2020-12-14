using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
    public class Day09 : IDay
    {
        public void Solve(string input)
        {
            Decode(input);
        }

        public void Decode(string input)
        {
            var numbers = Helper.LoadIntLines(input).ToList();
        }
    }
}
