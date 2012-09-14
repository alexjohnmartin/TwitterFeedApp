using System.Collections.Generic;
using System.Linq;
using PayByPhoneTwitterFeed.Model;

namespace PayByPhoneTwitterFeed.DataAccess
{
    public class TwitterFeedsProvider : ITwitterFeedsProvider
    {
        private ITwitterSearcher _twitterSearcher;
        private IStatisticsCalculator _statisticsCalculator; 

        public TwitterFeedsProvider(ITwitterSearcher twitterSearcher, IStatisticsCalculator statisticsCalculator)
        {
            _twitterSearcher = twitterSearcher;
            _statisticsCalculator = statisticsCalculator;
        }

        public TwitterFeedsProvider()
        {
            _twitterSearcher = new TwitterSearcher(); 
            _statisticsCalculator = new StatisticsCalculator();
        }

        public PayByPhoneFeedResult GetTwitterFeeds()
        {
            var feedResult = new PayByPhoneFeedResult();

            feedResult.Tweets = GetAllTweets();
            feedResult.Statistics = _statisticsCalculator.CalculateStatistics(feedResult.Tweets);

            return feedResult;
        }

        private IEnumerable<Tweet> GetAllTweets()
        {
            var paybyphoneTweets = _twitterSearcher.Search("paybyphone");
            var pay_by_phoneTweets = _twitterSearcher.Search("pay_by_phone");
            var paybyphone_ukTweets = _twitterSearcher.Search("paybyphone_uk");

            return from t in paybyphoneTweets.Concat(pay_by_phoneTweets).Concat(paybyphone_ukTweets)
                   orderby t.Created
                   select t; 
        }
    }
}