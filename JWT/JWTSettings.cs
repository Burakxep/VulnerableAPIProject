namespace VulnerableAPIProject.JWT
{
    public class JWTSettings: IJWTSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int accessTokenExpiration { get; set; }
        public int refreshTokenExpiration { get; set; }
    }
}
