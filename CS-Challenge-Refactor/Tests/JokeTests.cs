using System;
using System.Collections.Generic;
using CS_Challenge_Refactor.Src.Jokes;
using NUnit.Framework;

namespace CS_Challenge_Refactor.Tests
{
    [TestFixture]
    public class JokeUnitTests
    {
        [TestCase]
        public void createARandomJokeSuccessfully()
        {
            Joke joke = Joke.create();
            Assert.IsNotNull(joke);
            Assert.IsNotEmpty(joke.ToString());
            Assert.IsTrue(joke.ToString().Contains("Chuck Norris"));
        }

        [Test]
        public void createARandomJokeAndSubstituteNameSuccessfully()
        {
            Joke joke = Joke.create();
            joke.substituteName();
            Assert.IsNotNull(joke);
            Assert.IsNotEmpty(joke.ToString());
            Assert.IsFalse(joke.ToString().Contains("Chuck Norris"));
        }

        [Test]
        public void createARandomJokeForACategorySuccessfully()
        {
            Joke joke = Joke.create("travel");
            Assert.IsNotNull(joke);
            Assert.IsNotEmpty(joke.ToString());
            Assert.IsTrue(joke.ToString().Contains("Chuck Norris"));
        }

        [Test]
        public void createARandomJokeForANullCategorySuccessfully()
        {
            Joke joke = Joke.create(null);
            Assert.IsNotNull(joke);
            Assert.IsNotEmpty(joke.ToString());
            Assert.IsTrue(joke.ToString().Contains("Chuck Norris"));
        }

        [Test]
        public void createARandomJokeForAnInvalidCategoryUnsuccessfully()
        {
            try
            {
                Joke joke = Joke.create("not a category");
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsNotNull(e);
            }
        }

        [Test]
        public void getListOfCategoriesSuccessfully()
        {
            List<string> categories = Joke.getCategories();
            Assert.NotNull(categories);
            Assert.True(categories.Count > 0);
        }
    }
}
