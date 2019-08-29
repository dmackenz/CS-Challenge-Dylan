using CS_Challenge_Refactor.ChuckNorris;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CS_Challenge_Refactor.Src.Jokes
{
  /// <summary>
  /// Class <c>Joke</c> is responsible for handling Chuck Norris jokes.
  /// </summary>
  class Joke
  {
    // This member is used as the raw string of the joke.
    private string joke;

    // This member is the placeholder for the name that will be substituted into the joke.
    // This value will be null unless if the substituteName method is called.
    private string name;

    /// <summary>
    /// Creates an instance of a Joke.
    /// </summary>
    private Joke(string joke)
    {
      this.joke = joke;
      this.name = null;
    }

    /// <summary>
    /// <c>create</c> Creates an instance of a Joke with no specified category.
    /// </summary>
    /// <returns>The random Joke.</returns
    public static Joke create()
    {
      return create(null);
    }

    /// <summary>
    /// <c>create</c> Creates an instance of a Joke with a specific category.
    /// </summary>
    /// <returns>The random Joke within the category.</returns>
    public static Joke create(string category)
    {
      // Create a random joke if the provided category is null.
      if (category == null)
      {
        return new Joke(ChuckNorrisAPI.getRandomJoke());
      }
      // Create a joke within a category if the category is found.
      else if (ChuckNorrisAPI.isCategory(category))
      {
        return new Joke(ChuckNorrisAPI.getRandomJokeWithCategory(category));
      }
      // throw an exception if the category is invalid.
      else
      {
        throw new Exception("Could not create a Chuck Norris joke... invalid category given.");
      }
    }

    /// <summary>
    /// <c>getCategories</c> gets the list of available categories for the jokes.
    /// </summary>
    /// <returns>The list of valid categories.</returns>
    public static List<string> getCategories()
    {
      return ChuckNorrisAPI.getCategories();
    }

    /// <summary>
    /// <c>substituteName</c> substitutes 'Chuck Norris' for a random name.
    /// </summary>
    public void substituteName()
    {
      this.name = UINamesAPI.getName();
    }

    /// <summary>
    /// <c>ToString</c> gives the joke as a string.
    /// </summary>
    /// <returns>The joke as a string.</returns>
    public override string ToString()
    {
      if (this.name != null)
      {
        // Replace "Chuck Norris" with a random name.
        return Regex.Replace(this.joke, "Chuck Norris", this.name, RegexOptions.IgnoreCase);
      }
      else
      {
        return this.joke;
      }
    }
  }
}

