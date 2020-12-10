using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Day7
    {
        public static void DoBagging()
        {
            var descriptions = Helper.LoadLines(@"C:\dev\vs\AdventOfCode\AdventOfCode\Day7\input.txt");
            var lookingFor = "shiny gold";
            List<Bag> bags = descriptions.Select(description => new Bag(description)).ToList();


            var lookingForCount = bags.Count(bag => bag.HasBag(bags, lookingFor));
            Console.WriteLine($"{lookingForCount} bags contain {lookingFor}");

            var containingBags = bags.Find(b => b.Name == lookingFor).CountBags(bags) - 1;
            Console.WriteLine($"{lookingFor} contains {containingBags} bags");
        }
    }
}
