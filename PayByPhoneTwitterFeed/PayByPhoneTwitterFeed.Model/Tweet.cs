using System;

namespace PayByPhoneTwitterFeed.Model
{
    public class Tweet
    {
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}