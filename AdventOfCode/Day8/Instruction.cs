using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public enum OperationType
    {
        acc,
        jmp,
        nop
    }
    public class Instruction
    {
        public Instruction(OperationType operation, int argument)
        {
            Operation = operation;
            Argument = argument;
            Executed = false;
        }

        public Instruction(string operation, int argument)
        {
            Enum.TryParse(operation, true, out OperationType operationType);
            Operation = operationType;
            Argument = argument;
            Executed = false;
        }

        public bool Executed { get; set; }
        public OperationType Operation { get; set; }
        public int Argument { get; }

        public override string ToString()
        {
            return $"{Operation} {Argument} [{Executed}]";
        }
    }
}
