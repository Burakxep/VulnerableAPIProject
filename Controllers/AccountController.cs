﻿using AutoMapper;
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

namespace VulnerableAPIProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IMapper _mapper;
      


        public AccountController(IAccountRepo accountRepo, IMapper mapper )
        {
            _accountRepo = accountRepo;
            _mapper = mapper;

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

            return Ok("You have logged in as: " + request.email);
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
            _accountRepo.CreateAccount(tmp);


            return Ok("Account has been created for: " + request.email);
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetAccount([FromQuery] int id)
        {
            var account = _accountRepo.GetAccount(id);
            if (account == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(account);   
        }


        [AllowAnonymous]
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


    
