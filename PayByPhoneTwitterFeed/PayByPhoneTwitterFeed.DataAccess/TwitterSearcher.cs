using System.Collections.Generic;
using PayByPhoneTwitterFeed.Model;

namespace PayByPhoneTwitterFeed.DataAccess
{
    public class TwitterSearcher : ITwitterSearcher
    {
        private readonly IRestClient _restClient;
        private readonly IJsonParser _jsonParser;
        //private const string TwitterSearchUri = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name="; 
        private const string TwitterSearchUri = "http://search.twitter.com/search.json?q=";

        public TwitterSearcher(IRestClient restClient, IJsonParser jsonParser)
        {
            _restClient = restClient;
            _jsonParser = jsonParser;
        }

        public TwitterSearcher()
        {
            _restClient = new RestClient();
            _jsonParser = new JsonParser();
        }

        public IEnumerable<Tweet> Search(string searchTerm)
        {
            var jsonData = _restClient.GetResponse(TwitterSearchUri + searchTerm);
            return _jsonParser.Parse(jsonData); 
        }
    }
}