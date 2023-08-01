using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using VulnerableAPIProject.Data;
using VulnerableAPIProject.Dto.Account;
using VulnerableAPIProject.Entities.Base;

namespace VulnerableAPIProject.Repository.Base
{
    public class AccountRepo : IAccountRepo
    {
        private DataContext _context;

        public AccountRepo(DataContext context)
        {
            _context = context;
        }

        public Account GetAccount(int id)
        {
            var account = _context.Account.Where(a => a.Id == id).FirstOrDefault();
            return account;
        }

        public Account GetAccountbyIdnMail(int id ,string mail)
        {
            var account = _context.Account.Where(a => a.Id == id && a.email == mail).FirstOrDefault();
            return account;
        }

        public Account GetAccountByMailnPassword(string email, string password)
        {
            var account =  _context.Account.Where(a => a.email == email && a.password == password).FirstOrDefault();


            return account;

        }

        public Account GetAccountByMail(string email)
        {
            var account = _context.Account.Where(a =>a.email == email).FirstOrDefault();

                return account;
        }


        ICollection<Account> IAccountRepo.GetAccounts()
        {
            return _context.Account.ToList();
        }


        public bool AccountExists(int id)
        {
            return _context.Account.Any(a => a.Id == id);
        }

        public bool CreateAccount(Account account)
        {
            _context.Add(account);
            return Save();
        }

        public bool UpdateAccount(Account account)
        {
            _context.Update(account);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return dondurucu(saved);
        }

        public bool dondurucu (int saved)
        {
            if (saved == 0) return false;
            return true;

        }


        public bool DeleteAccount(Account account)
        {
            _context.Remove(account);    
            return Save();
        }

       
    }
}
