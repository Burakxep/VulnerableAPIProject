namespace VulnerableAPIProject.JWT
{
    public interface IJWTSettings
    {
        string Key { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        int accessTokenExpiration { get; set; }
        int refreshTokenExpiration { get; set; }
    }
}
