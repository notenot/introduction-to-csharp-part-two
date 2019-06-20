using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
    {
        public static void RegisterTo(IVirtualMachine vm)
        {
            var bracketPairs = GetBracketPairs(vm.Instructions);
			vm.RegisterCommand('[', b =>
            {
                if (vm.Memory[vm.MemoryPointer] == 0)
                    vm.InstructionPointer = bracketPairs[vm.InstructionPointer];
            });
			vm.RegisterCommand(']', b =>
            {
                if (vm.Memory[vm.MemoryPointer] != 0)
                    vm.InstructionPointer = bracketPairs[vm.InstructionPointer];
            });
		}

        private static Dictionary<int,int> GetBracketPairs(string instructions)
        {
            var leftBracketIndices = new Stack<int>();
            var bracketPairs = new Dictionary<int, int>();
            for (var i = 0; i < instructions.Length; ++i)
            {
                if (instructions[i] == '[')
                    leftBracketIndices.Push(i);

                if (instructions[i] == ']')
                {
                    var leftBracketIndex = leftBracketIndices.Pop();
                    bracketPairs.Add(leftBracketIndex, i);
                    bracketPairs.Add(i, leftBracketIndex);
                }
            }
            return bracketPairs;
        }
    }
}