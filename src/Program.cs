using System;
using IronSoftware.PhoneKeypad;

namespace IronSoftware.PhoneKeypad.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Old Phone Keypad Decoder");
            Console.WriteLine("========================\n");

            RunExamples();

            if (args.Length > 0)
            {
                Console.WriteLine("\nProcessing command-line argument:");
                ProcessInput(args[0]);
            }
            else
            {
                Console.WriteLine("\nInteractive Mode (type 'exit' to quit):");
                InteractiveMode();
            }
        }

        static void RunExamples()
        {
            Console.WriteLine("Example Tests:");
            TestCase("33#", "E");
            TestCase("227*#", "B");
            TestCase("4433555 555666#", "HELLO");
            TestCase("8 88777444666*664#", "TURING");
            Console.WriteLine();
        }

        static void TestCase(string input, string expected)
        {
            try
            {
                string result = OldPhonePad.Decode(input);
                bool passed = result == expected;
                string status = passed ? "✓ PASS" : "✗ FAIL";

                Console.WriteLine($"{status} | Input: {input,-25} => Expected: {expected,-10} Got: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ ERROR | Input: {input,-25} => {ex.Message}");
            }
        }

        static void InteractiveMode()
        {
            while (true)
            {
                Console.Write("Enter input (or 'exit'): ");
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || input.ToLower() == "exit")
                    break;

                ProcessInput(input);
            }
        }

        static void ProcessInput(string input)
        {
            try
            {
                string result = OldPhonePad.Decode(input);
                Console.WriteLine($"Output: {result}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }
        }
    }
}
