using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace CS_Challenge_Refactor
{
  /// <summary>
  /// Class <c>UINamesAPI</c> is responsible for interacting with the UI Names API.
  /// The main purpose of this class is to load random names from the API and store them
  /// so that they are available to substitute the name "Chuck Norris" in the jokes.
  /// </summary>
  class UINamesAPI
  {
    // Base URL for hitting the names API
    private static readonly string NAMES_URL = "http://uinames.com";
    
    // Endpoint that provides 500 random names of Canadian origin
    private static readonly string NAMES_ENDPOINT = "/api/?amount=500&region=canada";

    // Used for "caching" names received from the API
    private static List<string> retrievedNames = new List<string>();

    // Non-instantiable class
    private UINamesAPI() { }

    /// <summary>
    /// <c>loadNames</c> is responsible for loading the names from the API into the class.
    /// </summary>
    private static void loadNames()
    {
      // Initialize HTTP client
      using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
      {
        // Invoke endpoint
        client.BaseAddress = new Uri(NAMES_URL + NAMES_ENDPOINT);
        HttpResponseMessage response = client.GetAsync("").Result;
        response.EnsureSuccessStatusCode();
        
        // Decode response from API
        string jsonString = response.Content.ReadAsStringAsync().Result;
        dynamic[] names = JsonConvert.DeserializeObject<dynamic[]>(jsonString);
        
        // Cache each name retrieved from the API in a list
        foreach (var name in names)
        {
          retrievedNames.Add((string) (name.name + " " + name.surname));
        }
      }
    }

    /// <summary>
    /// <c>getName</c> is the function that a user can call in order to retrieve a random name.
    /// This method will automatically reload names into the cache if it has been depleted.
    /// </summary>
    /// <returns>A random full name (first and last) as a string.</returns>
    public static string getName()
    {
      // If the cache of names is empty, or has yet to be loaded, get names from the API
      if (retrievedNames.Count == 0)
      {
        loadNames();
      }

      // Remove the last name from the list and return it
      string name = retrievedNames.Last();
      retrievedNames.RemoveAt(retrievedNames.Count - 1);
      return name;
    }
  }
}
