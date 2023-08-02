using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VulnerableAPIProject.Repository;
using VulnerableAPIProject.Entities;
using VulnerableAPIProject.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using VulnerableAPIProject.Dto;
using VulnerableAPIProject.Dto.Account;
using VulnerableAPIProject.Entities.Base;
using VulnerableAPIProject.Data;
using Microsoft.EntityFrameworkCore;
using VulnerableAPIProject.JWT;
using System.Text.RegularExpressions;

namespace VulnerableAPIProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IMapper _mapper;
        private readonly JWTAuthManager _jwtAuthManager;


        public AccountController(IAccountRepo accountRepo, IMapper mapper , JWTAuthManager jwtAuthManager )
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
            _jwtAuthManager = jwtAuthManager;

        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] LoginRequest request)
        {
            var account = _accountRepo.GetAccountByMailnPassword(request.email, request.password);
            if (account == null)
            {
                return BadRequest("User not found.");
            }
            
            var token = _jwtAuthManager.GenerateTokens(account);

            return Ok("You have been logged in as: " + request.email + " Your token is: " + token);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register([FromBody] RegisterRequest request)
        {

            var account = _accountRepo.GetAccountByMail(request.email);
            if (account != null)
            {
                return BadRequest("The email address which you provided is used by another user.");
            }


            var tmp = new Account()
            {
                email = request.email,
                firstName = request.firstName,
                lastName = request.lastName,
                password = request.password 
            };

            string pattern = ("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            bool isMatch = Regex.IsMatch(request.password, pattern);
            if(isMatch == false)
            {
                return BadRequest("Password needs to have at least one uppercase,one lowercase letter, a minimum of" +
                    " 8 characters, one number and one special character.");
            } 

            _accountRepo.CreateAccount(tmp);


            return Ok("Account has been created for: " + request.email);
        }


        [Authorize(Roles ="User, admin")]
        [HttpGet]
        public ActionResult GetAccount([FromQuery] MailCheck check)
        {

            var account = _accountRepo.GetAccountByMail(check.email);
            if (account == null)
            {
                return BadRequest("User not found.");
            }

            var email = _jwtAuthManager.TakeEmailfromJWT(Request.Headers["Authorization"].ToString().Split(" ")[1]);
            if (email != check.email)
            {
                return BadRequest("Please enter your email.");
            }



            var token = _jwtAuthManager.GenerateTokens(account);
            return Ok("First name: " + account.firstName +  " Last name: " + account.lastName + " Email: "+account.email);
        }


        [Authorize(Roles = "admin")]
        [HttpDelete]
        public ActionResult DeleteAccount([FromQuery] DeleteARequest request)
        {

            var account = _accountRepo.GetAccountbyIdnMail(request.Id,request.email);
            if (account != null)
            {
                _accountRepo.DeleteAccount(account);
                return Ok("Account has been deleted.");
            }

            return BadRequest("User not found.");


        }
    }
}


    
