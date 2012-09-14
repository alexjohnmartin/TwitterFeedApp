using System.Collections.Generic;

namespace PayByPhoneTwitterFeed.Model
{
    public class PayByPhoneFeedResult
    {
        public IEnumerable<Tweet> Tweets { get; set; }
        public IEnumerable<AccountStatistics> Statistics { get; set; }
    }
}
