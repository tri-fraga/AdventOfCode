using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
    public class Day08 : IDay
    {
        public void Solve(string input)
        {
            Boot(input);
        }

        public void Boot(string input)
        {
            var instructionLines = Helper.LoadLines(input);

            var accumulatorInfinity = ProcessTillInfinityOrBeyond(ParseInstructions(instructionLines));
            Console.WriteLine($"The first answer is {accumulatorInfinity.Item1}");

            var accumulatorEnd = ProcessChanges(instructionLines);
            Console.WriteLine($"The second answer is {accumulatorEnd}");

        }

        private int ProcessChanges(string[] instructionLines)
        {
            for (int c = 0; c < instructionLines.Length; c++)
            {
                var modInstruction = ParseInstructions(instructionLines);

                switch (modInstruction[c].Operation)
                {
                    case OperationType.jmp:
                        modInstruction[c].Operation = OperationType.nop;
                        break;
                    case OperationType.nop:
                        modInstruction[c].Operation = OperationType.jmp;
                        break;
                }

                var process = ProcessTillInfinityOrBeyond(modInstruction);

                if (process.Item2)
                {
                    return process.Item1;
                }
                
            }

            return 0;
        }

        public Tuple<int, bool> ProcessTillInfinityOrBeyond(Instruction[] instructions)
        {
            var accumulator = 0;
            for (int i = 0; ; i++)
            {
                if (i == instructions.Length || instructions[i].Executed) 
                    return new Tuple<int, bool>(accumulator, i == instructions.Length);
                instructions[i].Executed = true;

                switch (instructions[i].Operation)
                {
                    case OperationType.acc:
                        accumulator += instructions[i].Argument;
                        break;
                    case OperationType.jmp:
                        i += instructions[i].Argument - 1;
                        break;
                    case OperationType.nop:
                    default:
                        continue;
                }
            }
        }

        private Instruction[] ParseInstructions(string[] instructions)
        {
            var parsedInstructions = new Instruction[instructions.Length];

            for (int i = 0; i < instructions.Length; i++)
            {
                var splitLine = instructions[i].Trim().Split(' ');

                var instr = splitLine[0];
                int.TryParse(splitLine[1], out int value);
                parsedInstructions[i] = new Instruction(instr, value);
            }

            return parsedInstructions;
        }
    }
}
