using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
    //https://adventofcode.com/2020/day/14
    public class Day14 : IDay
    {
        public void Solve(string input)
        {
            var lines = Helper.LoadLines(input);
            var sumV1 = DecoderV1(lines);
            Console.WriteLine($"The first sum is {sumV1}");
            var sumV2 = DecoderV2(lines);
            Console.WriteLine($"The second sum is {sumV2}");
        }

        public long DecoderV1(string[] programLines)
        {
            var memoryDictionary = new Dictionary<int, BitArray>();
            var mask = "";

            foreach (var line in programLines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = GetMaskFromString(line);
                }
                else if (line.StartsWith("mem"))
                {
                    var mem = GetAddressValueFromString(line);

                    var bitArray = GetBitArrayFromInt(mem.Item2);

                    for (int i = 0; i < bitArray.Length; i++)
                    {
                        switch (mask[i])
                        {
                            case '0':
                                bitArray[i] = false;
                                break;
                            case '1':
                                bitArray[i] = true;
                                break;
                            case 'X':
                                break;
                        }
                    }

                    memoryDictionary[mem.Item1] = bitArray;
                }
            }

            return memoryDictionary.Sum(entry => GetLongFromBitArray(entry.Value));
        }

        public long DecoderV2(string[] programLines)
        {
            var memoryDictionary = new Dictionary<long, long>();
            List<BitArray> masks = null;
            BitArray initialOverwritingMask = null;
            BitArray initialFloatingMask = null;

            foreach (var line in programLines)
            {
                if (line.StartsWith("mask"))
                {
                    var maskLine = GetMaskFromString(line);
                    var xMask = maskLine.Replace('1', '0');
                    var xCount = xMask.Count(c => c.Equals('X'));

                    masks = new List<BitArray>();
                    initialOverwritingMask = GetBitArrayFromString(maskLine.Replace('X', '0'));
                    initialFloatingMask = GetBitArrayFromString(xMask.Replace('X', '1'));
                    
                    for (int x = 0; x < Math.Pow(2, xCount); x++)
                    {
                        var floatingBits = GetBitArrayFromInt(x);
                        var floatingLine = xMask.ToCharArray();
                        var b = floatingBits.Length-1;
                        for (int m = floatingLine.Length-1; m >= 0; m--)
                        {
                            if (floatingLine[m] == 'X')
                            {
                                floatingLine[m] = floatingBits.Get(b) ? '1' : '0';
                                b--;
                            }
                        }
                        masks.Add(GetBitArrayFromString(new string(floatingLine)));
                    }

                }
                else if (line.StartsWith("mem"))
                {
                    if(initialFloatingMask == null || initialOverwritingMask == null)
                        throw new FormatException("Check your file");

                    var mem = GetAddressValueFromString(line);
                    foreach (var maskBitArray in masks)
                    {
                        var addressBitArray = GetBitArrayFromInt(mem.Item1);
                        for (int m = 0; m < maskBitArray.Length; m++)
                        {
                            if (initialOverwritingMask.Get(m)) addressBitArray[m] = true;
                            if (initialFloatingMask.Get(m)) addressBitArray[m] = maskBitArray.Get(m);
                        }

                        var currentAddress = GetLongFromBitArray(addressBitArray); 
                        memoryDictionary[currentAddress] = mem.Item2;
                    }
                }
            }

            return memoryDictionary.Sum(entry => entry.Value);
        }


        private static string GetMaskFromString(string line)
        {
            return Regex.Match(line, "^mask = (?<mask>[01X]+)$").Groups["mask"].Value;
        }
        private static Tuple<int,int> GetAddressValueFromString(string line)
        {
            var match = Regex.Match(line, @"^mem\[(?<addr>\d+)\] = (?<val>\d+)$");
            return new Tuple<int, int>(int.Parse(match.Groups["addr"].Value), int.Parse(match.Groups["val"].Value));
        }

        public static BitArray GetBitArrayFromInt(int value, int? length = 36)
        {
            var initialBitArray = new BitArray(BitConverter.GetBytes(value));
            var boolArray = new bool[length ?? initialBitArray.Length];
            initialBitArray.CopyTo(boolArray, 0);
            Array.Reverse(boolArray);
            return new BitArray(boolArray);
        }

        public static BitArray GetBitArrayFromString(string value, int length = 36)
        {
            var bitArray = new BitArray(length);
            for (int i = 0; i < length; i++)
            {
                bitArray[i] = value[i] == '1';
            }
            return bitArray;
        }

        public static long GetLongFromBitArray(BitArray bitArray, int length = 36)
        {
            var boolArray = new bool[length];
            bitArray.CopyTo(boolArray, 0);
            Array.Reverse(boolArray);
            var reversedBitArray = new BitArray(boolArray);
            var byteArray = new byte[8];
            reversedBitArray.CopyTo(byteArray, 0);
            return BitConverter.ToInt64(byteArray, 0);
        }

        public static int GetIntFromBitArray(BitArray bitArray, int length = 36)
        {
            var boolArray = new bool[length];
            bitArray.CopyTo(boolArray, 0);
            Array.Reverse(boolArray);
            var reversedBitArray = new BitArray(boolArray);
            var byteArray = new byte[8];
            reversedBitArray.CopyTo(byteArray, 0);
            return BitConverter.ToInt32(byteArray, 0);
        }

        private static void PrintBitArray(BitArray bitArray)
        {
            for (int i = 0; i < bitArray.Length; i++)
            {
                var bit = bitArray.Get(i);
                Console.Write(bit ? 1 : 0);
            }
            Console.WriteLine();
        }
    }
}
