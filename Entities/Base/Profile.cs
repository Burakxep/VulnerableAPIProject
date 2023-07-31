namespace VulnerableAPIProject.Entities.Base
{
    public class Profile
    {
        public int Id { get; set; } 
        public string email { get; set; }
        public string description { get; set; }
        public DateTime birthday { get; set; }
    }
}
