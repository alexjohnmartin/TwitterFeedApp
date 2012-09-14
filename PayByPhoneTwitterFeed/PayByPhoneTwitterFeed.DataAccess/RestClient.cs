using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace PayByPhoneTwitterFeed.DataAccess
{
    public class RestClient : IRestClient
    {
        private readonly IOAuthConfiguration _oAuthConfiguration;

        public RestClient(IOAuthConfiguration oAuthConfiguration)
        {
            _oAuthConfiguration = oAuthConfiguration;
        }

        public RestClient()
        {
            _oAuthConfiguration = new OAuthConfiguration();
        }

        public string GetResponse(string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "text/json";
            request.Method = "GET";
            ////request.Headers.Add("Authorization", GenerateAuthorizationHeaderParams(uri));
            ////request.KeepAlive = false; 

            var response = (HttpWebResponse)request.GetResponse();
            var responseEncoding = Encoding.GetEncoding(response.CharacterSet);

            string result;
            using (var sr = new StreamReader(response.GetResponseStream(), responseEncoding))
            {
                result = sr.ReadToEnd();
            }

            return result; 
        }

        ////private string GenerateAuthorizationHeaderParams(string uri)
        ////{
        ////    //based on code from here:
        ////    //http://garyshortblog.wordpress.com/2011/02/11/a-twitter-oauth-example-in-c/

        ////    //GS - Get the oAuth params
        ////    string status = _oAuthConfiguration.Status;
        ////    string oauth_consumer_key = _oAuthConfiguration.Key;
        ////    string oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
        ////    string oauth_signature_method = "HMAC-SHA1";
        ////    string oauth_token = _oAuthConfiguration.Token;
        ////    TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        ////    string oauth_timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();
        ////    string oauth_version = "1.0";

        ////    //GS - When building the signature string the params
        ////    //must be in alphabetical order. I can't be bothered
        ////    //with that, get SortedDictionary to do it's thing
        ////    var sd = new SortedDictionary<string, string>
        ////                 {
        ////                     //{"status", status},
        ////                     {"oauth_version", oauth_version},
        ////                     {"oauth_consumer_key", oauth_consumer_key},
        ////                     {"oauth_nonce", oauth_nonce},
        ////                     {"oauth_signature_method", oauth_signature_method},
        ////                     {"oauth_timestamp", oauth_timestamp},
        ////                     {"oauth_token", oauth_token}
        ////                 };

        ////    //GS - Build the signature string
        ////    string baseString = String.Empty;
        ////    baseString += "GET" + "&";
        ////    baseString += Uri.EscapeDataString(uri) + "&";

        ////    foreach (KeyValuePair<string, string> entry in sd)
        ////    {
        ////        baseString += Uri.EscapeDataString(entry.Key + "=" + entry.Value + "&");
        ////    }

        ////    //GS - Remove the trailing ambersand char, remember 
        ////    //it's been urlEncoded so you have to remove the 
        ////    //last 3 chars - %26
        ////    baseString = baseString.Substring(0, baseString.Length - 3);

        ////    //GS - Build the signing key
        ////    string consumerSecret = _oAuthConfiguration.Secret;
        ////    string oauth_token_secret = _oAuthConfiguration.TokenSecret;
        ////    string signingKey = Uri.EscapeDataString(consumerSecret) + "&" + Uri.EscapeDataString(oauth_token_secret);

        ////    //GS - Sign the request
        ////    HMACSHA1 hasher = new HMACSHA1(new ASCIIEncoding().GetBytes(signingKey));

        ////    string signatureString = Convert.ToBase64String(hasher.ComputeHash(new ASCIIEncoding().GetBytes(baseString)));

        ////    //GS - Tell Twitter we don't do the 100 continue thing
        ////    ////ServicePointManager.Expect100Continue = false;

        ////    string authorizationHeaderParams = String.Empty;
        ////    authorizationHeaderParams += "OAuth ";
        ////    authorizationHeaderParams += "oauth_consumer_key=" + "\"" + Uri.EscapeDataString(oauth_consumer_key) + "\",";
        ////    authorizationHeaderParams += "oauth_nonce=" + "\"" + Uri.EscapeDataString(oauth_nonce) + "\",";
        ////    authorizationHeaderParams += "oauth_signature=" + "\"" + Uri.EscapeDataString(signatureString) + "\",";
        ////    authorizationHeaderParams += "oauth_signature_method=" + "\"" + Uri.EscapeDataString(oauth_signature_method) + "\",";
        ////    authorizationHeaderParams += "oauth_timestamp=" + "\"" + Uri.EscapeDataString(oauth_timestamp) + "\",";
        ////    authorizationHeaderParams += "oauth_token=" + "\"" + Uri.EscapeDataString(oauth_token) + "\",";
        ////    authorizationHeaderParams += "oauth_version=" + "\"" + Uri.EscapeDataString(oauth_version) + "\"";

        ////    return authorizationHeaderParams; 
        ////}
    }
}