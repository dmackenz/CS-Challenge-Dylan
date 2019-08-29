using CS_Challenge_Refactor.ChuckNorris;
using CS_Challenge_Refactor.Src.CommandLine;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CS_Challenge_Refactor
{
  /// <summary>
  /// Class <c>Program</c> is the main entry point for running the program.
  /// This program is a command line utility for requesting random Chuck Norris jokes.
  /// This program also allows you to substitute the name Chuck Norris in the joke with
  /// a random name.
  /// </summary>
  class Program
  {
    /// <summary>
    /// <c>Main</c> runs the program.
    /// </summary>
    static void Main(string[] args)
    {

      CommandLoop.startCommandLoop();
    }
  }
}
