namespace PayByPhoneTwitterFeed.DataAccess
{
    public interface IOAuthConfiguration
    {
        string Status { get; }
        string Key { get; }
        string Secret { get; }
        string Token { get; }
        string TokenSecret { get; }
    }
}