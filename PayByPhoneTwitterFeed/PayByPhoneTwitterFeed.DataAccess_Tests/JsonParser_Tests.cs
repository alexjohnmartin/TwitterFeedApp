using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PayByPhoneTwitterFeed.DataAccess;
using PayByPhoneTwitterFeed.Model;
using Rhino.Mocks;

namespace PayByPhoneTwitterFeed.DataAccess_Tests
{
    public class JsonParser_Tests
    {
        [TestFixture]
        public class When_I_parse_valid_json_data : Given_a_parser
        {
            private IEnumerable<Tweet> _result;

            [SetUp]
            public new void Setup()
            {
                _result = Parser.Parse(ValidJson); 
            }

            [Test]
            public void It_should_extract_at_least_1_tweet()
            {
                Assert.That(_result.Count(), Is.GreaterThan(0));
            }

            [Test]
            public void It_should_parse_the_first_tweet_correctly()
            {
                var tweet = _result.First();
                Assert.That(tweet.Author, Is.EqualTo(Author));
                Assert.That(tweet.Content, Is.EqualTo(Content));
                Assert.That(tweet.Created.Date, Is.EqualTo(_created.Date));
            }
        }
    }

    public class Given_a_parser
    {
        private const string TwitterJsonTemplate = @"""created_at"":""{2}"",""from_user"":""{0}"",""text"":""{1}""";
        protected const string Author = "testAuthor";
        protected const string Content = "this is a test tweet";
        protected readonly DateTime _created = DateTime.Now;
        protected JsonParser Parser;
        protected string ValidJson; 

        [SetUp]
        public void Setup()
        {
            ValidJson = String.Format(TwitterJsonTemplate, Author, Content, _created); 

            var valueExtractor = MockRepository.GenerateStub<IJsonValueExtractor>();
            valueExtractor.Stub(v => v.ExtractValueFromJson(ValidJson, "created_at")).Return(_created.ToString());
            valueExtractor.Stub(v => v.ExtractValueFromJson(ValidJson, "from_user")).Return(Author);
            valueExtractor.Stub(v => v.ExtractValueFromJson(ValidJson, "text")).Return(Content);

            Parser = new JsonParser(valueExtractor);
        }
    }
}