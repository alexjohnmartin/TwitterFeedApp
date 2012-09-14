using NUnit.Framework;
using PayByPhoneTwitterFeed.DataAccess;

namespace PayByPhoneTwitterFeed.DataAccess_Tests
{
    public class JsonValueExtractor_Tests
    {
        [TestFixture]
        public class When_parsing_json_data : Given_an_extractor
        {
            private const string Field = "from_user";
            private const string JsonData = @"""created_at"":""Sat, 08 Sep 2012 04:58:16 +0000"",""from_user"":""AlexJohnMartin"",""text"":""THERE'S NANDOS IN VANCOUVER! I'm home.""";
            private string _result;

            [SetUp]
            public new void Setup()
            {
                _result = Extractor.ExtractValueFromJson(JsonData, Field); 
            }

            [Test]
            public void It_should_extract_the_correct_value()
            {
                Assert.That(_result, Is.EqualTo("AlexJohnMartin"));
            }
        }
    }

    public class Given_an_extractor
    {
        protected JsonValueExtractor Extractor;

        [SetUp]
        public void Setup()
        {
            Extractor = new JsonValueExtractor();
        }
    }
}
