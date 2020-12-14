using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
    public class Bag
    {
        public Bag(string name, Dictionary<string, int> containingBags)
        {
            Name = name;
            ContainingBags = containingBags;
        }

        public Bag(string description)
        {
            Name = GetNameFromDescription(description);
            ContainingBags = GetBagsFromDescription(description);
        }

        public string Name { get; set; }

        public Dictionary<string, int> ContainingBags { get; set; }

        public bool HasBag(List<Bag> listOfBags, string name)
        {
            if (ContainingBags.ContainsKey(name))
            {
                return true;
            }

            foreach (var bag in ContainingBags)
            {
                var linkedBag = listOfBags.Find(b => b.Name == bag.Key);
                if (linkedBag != null && linkedBag.HasBag(listOfBags, name))
                {
                    return true;
                }
            }

            return false;
        }

        public int CountBags(List<Bag> listOfBags)
        {
            int count = 1;

            foreach (var bag in ContainingBags)
            {
                var linkedBag = listOfBags.Find(b => b.Name == bag.Key);

                var linkedCount = linkedBag.CountBags(listOfBags);
                count += bag.Value * linkedCount;
            }

            return count;
        }

        public static string GetNameFromDescription(string bagDescription)
        {
            var nameMatch = Regex.Match(bagDescription, "(?<mainBag>[a-z ]+) bag[s]* contain");
            var name = nameMatch.Groups["mainBag"].Value;
            return name;
        }

        public static Dictionary<string, int> GetBagsFromDescription(string bagDescription)
        {
            var bags = new Dictionary<string, int>();
            var bagMatches = Regex.Matches(bagDescription, @"((?<count>[\d]+) (?<bag>[a-z ]+) bag[s]*[,.]*)+");
            foreach (Match match in bagMatches)
            {
                int.TryParse(match.Groups["count"].Value, out var count);
                bags.Add(match.Groups["bag"].Value, count);
            }

            return bags;
        }

    }
}
