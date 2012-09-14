using PayByPhoneTwitterFeed.Model;

namespace PayByPhoneTwitterFeed.DataAccess
{
    public interface ITwitterFeedsProvider
    {
        PayByPhoneFeedResult GetTwitterFeeds();
    }
}
