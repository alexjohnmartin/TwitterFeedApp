using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using PayByPhoneTwitterFeed.Controllers;
using PayByPhoneTwitterFeed.DataAccess;
using PayByPhoneTwitterFeed.Model;
using Rhino.Mocks;

namespace PayByPhoneTwitterFeed_Tests.Controllers
{
    public class TwitterFeedsController_Tests
    {
        [TestFixture]
        public class When_the_index_action_is_called : Given_a_TwitterFeedsController
        {
            private ActionResult _result; 

            [SetUp]
            public new void Setup()
            {
                _result = Controller.Index(); 
            }

            [Test]
            public void It_should_call_the_provider_for_data()
            {
                Provider.AssertWasCalled(p => p.GetTwitterFeeds());   

                //this is a clean but brittle test since it is checking internal implementation
                //if someone were to alter the Controller class this test would break even if the class functioned correctly
            }

            [Test]
            public void It_should_return_data_from_the_provider()
            {
                Assert.That(_result, Is.Not.Null);
                var content = (PayByPhoneFeedResult)(_result as JsonResult).Data; 
                Assert.That(content.Tweets.Count(), Is.GreaterThan(0));

                //this is a less pretty but much more stable test because it tests the outcome
                //if the Controller is altered this test doesn't care - it just checks that the result is correct
            }
        }
    }

    public class Given_a_TwitterFeedsController
    {
        protected TwitterFeedsController Controller;
        protected ITwitterFeedsProvider Provider;
        private readonly PayByPhoneFeedResult Result = new PayByPhoneFeedResult
                                                  {
                                                      Statistics = new List<AccountStatistics>(),
                                                      Tweets = new List<Tweet>{new Tweet()}
                                                  };

        [SetUp]
        public void Setup()
        {
            Provider = MockRepository.GenerateMock<ITwitterFeedsProvider>();
            Provider.Stub(p => p.GetTwitterFeeds()).Return(Result);

            Controller = new TwitterFeedsController(Provider);
        }
    }
}
