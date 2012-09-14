using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PayByPhoneTwitterFeed.DataAccess;
using PayByPhoneTwitterFeed.Model;
using Rhino.Mocks;

namespace PayByPhoneTwitterFeed.DataAccess_Tests
{
    public class TwitterSearcher_Tests
    {
        [TestFixture]
        public class When_searching_for_tweets : Given_a_searcher
        {
            private IEnumerable<Tweet> _result;
            private const string SearchTerm = "testing123";
            private const string TwitterSearchUri = "http://search.twitter.com/search.json?q=";

            [SetUp]
            public new void Setup()
            {
                _result = Searcher.Search(SearchTerm);
            }

            [Test]
            public void It_should_call_the_uri_with_the_search_term()
            {
                RestClient.AssertWasCalled(r => r.GetResponse(TwitterSearchUri + SearchTerm));
            }

            [Test]
            public void It_should_return_the_parsed_tweets()
            {
                Assert.That(_result.Count(), Is.GreaterThan(0));
            }
        }
    }

    public class Given_a_searcher
    {
        protected ITwitterSearcher Searcher;
        protected IJsonParser JsonParser;
        protected IRestClient RestClient;
        private string _jsonData = "test";

        [SetUp]
        public void Setup()
        {
            RestClient = MockRepository.GenerateMock<IRestClient>();
            RestClient.Stub(r => r.GetResponse(string.Empty)).IgnoreArguments().Return(_jsonData); 

            JsonParser = MockRepository.GenerateStub<IJsonParser>();
            JsonParser.Stub(j => j.Parse(_jsonData)).Return(new List<Tweet> {new Tweet()});
            
            Searcher = new TwitterSearcher(RestClient, JsonParser);
        }
    }
}
