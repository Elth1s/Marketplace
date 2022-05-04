namespace WebAPI.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        //Hours
        public int AccessTokenDuration { get; set; }
        //Days
        public int RefreshTokenDuration { get; set; }
    }
}
