using VulnerableAPIProject.Data;
using VulnerableAPIProject.Entities;
using VulnerableAPIProject.Entities.Base;

namespace VulnerableAPIProject
{
    public class Seed
    {

        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.Account.Any())
            {
                var account = new Account() { email = "test", firstName = "test", lastName = "test", password = "test", role = "User" };
                dataContext.Account.Add(account);
                dataContext.SaveChanges();
            }   

            if(!dataContext.Profile.Any()) {
                var profile = new Profile() { email = "test", description = "test", birthday = DateTime.Now};
                dataContext.Profile.Add(profile);
                dataContext.SaveChanges();
            }



        }
    }
}
