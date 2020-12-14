using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
    public class Day04 : IDay
    {
        public void Solve(string input)
        {
            ProcessPassports(input, false);
            ProcessPassports(input, true);
        }

        public void ProcessPassports(string input, bool validate = true)
        {
            var passports = GetPassports(input);
            var validCount = 0;


            Dictionary<string, Func<string, bool>> RuleList = new Dictionary<string, Func<string, bool>>
            {
                {"byr", BirthYear},
                {"iyr", IssueYear},
                {"eyr", ExpirationYear},
                {"hgt", Height},
                {"hcl", HairColor},
                {"ecl", EyeColor},
                {"pid", PassportID},
                {"cid", CountryId}
            };

            foreach (var entry in passports)
            {
                var isValid = true;
                foreach (var rule in RuleList.Keys.Where(k => !k.Equals("cid")))
                {
                    if (!entry.Contains(rule + ":"))
                    {
                        isValid = false;
                        break;
                    }
                }

                if (validate && isValid)
                {
                    foreach (var entryKeyValue in entry.Split(' '))
                    {
                        var entryKV = entryKeyValue.Split(':');
                        if (!RuleList[entryKV[0]](entryKV[1]))
                        {
                            //Console.WriteLine("Key:" + entryKV[0] + ",Value:" + entryKV[1]);
                            isValid = false;
                            break;
                        }
                    }
                }

                if (isValid)
                {
                    validCount++;
                } 
                else 
                {
                    Console.WriteLine(entry);
                }
            }

            Console.WriteLine(validCount + " Passports were valid!");
        }

        private readonly Func<string, bool> BirthYear = x => int.TryParse(x, out int ix) && ix >= 1920 && ix <= 2002;
        private readonly Func<string, bool> IssueYear = x => int.TryParse(x, out int ix) && ix >= 2010 && ix <= 2020;
        private readonly Func<string, bool> ExpirationYear = x => int.TryParse(x, out int ix) && ix >= 2020 && ix <= 2030;
        private readonly Func<string, bool> Height = x =>
        {
            if (x.EndsWith("cm"))
            {
                return int.TryParse(x.Substring(0, 3), out int ix) && ix >= 150 && ix <= 193;
            } 
            if (x.EndsWith("in"))
            {
                return int.TryParse(x.Substring(0, 2), out int ix) && ix >= 59 && ix <= 76;
            }

            return false;
        };
        private readonly Func<string, bool> HairColor = x => Regex.IsMatch(x, "^#[a-f0-9]{6}$");
        private readonly Func<string, bool> EyeColor = x => "amb blu brn gry grn hzl oth".Split(' ').Contains(x);
        private readonly Func<string, bool> PassportID = x => Regex.IsMatch(x, "^[0-9]{9}$");
        private readonly Func<string, bool> CountryId = x => true;

        private List<string> GetPassports(string path)
        {
            var entries = Regex.Split(File.ReadAllText(path), "\n\n").ToList();
            return entries.Select(e => Regex.Replace(e, "\n", " ").Trim()).ToList();
        }
    }
}
