using System;
using NUnit.Framework;
using PayByPhoneTwitterFeed.DataAccess;
using Rhino.Mocks;

namespace PayByPhoneTwitterFeed.DataAccess_IntegrationTests
{
    public class RestClient_Tests
    {
        [TestFixture]
        public class When_I_call_a_twitter_api_uri_with_the_RestClient : Given_a_RestClient
        {
            private const string Uri = "http://search.twitter.com/search.json?q=alexjohnmartin";
            private string _result;

            [SetUp]
            public new void Setup()
            {
                _result = restClient.GetResponse(Uri); 
            }

            [Test]
            public void It_should_return_data()
            {
                Assert.That(_result.Contains("results"));
                Console.WriteLine(_result); 
            }
        }
    }

    public class Given_a_RestClient
    {
        protected RestClient restClient;
        private IOAuthConfiguration oAuthConfiguration;

        [SetUp]
        public void Setup()
        {
            oAuthConfiguration = MockRepository.GenerateStub<IOAuthConfiguration>();

            restClient = new RestClient(oAuthConfiguration);
        }
    }
}
