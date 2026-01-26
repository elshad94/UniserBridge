namespace Project.Core.Settings
{
    public class JwtOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
        public string RefreshTokenSecurityKey { get; set; }

    }
}
