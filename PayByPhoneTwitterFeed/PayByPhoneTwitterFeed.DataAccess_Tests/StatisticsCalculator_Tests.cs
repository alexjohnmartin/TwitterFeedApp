using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PayByPhoneTwitterFeed.DataAccess;
using PayByPhoneTwitterFeed.Model;

namespace PayByPhoneTwitterFeed.DataAccess_Tests
{
    public class StatisticsCalculator_Tests
    {
        [TestFixture]
        public class When_I_calculate_the_statistics : Given_a_calculator
        {
            private IEnumerable<AccountStatistics> _result;
            private const string Author1 = "paybyphone";
            private const string Author2 = "pay_by_phone";
            private const string Author3 = "paybyphone_uk"; 

            [SetUp]
            public new void Setup()
            {
                IEnumerable<Tweet> tweets = new List<Tweet>
                                                {
                                                    new Tweet{ Author = Author1, Content = "test"}, 
                                                    new Tweet{ Author = Author2, Content = "test @alexjohnmartin"}, 
                                                    new Tweet{ Author = Author3, Content = "test"}, 
                                                    new Tweet{ Author = Author1, Content = "test @a @b"}, 
                                                    new Tweet{ Author = Author1, Content = "test @a @b @c"}, 
                                                    new Tweet{ Author = Author2, Content = "test"}, 
                                                    new Tweet{ Author = Author2, Content = "test"}, 
                                                    new Tweet{ Author = Author3, Content = "test"}, 
                                                };


                _result = Calculator.CalculateStatistics(tweets);
            }

            [Test]
            public void It_should_return_a_valid_result()
            {
                Assert.That(_result, Is.Not.Null);
                Assert.That(_result.Count(), Is.GreaterThan(0));
            }

            [Test]
            public void It_should_return_3_different_stats()
            {
                Assert.That(_result.Count(), Is.EqualTo(3));
            }

            [Test]
            public void It_should_calculate_stats_for_paybyphone()
            {
                var stats = (from s in _result where s.AccountName == Author1 select s).First(); 
                Assert.That(stats.NumberOfTweets, Is.EqualTo(3));
                Assert.That(stats.NumberOfTimesOtherUsersMentioned, Is.EqualTo(5));
            }

            [Test]
            public void It_should_calculate_stats_for_pay_by_phone()
            {
                var stats = (from s in _result where s.AccountName == Author2 select s).First();
                Assert.That(stats.NumberOfTweets, Is.EqualTo(3));
                Assert.That(stats.NumberOfTimesOtherUsersMentioned, Is.EqualTo(1));
            }

            [Test]
            public void It_should_calculate_stats_for_paybyphone_uk()
            {
                var stats = (from s in _result where s.AccountName == Author3 select s).First();
                Assert.That(stats.NumberOfTweets, Is.EqualTo(2));
                Assert.That(stats.NumberOfTimesOtherUsersMentioned, Is.EqualTo(0));
            }
        }
    }

    public class Given_a_calculator
    {
        protected StatisticsCalculator Calculator;

        [SetUp]
        public void Setup()
        {
            Calculator = new StatisticsCalculator();
        }
    }
}
