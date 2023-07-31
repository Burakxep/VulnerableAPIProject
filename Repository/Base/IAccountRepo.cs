using VulnerableAPIProject.Dto.Account;
using VulnerableAPIProject.Entities.Base;

namespace VulnerableAPIProject.Repository.Base
{
    public interface IAccountRepo
    {
        ICollection<Account> GetAccounts();
        Account GetAccount(int id);
        Account GetAccountbyIdnMail(int id,string email);
        Account GetAccountByMailnPassword(string email,string password);
        Account GetAccountByMail(string email);

        bool AccountExists(int id);
        bool CreateAccount(Account account);
        bool UpdateAccount(Account account);
        bool DeleteAccount(Account account);
        bool Save();
    }
}
