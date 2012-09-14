using System.Collections.Generic;
using PayByPhoneTwitterFeed.Model;

namespace PayByPhoneTwitterFeed.DataAccess
{
    public interface IJsonParser
    {
        IEnumerable<Tweet> Parse(string jsonData);
    }
}