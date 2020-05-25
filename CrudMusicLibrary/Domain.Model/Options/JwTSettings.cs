namespace Domain.Model.Options
{
    public class JwTSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int ExpiryTimeInSeconds { get; set; }
    }
}
