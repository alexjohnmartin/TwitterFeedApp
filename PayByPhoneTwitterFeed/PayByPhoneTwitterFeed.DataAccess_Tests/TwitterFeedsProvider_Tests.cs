using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PayByPhoneTwitterFeed.DataAccess;
using PayByPhoneTwitterFeed.Model;
using Rhino.Mocks;

namespace PayByPhoneTwitterFeed.DataAccess_Tests
{
    public class TwitterFeedsProvider_Tests
    {
        [TestFixture]
        public class When_getting_twitter_feeds_from_the_provider : Given_a_provider
        {
            private PayByPhoneFeedResult _result;

            [SetUp]
            public new void Setup()
            {
                _result = Provider.GetTwitterFeeds(); 
            }

            [Test]
            public void It_should_put_a_valid_reponse_together()
            {
                Assert.That(_result, Is.Not.Null);
                Assert.That(_result.Tweets, Is.Not.Null);
                Assert.That(_result.Statistics, Is.Not.Null);
            }

            [Test]
            public void It_should_combine_tweets_from_3_feeds()
            {
                Assert.That(_result.Tweets.Count(), Is.EqualTo(3));
            }

            [Test]
            public void It_should_get_twitter_feeds_for_paybyphone()
            {
                Assert.That(_result.Statistics.Count(), Is.GreaterThan(0));
                IEnumerable<AccountStatistics> payByPhonesStats = FindStatsByName("@paybyphone");
                Assert.That(payByPhonesStats.Count(), Is.GreaterThan(0));
            }

            [Test]
            public void It_should_get_twitter_feeds_for_pay_by_phone()
            {
                Assert.That(_result.Statistics.Count(), Is.GreaterThan(0));
                IEnumerable<AccountStatistics> payByPhonesStats = FindStatsByName("@pay_by_phone");
                Assert.That(payByPhonesStats.Count(), Is.GreaterThan(0));
            }

            [Test]
            public void It_should_get_twitter_feeds_for_paybyphone_uk()
            {
                Assert.That(_result.Statistics.Count(), Is.GreaterThan(0));
                IEnumerable<AccountStatistics> payByPhonesStats = FindStatsByName("@paybyphone_uk");
                Assert.That(payByPhonesStats.Count(), Is.GreaterThan(0));
            }

            private IEnumerable<AccountStatistics> FindStatsByName(string name)
            {
                return (from t in _result.Statistics
                        where
                            t.AccountName.Equals(name,
                                                 StringComparison.InvariantCultureIgnoreCase)
                        select t);
            }
        }
    }

    public class Given_a_provider
    {
        protected TwitterFeedsProvider Provider; 

        [SetUp]
        public void Setup()
        {
            var statisticsCalculator = MockRepository.GenerateStub<IStatisticsCalculator>();
            statisticsCalculator.Stub(s => s.CalculateStatistics(null)).IgnoreArguments().Return(
                new List<AccountStatistics>
                    {
                        new AccountStatistics {AccountName = "@paybyphone"},
                        new AccountStatistics {AccountName = "@pay_by_phone"},
                        new AccountStatistics {AccountName = "@paybyphone_uk"}
                    });

            var twitterSearcher = MockRepository.GenerateStub<ITwitterSearcher>();
            twitterSearcher.Stub(t => t.Search(string.Empty)).IgnoreArguments().Return(new List<Tweet> {new Tweet()});

            Provider = new TwitterFeedsProvider(twitterSearcher, statisticsCalculator);
        }
    }
}
