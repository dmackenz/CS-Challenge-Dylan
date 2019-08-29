using System;
using System.Collections.Generic;
using System.Linq;

namespace CS_Challenge_Refactor
{
  /// <summary>
  /// Class <c>CommandLineUtils</c> is responsible for providing utility functions for the command line.
  /// </summary>
  class CommandLineUtils
  {
    /// <summary>
    /// <c>listenForCharacterInput</c> pauses the execution of the program and asks for user input
    /// on the command line for a specific key to be pressed on the keyboard.
    /// Note that if a key is pressed that is not in the <c>expectedKeys</c> List,
    /// the user will be prompted again until they enter a valid input.
    /// </summary>
    /// <param name="expectedKeys">A List of the keys to listen for on the keyboard.</param>
    /// <returns>The character that the user pressed in the expected list.</returns>
    public static char listenForCharacterInput(List<char> expectedKeys)
    {
      bool isCorrectKeyEntered = false;
      char enteredKey = '\0';

      // Print the available options to the user
      // These are the keys that the user can be press to control the logic flow
      string toPrint = "(";
      for (int i = 0; i < expectedKeys.Count; i++)
      {
        if (i != 0)
        {
          toPrint += "/";
        }
        toPrint += expectedKeys.ElementAt(i);
      }
      toPrint += ")";

      // Loop until the user presses one of the keys that are expected
      while (!isCorrectKeyEntered)
      {
        // Get key pressed from user
        Console.WriteLine(toPrint);
        enteredKey = Console.ReadKey().KeyChar;

        // Determine if the key pressed was in the list of expected keys
        int idx = expectedKeys.IndexOf(enteredKey);
        if (idx != -1)
        {
          Console.WriteLine();
          isCorrectKeyEntered = true;
        }
        else
        {
          Console.WriteLine("\nWrong key entered. Please try again...");
        }
      }

      // Return the key that the user pressed from the expected list
      return enteredKey;
    }

    /// <summary>
    /// <c>listenForStringInput</c> pauses the execution of the program and asks for user input
    /// on the command line in the form of a string.
    /// Note that if a string is entered that is not in the <c>expectedStrings</c> List,
    /// the user will be prompted again until they enter a valid string.
    /// </summary>
    /// <param name="expectedStrings">A List of the strings to listen for from the user input.</param>
    /// <returns>The string that the user entered in the expected list.</returns>
    public static string listenForStringInput(List<string> expectedStrings)
    {
      bool isCorrectStringEntered = false;
      string enteredString = "";

      // Loop until one of the expected strings is entered
      while (!isCorrectStringEntered)
      {
        // Get the string entered by the user
        enteredString = Console.ReadLine().ToLower().Trim();

        // Determine if the string is one of the expected options
        int idx = expectedStrings.IndexOf(enteredString);
        if (idx != -1)
        {
          Console.WriteLine();
          isCorrectStringEntered = true;
        }
        else
        {
          Console.WriteLine("\nUnexpected string entered. Please try again...");
        }
      }

      // Return the string that the user entered from the expected list
      return enteredString;
    }

    public static int listenForSingleDigitValue()
    {
      return int.Parse(CommandLineUtils.listenForCharacterInput(new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9' }).ToString());
    }
  }
}
