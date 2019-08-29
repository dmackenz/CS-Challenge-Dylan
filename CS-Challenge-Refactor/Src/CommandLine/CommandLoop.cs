using CS_Challenge_Refactor.ChuckNorris;
using CS_Challenge_Refactor.Src.Jokes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CS_Challenge_Refactor.Src.CommandLine
{
    class CommandLoop
    {
        /// <summary>
        /// <c>MainCommand</c> represents the characters that the user can press for the main control of the program.
        /// </summary>
        public enum MainCommand
        {
            Instructions = '?',
            Categories = 'c',
            Random = 'r',
            Quit = 'q'
        }

        /// <summary>
        /// <c>YesNoCommand</c> represents the characters that the user can press when prompted with a yes or no question.
        /// </summary>
        public enum YesNoCommand
        {
            Yes = 'y',
            No = 'n'
        }

        /// <summary>
        /// <c>getMainCommands</c> compiles a list of characters for the main commands in the program.
        /// </summary>
        private static List<char> getMainCommands()
        {
            List<char> commands = new List<char>();
            commands.Add((char) MainCommand.Instructions);
            commands.Add((char) MainCommand.Categories);
            commands.Add((char) MainCommand.Random);
            commands.Add((char) MainCommand.Quit);
            return commands;
        }

        /// <summary>
        /// <c>getYesNoCommands</c> compiles a list of characters used for a yes or no question.
        /// </summary>
        private static List<char> getYesNoCommands()
        {
            List<char> commands = new List<char>();
            commands.Add((char) YesNoCommand.Yes);
            commands.Add((char) YesNoCommand.No);
            return commands;
        }

        /// <summary>
        /// <c>startCommandLoop</c> will execute a command loop for interacting with the user.
        /// This function will be running throughout the entire duration of the program.
        /// </summary>
        public static void startCommandLoop()
        {
            Console.WriteLine("Press ? to get instructions...");
            while (true)
            {
                try
                {
                    // Retrieve the key pressed by the user for the main command loop.
                    char commandKey = CommandLineUtils.listenForCharacterInput(getMainCommands());

                    // Get instructions
                    if (commandKey == (char)MainCommand.Instructions)
                    {
                        printInstructions();
                    }
                    // List the categories available from the Chuck Norris API
                    else if (commandKey == (char)MainCommand.Categories)
                    {
                        listCategories();
                    }
                    // Start the process of telling a random joke
                    else if (commandKey == (char)MainCommand.Random)
                    {
                        startJoke();
                    }
                    // End the command loop
                    else if (commandKey == (char)MainCommand.Quit)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error during command.\n Message: {e.Message}.\nPlease try again.");
                }
            }
        }

        /// <summary>
        /// <c>printInstructions</c> lists the available commands for the user.
        /// </summary>
        private static void printInstructions()
        {
            Console.WriteLine("Here are the instructions...");
            Console.WriteLine($"--> Press {(char) MainCommand.Instructions} to get instructions");
            Console.WriteLine($"--> Press {(char) MainCommand.Categories} to get categories");
            Console.WriteLine($"--> Press {(char) MainCommand.Random} to get random jokes");
            Console.WriteLine($"--> Press {(char) MainCommand.Quit} to quit");
        }

        /// <summary>
        /// <c>listCategories</c> lists the available categories for the jokes that can
        /// be retrieved from the Chuck Norris API.
        /// </summary>
        private static void listCategories()
        {
            Console.WriteLine("Here are the categories...");
            foreach (string category in Joke.getCategories())
            {
                Console.WriteLine("--> " + category);
            }
        }

        /// <summary>
        /// <c>startJoke</c> starts the flow for getting a joke for the user.
        /// This function will also prompt the user about additional commands for 
        /// how they would like to get a joke from the API.
        /// </summary>
        private static void startJoke()
        {
            // Ask the user if they would like to substitute Chuck Norris with a random name
            Console.WriteLine("Do you want to use a random name?");
            bool includeRandomName = CommandLineUtils.listenForCharacterInput(getYesNoCommands()) == (char) YesNoCommand.Yes;

            // Ask the user if they would like to specify a category for the Chuck Norris joke
            Console.WriteLine("Do you want to specify a category?");
            bool includeCategory = CommandLineUtils.listenForCharacterInput(getYesNoCommands()) == (char) YesNoCommand.Yes;

            // If the user wants to include a category, get the specific category from the user
            string category = null;
            if (includeCategory)
            {
                Console.WriteLine("Enter a category: ");
                category = CommandLineUtils.listenForStringInput(Joke.getCategories());
                Console.WriteLine(category);
            }

            // Ask the user how many jokes that they want to retrieve from the API
            Console.WriteLine("How many jokes do you want? (1-9)");
            int numberOfJokes = CommandLineUtils.listenForSingleDigitValue();

            for (int i = 0; i < numberOfJokes; i++)
            {
                // Create a new joke... note that if the category is null, a regular random joke will be made.
                Joke joke = Joke.create(category);

                // Substitute Chuck Norris for random name, if requested
                if (includeRandomName)
                {
                    joke.substituteName();
                }

                // Print the final joke to the console
                Console.WriteLine("\n" + joke.ToString() + "\n");
            }
        }
    }
}
