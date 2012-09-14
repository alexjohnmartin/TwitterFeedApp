using System.Configuration;

namespace PayByPhoneTwitterFeed.DataAccess
{
    public class OAuthConfiguration : IOAuthConfiguration
    {
        public string Status { get { return ConfigurationManager.AppSettings["OAuthStatus"]; } }
        public string Key { get { return ConfigurationManager.AppSettings["OAuthKey"]; } }
        public string Secret { get { return ConfigurationManager.AppSettings["OAuthSecret"]; } }
        public string Token { get { return ConfigurationManager.AppSettings["OAuthToken"]; } }
        public string TokenSecret { get { return ConfigurationManager.AppSettings["OAuthTokenSecret"]; } }
    }
}