﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Helper
    {
        public static string[] LoadLines(string path, string seperator = "\n")
        {
            return Regex.Split(File.ReadAllText(path).Trim(), seperator);
        }

        public static int[] LoadIntLines(string path, string seperator = "\n")
        {
            return Array.ConvertAll(LoadLines(path, seperator), int.Parse); 
        }
    }
}
