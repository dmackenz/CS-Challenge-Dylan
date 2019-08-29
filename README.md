# CS-Challenge-Dylan

The original source code can be found in CS-Challenge-Master. The new refactored code is inside of CS-Challenge-Refactor.

# Changes to the Codebase

### Created a Wrapper Class For The Chuck Norris API Integration (`Src/Jokes/ChuckNorrisAPI.cs`)

I decided that a better design approach would be to create a wrapper class that handles all of the integrations with the Chuck Norris API (`ChuckNorrisAPI`). This helps with encapsulating all of the logic involved with communicating with the Chuck Norris API into a single file.

This wrapper class is now responsible for obtaining the categories, getting a random joke and getting a random joke within a specific category. This class is also responsible for parsing the JSON responses from the Chuck Norris API into C# objects that are compatible with the other classes in the program.

```C#
ChuckNorrisAPI.getCategories()
```

One larger design change is that this class now keeps a static cache of the categories that are available by the API. The categories are not likely to change during the runtime of the program, so it makes sense to only call the API to retrieve them once and cache the results in the class for later use. When the categories are requested, if they have not yet been cached within the class, they will be loaded from the API. Every time after that, the cache of the categories is simply returned to the caller. This means that the initial call for the categories is slow because the API call needs to occur in order to load the cache, however, every subsequent call will be much faster as the API no longer needs to be invoked.

Another advantage of caching the categories is that it reduces the number of outbound API requests to the Chuck Norris API, which avoids the possibility of being rate-limited by the API which is less error-prone.

### Created a Wrapper Class For The UI Names API Integration (`Src/Jokes/UINamesAPI.cs`)

The second major change that I made was encapsulating the UI Names API integration into another wrapper class (`UINamesAPI`). This has similar benefits to the Chuck Norris API wrapper in terms of encapsulating all of the API logic into a single file and providing access modifiers around that logic.

```C#
UINamesAPI.getName()
```

Similar to the Chuck Norris API, the UI Names wrapper also keeps a cache of names that are readily available to the program. The https://uinames.com/api/ API has a request limit of 7 requests per minute. This can very easily be surpassed if a user wants to request 10 jokes all with substituted names. To counteract this issue, a large API call is made for 500 names and the names are cached within the class by calling https://uinames.com/api/?amount=500.

Every time that a random name is requested, it'll be removed from the list of 500 names that are cached from the API. This is not only avoiding the API request limit issue, but it is also much higher performance than making an API call for every name substitution. Once the 500 names are exhausted, another API call is made to retrieve a new set of 500 names.

### Creating a Joke class (`Src/Jokes/Joke.cs`)

Another change that I made was encapsulating the actual joke inside of its own object (`Joke`). This allowed for a more simplistic approach to implementing the logic in the command loop and allowed for all of the logic surrounding jokes to be unit tested from outside of the command loop. Instead of interacting with the API integrations directly, I decided that it would be better idea to only handle the `Joke` objects inside of the command loop and abstract all of the API logic within the `Joke` class. This further simplified the command loop.

### Command Line Utilities (`Src/CommandLine/CommandLineUtils.cs`)

I decided to implement a class specifically for interacting with the user input on the command line (`CommandLineUtils`). The reason for this is that there is a lot of repeated logic that could have been encapsulated inside of reusable functions in the original program for when the user is prompted for input.

There are two main helper functions for the user interacting with the command line. One is for listening for the user to type a specific character (used for control in the command loop).

```C#
CommandLineUtils.listenForCharacterInput()
```

The second command line utility function is for listening for the user to enter a specific string on the command line (used for establishing a category).

```C#
CommandLineUtils.listenForStringInput()
```

These two functions largely simplify the implementation of the command loop that runs throughout the duration of the program.

### New Look and Feel to the Command Loop (`Src/CommandLine/CommandLoop.cs`)

The command loop is now inside of its own class (`CommandLoop`). I attempted to take as much of the business logic out of the command loop as possible and simply call methods available from other classes. This made the command loop much simpler and avoided a lot of potential bugs that could be made by not implementing the command control properly.

Another major change is the introduction of enumerations for the command line control characters. There are two enums currently in the program, `MainCommand` and `YesNoCommand`, which define the possible character inputs for the command line. Introducing these enumerations allow for a less error-prone way of programming with hardcoded character control values. Instead of referencing a hardcoded character everywhere in the code, the enumerations are used instead, which reduces the likelihood of a typo/logic error in the code and also makes the code more readable.

### Unit Tests (`Tests/`)

I decided to include unit tests for testing the classes `ChuckNorrisAPI`, `UINamesAPI` and `Joke`. These three classes contain most of the logic of the program apart from the command control. These unit tests also connect to the external APIs and retrieve real information from the APIs meaning that they also test all of the API integrations. Including unit testing is valuable as it makes the code better and provides safegaurds for future alterations to the code.
