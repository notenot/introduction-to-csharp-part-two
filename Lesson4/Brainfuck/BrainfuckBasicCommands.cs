using System;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b => 
                { write(Convert.ToChar(b.Memory[b.MemoryPointer])); });
			vm.RegisterCommand('+', b => 
                { unchecked { ++b.Memory[b.MemoryPointer]; } });
            vm.RegisterCommand('-', b => 
                { unchecked { --b.Memory[b.MemoryPointer]; } });
            vm.RegisterCommand(',', b => 
                { b.Memory[b.MemoryPointer] = Convert.ToByte(read()); });
            vm.RegisterCommand('>', b => 
                { b.MemoryPointer = (b.MemoryPointer + 1) % b.Memory.Length; });
            vm.RegisterCommand('<', b => 
                { b.MemoryPointer = (b.MemoryPointer + b.Memory.Length - 1) % b.Memory.Length; });
            RegisterConstantCommands(vm);
        }

        private static void RegisterConstantCommands(IVirtualMachine vm)
        {
            for (var ch = 'a'; ch <= 'z'; ++ch)
            {
                var lowercase = ch;
                vm.RegisterCommand(lowercase, b =>
                    { b.Memory[b.MemoryPointer] = Convert.ToByte(lowercase); });
                var uppercase = char.ToUpper(ch);
                vm.RegisterCommand(uppercase, b =>
                    { b.Memory[b.MemoryPointer] = Convert.ToByte(uppercase); });
            }
            for (var ch = '0'; ch <= '9'; ++ch)
            {
                var number = ch;
                vm.RegisterCommand(number, b =>
                    { b.Memory[b.MemoryPointer] = Convert.ToByte(number); });
            }
        }
	}
}