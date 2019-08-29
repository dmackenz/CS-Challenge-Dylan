using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace CS_Challenge_Refactor.ChuckNorris
{
  /// <summary>
  /// Class <c>ChuckNorrisAPI</c> is responsible for interacting with the Chuck Norris API.
  /// This class loads random jokes from the Chuck Norris API and returns them to the caller.
  /// </summary>
  class ChuckNorrisAPI
  {
    // Base API url for getting Chuck Norris jokes
    private static readonly string CHUCK_NORIS_URL = "https://api.chucknorris.io";

    // API endpoint for getting a list o available joke categories 
    private static readonly string CATEGORIES_ENDPOINT = "/jokes/categories";

    // API endpoint for getting a random joke
    private static readonly string JOKES_ENDPOINT = "/jokes/random";

    // Used to store the categories retrieved from the API
    private static bool areCategoriesRetrieved = false;
    private static List<string> retrievedCategories = new List<string>();

    // Non-instantiable class
    private ChuckNorrisAPI() { }

    /// <summary>
    /// <c>getJson</c> loads a json response from an HTTP GET endpoint.
    /// </summary>
    /// <param name="url">The url of the HTTP GET endpoint.</param>
    /// <returns>The JSON response as a string.</returns>
    private static string getJson(string url)
    {
      // Create the HTTP client
      using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
      {
        // Invoke the endpoint and ensure the correct status code
        client.BaseAddress = new Uri(url);
        HttpResponseMessage response = client.GetAsync("").Result;
        response.EnsureSuccessStatusCode();

        // Return the result from the API as a string
        return response.Content.ReadAsStringAsync().Result;
      }
    }

    /// <summary>
    /// <c>getCategories</c> gets the list of available categories
    /// for random jokes from the Chuck Norris API.
    /// </summary>
    /// <returns>A List of categories as strings.</returns>
    public static List<string> getCategories()
    {
      // Only call the API for categories once
      // Note that the if statement will only execute once throughout the runtime of the program
      if (!areCategoriesRetrieved)
      {
        // Call the API to get the list of joke categories
        string jsonResponse = getJson(CHUCK_NORIS_URL + CATEGORIES_ENDPOINT);

        // Parse the categories and cache them in the class
        retrievedCategories = new List<string>(JsonConvert.DeserializeObject<string[]>(jsonResponse));
        for (int i = 0; i < retrievedCategories.Count; i++)
        {
          retrievedCategories[i] = normalizeCategory(retrievedCategories[i]);
        }
        areCategoriesRetrieved = true;
      }

      return retrievedCategories;
    }

    /// <summary>
    /// <c>callForJoke</c> calls the Chuck Norris API for a joke.
    /// </summary>
    /// <param name="url">The url of for retrieving the joke.</param>
    /// <returns>The joke from the API as a string.</returns>
    private static string callForJoke(string url)
    {
      // Call the joke API
      string jsonString = getJson(url);

      // Parse the joke from the API response and return it as a string
      dynamic joke = JsonConvert.DeserializeObject<dynamic>(jsonString);
      return (string)joke.value;
    }

    /// <summary>
    /// <c>getRandomJoke</c> gets a random joke from the Chuck Norris API.
    /// </summary>
    /// <returns>The random joke as a string.</returns>
    public static string getRandomJoke()
    {
      return callForJoke(CHUCK_NORIS_URL + JOKES_ENDPOINT);
    }

    /// <summary>
    /// <c>getRandomJokeWithCategory</c> gets a random joke from the
    /// Chuck Norris API for a specific category.
    /// </summary>
    /// <returns>The random joke from the category as a string.</returns>
    public static string getRandomJokeWithCategory(string category)
    {
      if (!isCategory(normalizeCategory(category)))
      {
        throw new Exception("Did not provide a proper category when retrieving Chuck Norris joke.");
      }
      return callForJoke(CHUCK_NORIS_URL + JOKES_ENDPOINT + "?category=" + normalizeCategory(category));
    }

    /// <summary>
    /// <c>normalizeCategory</c> normalizes a string that represents a category.
    /// This is used so that all of the categories that are cached adhere to the same format
    /// so that they can easily be compared.
    /// </summary>
    /// <returns>The string representing the category after it has been normalized.</returns>
    private static string normalizeCategory(string category)
    {
      return category.ToLower().Trim();
    }

    /// <summary>
    /// <c>isCategory</c> determines whether a string is in the list of available categories.
    /// </summary>
    /// <returns>true if it is in the list, false if not.</returns>
    public static bool isCategory(string category)
    {
      if (category == null)
      {
        return false;
      }
      List<string> categories = getCategories();
      return categories.Contains(normalizeCategory(category));
    }
  }
}
