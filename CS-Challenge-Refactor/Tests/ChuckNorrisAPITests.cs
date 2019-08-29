using CS_Challenge_Refactor.ChuckNorris;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CS_Challenge_Refactor.Tests
{
    [TestFixture]
    class ChuckNorrisAPITests
    {
        [TestCase]
        public void getCategoriesSuccessfully()
        {
            List<string> categories = ChuckNorrisAPI.getCategories();
            Assert.IsNotNull(categories);
            Assert.IsNotEmpty(categories);
        }

        [TestCase]
        public void getRandomJokeSuccessfully()
        {
            string joke = ChuckNorrisAPI.getRandomJoke();
            Assert.IsNotNull(joke);
            Assert.IsNotEmpty(joke);
            Assert.IsTrue(joke.Contains("Chuck Norris"));
        }

        [TestCase]
        public void getRandomJokeWithCategorySuccessfully()
        {
            List<string> categories = ChuckNorrisAPI.getCategories();
            string joke = ChuckNorrisAPI.getRandomJokeWithCategory(categories[0]);
            Assert.IsNotNull(joke);
            Assert.IsNotEmpty(joke);
            Assert.IsTrue(joke.Contains("Chuck Norris"));
        }

        [TestCase]
        public void getRandomJokeWithCategoryUnsuccessfully()
        {
            try
            {
                string joke = ChuckNorrisAPI.getRandomJokeWithCategory("not a valid category");
                Assert.IsNull(joke);
            }
            catch (Exception e)
            {
                Assert.IsNotNull(e);
            }
        }

        [TestCase]
        public void verifyIsCategoryProperCategorySuccessfully()
        {
            List<string> categories = ChuckNorrisAPI.getCategories();
            Assert.IsTrue(ChuckNorrisAPI.isCategory(categories[0]));
        }

        [TestCase]
        public void verifyIsCategoryNullCategoryUnsuccessfully()
        {
            Assert.IsFalse(ChuckNorrisAPI.isCategory(null));
        }

        [TestCase]
        public void verifyIsCategoryInvalidCategoryUnsuccessfully()
        {
            Assert.IsFalse(ChuckNorrisAPI.isCategory("not a category"));
        }
    }
}
