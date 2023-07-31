namespace VulnerableAPIProject.JWT
{
    public interface IJWTConfig
    {
        string Key { get; set; }
        string Issuer { get; set; }
    }
}
