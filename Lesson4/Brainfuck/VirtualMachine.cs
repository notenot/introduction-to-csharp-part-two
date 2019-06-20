using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
    {
        public string Instructions { get; }
        public int InstructionPointer { get; set; }
        public byte[] Memory { get; }
        public int MemoryPointer { get; set; }

        private int memorySize;
        private Dictionary<char, Action<IVirtualMachine>> actions;

		public VirtualMachine(string program, int memorySize = 30000)
        {
            MemoryPointer = 0;
            InstructionPointer = 0;
            this.memorySize = memorySize;
            Memory = new byte[memorySize];
            Instructions = program;
            actions = new Dictionary<char, Action<IVirtualMachine>>();
        }

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
            if (!actions.ContainsKey(symbol))
			    actions.Add(symbol, execute);
		}

		public void Run()
        {
            while (InstructionPointer < Instructions.Length)
            {
                var command = Instructions[InstructionPointer];
                if (actions.ContainsKey(command))
                    actions[command](this);
                ++InstructionPointer;
            }
        }
	}
}