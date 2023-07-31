using VulnerableAPIProject.Entities.Base;
using VulnerableAPIProject.Data;
using Microsoft.EntityFrameworkCore;

namespace VulnerableAPIProject.Repository.Base
{
    public class ProfileRepo : IProfileRepo
    {
        private DataContext _context;

        public ProfileRepo(DataContext context)
        {
            _context = context;
        }

        public Profile GetProfile(string email)
        {
            return _context.Profile.Where(a => a.email == email).FirstOrDefault();
        }


        public ICollection<Profile> GetProfiles()
        {
            return _context.Profile.ToList();
        }


        public bool ProfileExists(int id)
        {
            return _context.Profile.Any(p => p.Id == id);
        }

        public bool CreateProfile(Profile profile)
        {
            _context.Add(profile);
            return Save();

        }

        public bool UpdateProfile(Profile profile)
        {
            _context.Update(profile);
            return Save();
        }

        public bool DeleteProfile(Profile profile)
        {
            _context.Remove(profile);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return dondurucu(saved);
        }

        public bool dondurucu(int saved)
        {
            if (saved == 0) return false;
            return true;

        }


    }
}
