using System.Collections.Generic;
using PayByPhoneTwitterFeed.Model;

namespace PayByPhoneTwitterFeed.DataAccess
{
    public interface IStatisticsCalculator
    {
        IEnumerable<AccountStatistics> CalculateStatistics(IEnumerable<Tweet> tweets);
    }
}