using VulnerableAPIProject.Entities;
using VulnerableAPIProject.Entities.Base;

namespace VulnerableAPIProject.Repository.Base
{
    public interface IProfileRepo
    {
        ICollection<Profile> GetProfiles();
        Profile GetProfile(string email);

        bool ProfileExists(int id);
        bool CreateProfile(Profile profile);
        bool UpdateProfile(Profile profile);
        bool DeleteProfile(Profile profile);
        bool Save();
    }
}
