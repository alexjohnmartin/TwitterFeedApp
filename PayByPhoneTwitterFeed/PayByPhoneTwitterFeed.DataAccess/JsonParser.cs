using System;
using System.Collections.Generic;
using PayByPhoneTwitterFeed.Model;

namespace PayByPhoneTwitterFeed.DataAccess
{
    public class JsonParser : IJsonParser
    {
        private readonly IJsonValueExtractor _jsonValueExtractor;

        public JsonParser(IJsonValueExtractor jsonValueExtractor)
        {
            _jsonValueExtractor = jsonValueExtractor;
        }

        public JsonParser()
        {
            _jsonValueExtractor = new JsonValueExtractor(); 
        }

        private const string CreatedAt = "created_at";
        private const string FromUser = "from_user";
        private const string Text = "text"; 

        public IEnumerable<Tweet> Parse(string jsonData)
        {
            var tweets = new List<Tweet>(); 

            while (jsonData.Contains(CreatedAt) && 
                   jsonData.Contains("from_user") &&
                   jsonData.Contains("text"))
            {
                tweets.Add(new Tweet
                               {
                                   Author = _jsonValueExtractor.ExtractValueFromJson(jsonData, FromUser),
                                   Content = _jsonValueExtractor.ExtractValueFromJson(jsonData, Text),
                                   Created =
                                       DateTime.Parse(_jsonValueExtractor.ExtractValueFromJson(jsonData, CreatedAt))
                               });

                jsonData = jsonData.Substring(jsonData.IndexOf(Text) + Text.Length);
            }

            return tweets; 
        }
    }
}