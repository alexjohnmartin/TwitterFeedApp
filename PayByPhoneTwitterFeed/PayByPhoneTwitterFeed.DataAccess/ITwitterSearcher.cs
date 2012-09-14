using System.Collections.Generic;
using PayByPhoneTwitterFeed.Model;

namespace PayByPhoneTwitterFeed.DataAccess
{
    public interface ITwitterSearcher
    {
        IEnumerable<Tweet> Search(string searchTerm); 
    }
}