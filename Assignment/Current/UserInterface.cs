using System;
using System.Text;
using System.Collections.Generic;

namespace CAB201
{

    /// <summary>
    /// A collection of static utility methods which implement a simple text-based
    /// user interface.
    /// </summary>
    public class UserInterface
    {

        /// <summary>
        /// Displays a menu, with the options numbered from 1 to options.Length,
        /// the gets a validated integer in the range 1..options.Length. 
        /// Subtracts 1, then returns the result. If the supplied list of options 
        /// is empty, returns an error value (-1).
        /// </summary>
        /// <param name="title">A heading to display before the menu is listed.</param>
        /// <param name="options">The list of objects to be displayed.</param>
        /// <returns>Return value is either -1 (if no options are provided) or a 
        /// value in 0 .. (options.Length-1).</returns>

        public static int GetOption(string title, params object[] options)
        {
            if (options.Length == 0)
            {
                return -1;
            }

            int digitsNeeded = (int)(1 + Math.Floor(Math.Log10(options.Length)));

            Console.WriteLine(title);

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{(i + 1).ToString().PadLeft(digitsNeeded)} {options[i]}");
            }

            Console.WriteLine();

            int option = GetInt($"Please enter a choice between 1 and {options.Length}", 1, options.Length);

            return option - 1;

        }

        public static currentList ChooseFromList<currentList>(IList<currentList> list)
        {
            if (!(list.Count > 0))
            {
                UserInterface.Error("No properties available at the current time");
                return default(currentList);
            }

            Console.WriteLine();

            DisplayOption("Please choose one of the following:", list);
            var option = UserInterface.chooseOption(1, list.Count);
            return list[option];
        }

        public static void DisplayOption<currentList>(string title, IList<currentList> list, string failMsg = "None")
        {
            Console.WriteLine(title);
            if (list.Count == 0)
            {

                Console.WriteLine($"{failMsg}");
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine("{0}) {1}", i + 1, list[i].ToString());
            }
            Console.WriteLine();
        }

        public static int chooseOption(int min, int max)
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                var option = key.KeyChar - '0';
                if (min <= option && option <= max)
                {
                    return option - 1;
                }
                else
                {
                    Message("Invalid option");
                }
            }
        }

        /// <summary>
        /// Gets a validated integer between the designated lower and upper bounds.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <param name="min">The lower bound.</param>
        /// <param name="max">The upper bound.</param>
        /// <returns>A value x such that lowerBound <= x <= upperBound.</returns>

        public static int GetInt(string prompt, int min, int max)
        {
            if (min > max)
            {
                int t = min;
                min = max;
                max = t;
            }

            while (true)
            {
                int result = GetInt(prompt);

                if (min <= result && result <= max)
                {
                    return result;
                }
                else
                {
                    Error("Supplied value is out of range");
                }
            }
        }

        /// <summary>
        /// Gets a validated integer.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <returns>An integer supplied by the user.</returns>
        public static int GetInt(string prompt)
        {
            while (true)
            {
                string response = GetInput(prompt);

                int result;

                if (int.TryParse(response, out result))
                {
                    return result;
                }
                else
                {
                    Error("Supplied value is not an integer");
                }
            }
        }

        /// <summary>
        /// Gets a validated floating point between the designated lower and upper bounds.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <param name="min">The lower bound.</param>
        /// <param name="max">The upper bound.</param>
        /// <returns>A value x such that lowerBound <= x <= upperBound.</returns>
        public static double GetDouble(string prompt, double min, double max)
        {
            if (min > max)
            {
                double t = min;
                min = max;
                max = t;
            }

            while (true)
            {
                double result = GetDouble(prompt);

                if (min <= result && result <= max)
                {
                    return result;
                }
                else
                {
                    Error("Supplied value is out of range");
                }
            }
        }

        /// <summary>
        /// Gets a validated floating point value.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <returns>A floating point value supplied by the user.</returns>
        public static double GetDouble(string prompt)
        {
            while (true)
            {
                string response = GetInput(prompt);

                double result;

                if (double.TryParse(response, out result))
                {
                    return result;
                }
                else
                {
                    Error("Supplied value is not numeric");
                }
            }
        }


        /// <summary>
        /// Gets a validated Boolean value.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <returns>A Boolean value supplied by the user.</returns>
        public static bool GetBool(string prompt)
        {
            while (true)
            {
                string response = GetInput(prompt);

                bool result;

                if (bool.TryParse(response, out result))
                {
                    return result;
                }
                else
                {
                    Error("Supplied value is not a boolean");
                }
            }
        }

        /// <summary>
        /// Gets a single line of text from user.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <returns>Returns a single line of text from user.</returns>
        public static string GetInput(string prompt)
        {
            Console.WriteLine("{0}:", prompt);
            return Console.ReadLine();
        }

        /// <summary>
        /// Gets a single line of text from user. The text is processed one
        /// character at a time, and the input is concealed.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <returns>Returns a single line of text from user.</returns>
        public static string GetPassword(string prompt)
        {
            Console.WriteLine("{0}:", prompt);
            StringBuilder password = new System.Text.StringBuilder();

            while (true)
            {
                var keyInfo = Console.ReadKey(intercept: true);
                var key = keyInfo.Key;

                if (key == ConsoleKey.Enter)
                    break;
                else if (key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        Console.Write("\b \b");
                        password.Remove(password.Length - 1, 1);
                    }
                }
                else
                {
                    Console.Write("*");
                    password.Append(keyInfo.KeyChar);
                }
            }

            Console.WriteLine();
            return password.ToString();
        }

        /// <summary>
        /// Displays an error message and asks user to try again.
        /// </summary>
        /// <param name="msg">The message to display</param>
        public static void Error(string msg)
        {
            Console.WriteLine($"{msg}, please try again");
            Console.WriteLine();
        }

        /// <summary>
        /// Displays the content of an arbitrary object.
        /// </summary>
        /// <param name="msg">The object to display</param>
        public static void Message(object msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine();
        }

        internal static void Message(string v, string userName)
        {
            throw new NotImplementedException();
        }
    }
}