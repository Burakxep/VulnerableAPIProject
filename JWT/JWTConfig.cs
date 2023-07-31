namespace VulnerableAPIProject.JWT
{
    public class JWTConfig : IJWTConfig
    {
        public string Key { get; set ; }
        public string Issuer { get; set; }
    }
}
