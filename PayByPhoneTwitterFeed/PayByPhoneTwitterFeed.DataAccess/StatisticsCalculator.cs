using System.Collections.Generic;
using System.Linq;
using PayByPhoneTwitterFeed.Model;

namespace PayByPhoneTwitterFeed.DataAccess
{
    public class StatisticsCalculator : IStatisticsCalculator
    {
        public IEnumerable<AccountStatistics> CalculateStatistics(IEnumerable<Tweet> tweets)
        {
            var stats = new Dictionary<string, AccountStatistics>(); 

            foreach (var tweet in tweets)
            {
                if (!stats.ContainsKey(tweet.Author))
                {
                    stats.Add(tweet.Author, new AccountStatistics{AccountName = tweet.Author});
                }

                stats[tweet.Author].NumberOfTweets++;
                stats[tweet.Author].NumberOfTimesOtherUsersMentioned += CountUserMentionsInTweet(tweet.Content); 
            }

            return stats.Values; 
        }

        private static int CountUserMentionsInTweet(string tweetText)
        {
            return (from c in tweetText.ToCharArray() where c == '@' select c).Count();
        }
    }
}