using System;

namespace Brainfuck
{
    class Brainfuck
    {
        private static int pointer = 0;
        private static byte[] memory = new byte[30000];

        internal static void Run(string input)
        {
            for (int pos = 0; pos < input.Length; pos++)
            {
                char c = input[pos];
                switch (c)
                {
                    case '>':
                        pointer++;
                        break;
                    case '<':
                        pointer--;
                        break;
                    case '+':
                        memory[pointer]++;
                        break;
                    case '-':
                        memory[pointer]--;
                        break;
                    case '.':
                        WriteByte(memory[pointer]);
                        break;
                    case ',':
                        memory[pointer] = ReadByte();
                        break;
                    case '[':
                        pos = MoveForward(pos, input);
                        break;
                    case ']':
                        pos = MoveBack(pos, input);
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine($"Invalid command: {c}");
                        break;
                }
            }
        }

        private static int MoveBack(int currentPostion, string input)
        {
            if (memory[pointer] != 0)
            {
                int nrOfClosedBrackets = 0;
                for (int i = currentPostion - 1; i >= 0; i--)
                {
                    char bla = input[i];
                    if (input[i].Equals(']'))
                        nrOfClosedBrackets++;
                    else if (input[i].Equals('['))
                        if (nrOfClosedBrackets == 0)
                            return i; // No need to add one because the main loop takes care of that.
                        else
                            nrOfClosedBrackets--;
                }
            }
            return currentPostion;
        }

        private static int MoveForward(int currentPostion, string input)
        {
            if (memory[pointer] == 0)
            {
                int nrOfOpenBrackets = 0;
                for (int i = currentPostion + 1; i < input.Length; i++)
                {
                    if (input[i].Equals('[')) nrOfOpenBrackets++;
                    else if (input[i].Equals(']'))
                    {
                        if (nrOfOpenBrackets == 0)
                            return currentPostion + i + 1; // We jump to the command after the ]
                        else
                            nrOfOpenBrackets--;
                    }
                }
            }
            return currentPostion;
        }

        private static byte ReadByte()
        {
            return (byte)Console.Read();
        }

        private static void WriteByte(byte v)
        {
            Console.Write((char)v);
        }
    }
}
