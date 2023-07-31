using VulnerableAPIProject.Dto.Account;
namespace VulnerableAPIProject.Dto.Profile
{
    public class ProfileRequest
    {
        public string email { get; set; }
        public string description { get; set; }
        public DateTime birthday { get; set; }
    }
}
