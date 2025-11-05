using System;
using System.Collections.Generic;
using System.Text;

namespace IronSoftware.PhoneKeypad
{
    /// <summary>
    /// Provides functionality to decode old phone keypad input into text.
    /// </summary>
    public static class OldPhonePad
    {
        private static readonly Dictionary<char, string> Keypad = new Dictionary<char, string>
        {
            { '0', " " },
            { '1', "&'(" },
            { '2', "ABC" },
            { '3', "DEF" },
            { '4', "GHI" },
            { '5', "JKL" },
            { '6', "MNO" },
            { '7', "PQRS" },
            { '8', "TUV" },
            { '9', "WXYZ" }
        };

        private const char Send = '#';
        private const char Backspace = '*';
        private const char Pause = ' ';

        /// <summary>
        /// Converts old phone keypad input into the corresponding text output.
        /// </summary>
        /// <param name="input">The keypad input string ending with '#'</param>
        /// <returns>The decoded text string</returns>
        /// <exception cref="ArgumentNullException">Thrown when input is null</exception>
        /// <exception cref="ArgumentException">Thrown when input is empty or doesn't end with '#'</exception>
        /// <remarks>
        /// Rules:
        /// - Press a number key multiple times to cycle through its letters (e.g., "222" = "C")
        /// - Use space ' ' to pause between same-key characters (e.g., "2 2" = "AA")
        /// - Use '*' to backspace/delete the last character
        /// - Use '#' to send/complete the message
        /// </remarks>
        public static string Decode(string input)
        {
            ValidateInput(input);

            var result = new StringBuilder();
            var currentGroup = string.Empty;

            foreach (char c in input)
            {
                if (c == Send)
                {
                    AppendGroupToResult(currentGroup, result);
                    break;
                }
                else if (c == Backspace)
                {
                    AppendGroupToResult(currentGroup, result);
                    currentGroup = string.Empty;
                    DeleteLastCharacter(result);
                }
                else if (c == Pause)
                {
                    AppendGroupToResult(currentGroup, result);
                    currentGroup = string.Empty;
                }
                else if (char.IsDigit(c))
                {
                    if (currentGroup.Length > 0 && currentGroup[0] != c)
                    {
                        AppendGroupToResult(currentGroup, result);
                        currentGroup = string.Empty;
                    }
                    currentGroup += c;
                }
                else
                {
                    throw new ArgumentException($"Invalid character '{c}' in input. Only digits, space, '*', and '#' are allowed.");
                }
            }

            return result.ToString();
        }

        private static void ValidateInput(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null");

            if (input.Length == 0)
                throw new ArgumentException("Input cannot be empty", nameof(input));

            if (!input.EndsWith(Send.ToString()))
                throw new ArgumentException($"Input must end with '{Send}'", nameof(input));
        }

        private static void AppendGroupToResult(string group, StringBuilder result)
        {
            if (group.Length == 0)
                return;

            char key = group[0];

            if (!Keypad.ContainsKey(key))
                return;

            string letters = Keypad[key];
            int index = (group.Length - 1) % letters.Length;
            result.Append(letters[index]);
        }

        private static void DeleteLastCharacter(StringBuilder result)
        {
            if (result.Length > 0)
                result.Length--;
        }
    }
}
