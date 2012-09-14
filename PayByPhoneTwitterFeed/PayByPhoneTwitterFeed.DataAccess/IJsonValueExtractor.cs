namespace PayByPhoneTwitterFeed.DataAccess
{
    public interface IJsonValueExtractor
    {
        string ExtractValueFromJson(string jsonData, string fieldName);
    }
}