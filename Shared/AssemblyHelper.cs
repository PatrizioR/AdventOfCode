using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public static class AssemblyHelper
    {
        public static int CountAccBeforeRepeat(string input)
        {
            var instructions = Instructions(Map(input));
            return ProcessAcc(instructions, new List<Instruction>(), 0, null);
        }

        public static int CountAccAfterFix(string input)
        {
            var instructions = Instructions(Map(input));
            try
            {
                foreach(var instruction in instructions.Where(inst => inst.Op != Operation.acc))
                {
                    var oldOp = instruction.Op;
                    instruction.Op = instruction.Op == Operation.jmp ? Operation.nop : Operation.jmp;
                    ProcessAcc(instructions, new List<Instruction>(), 0, null);
                    instruction.Op = oldOp;
                }

            }catch(ArgumentOutOfRangeException aofre)
            {
                return (int)aofre.ActualValue;
            }

            return -1;
        }

        private static List<string> Map(string input)
        {
            return input.Split("\n").Select(item => item?.Trim()).Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
        }

        private static List<Instruction> Instructions(List<string> map)
        {
            List<Instruction> list = new List<Instruction>();
            for(int i = 0; i < map.Count(); i++)
            {
                list.Add(new Instruction(i + 1, map[i]));
            }
            return list;
        }

        public static int ProcessAcc(List<Instruction> instructions, List<Instruction> executedInstructions, int acc, Instruction currentInstruction)
        {
            if(currentInstruction == null)
            {
                // start
                return ProcessAcc(instructions, executedInstructions, acc, instructions.First());
            }

            if (executedInstructions.Contains(currentInstruction))
            {
                // end
                return acc;
            }

            int nextLine = currentInstruction.Line + 1;

            switch (currentInstruction.Op)
            {
                case Operation.acc:
                    acc += currentInstruction.Command;
                    break;
                case Operation.jmp:
                    nextLine = currentInstruction.Line + currentInstruction.Command;
                    break;
                case Operation.nop:
                    break;
            }

            executedInstructions.Add(currentInstruction);
            var nextInstruction = instructions.FirstOrDefault(instruction => instruction.Line == nextLine);
            if(nextInstruction == null)
            {
                throw new ArgumentOutOfRangeException(nameof(nextInstruction), acc, $"Line is invalid {nextLine}");
            }
            return ProcessAcc(instructions, executedInstructions, acc, nextInstruction);
        }

        public enum Operation
        {
            nop,
            acc,
            jmp
        }

        public class Instruction
        {
            public Instruction(int line, string input)
            {
                Line = line;
                var split = input.Split(" ");
                Op = (Operation)(Enum.Parse(typeof(Operation), split[0]));
                Command = int.Parse(split[1]);
            }

            public int Line { get; private set; }
            public Operation Op { get; set; }
            public int Command { get; set; }
        }
    }
}
