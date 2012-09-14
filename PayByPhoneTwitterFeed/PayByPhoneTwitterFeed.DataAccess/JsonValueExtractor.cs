namespace PayByPhoneTwitterFeed.DataAccess
{
    public class JsonValueExtractor : IJsonValueExtractor
    {
        public string ExtractValueFromJson(string jsonData, string fieldName)
        {
            int startIndex = jsonData.IndexOf(fieldName) + fieldName.Length + 3;
            int length = jsonData.IndexOf('"', startIndex + 1) - startIndex; 
            return jsonData.Substring(startIndex, length);
        }
    }
}