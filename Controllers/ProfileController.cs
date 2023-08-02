using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VulnerableAPIProject.Repository;
using VulnerableAPIProject.Entities;
using VulnerableAPIProject.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using VulnerableAPIProject.Dto;
using VulnerableAPIProject.Dto.Profile;
using VulnerableAPIProject.Entities.Base;
using VulnerableAPIProject.Dto.Account;
using VulnerableAPIProject.JWT;

namespace VulnerableAPIProject.Controllers

 
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepo _profileRepo;
        private readonly IMapper _mapper;
        private readonly IAccountRepo _accountRepo;
        private readonly JWTAuthManager _jwtAuthManager;

        public ProfileController(IProfileRepo profileRepo, IMapper mapper, IAccountRepo accountRepo, JWTAuthManager jwtAuthManager)
        {
            _profileRepo = profileRepo;
            _mapper = mapper;
            _accountRepo = accountRepo;
            _jwtAuthManager = jwtAuthManager;
        }


        [Authorize(Roles = "User , admin")] 
        [HttpGet]
        public ActionResult GetProfile([FromQuery] string email)
        {

            var profile = _profileRepo.GetProfile(email);
            

                if (profile == null)
            {
                return BadRequest("Profile not found.");
            }

            return Ok(profile);
        }



        [Authorize(Roles = "User, admin")] 
        [HttpPost]
        public ActionResult CreateProfile([FromBody] ProfileRequest request , [FromQuery] MailCheck check)
        {

            var profile =  _profileRepo.GetProfile(request.email);
            var account = _accountRepo.GetAccountByMail(check.email);

            var email = _jwtAuthManager.TakeEmailfromJWT(Request.Headers["Authorization"].ToString().Split(" ")[1]);
            if (email != check.email)
            {
                return BadRequest("Please enter your email.");
            }

            if (profile == null && account != null)
            {

                var tmp = new Entities.Base.Profile() {email = request.email, 
                                                       description = request.description, 
                                                       birthday = request.birthday};

                _profileRepo.CreateProfile(tmp);
                return Ok("Profile has been created for: " + request.email);

            }
           else return BadRequest();
        }
        
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public ActionResult DeleteProfile([FromQuery] DeleteRequest request)
        {
            var profile = _profileRepo.GetProfile(request.email);

            if (profile != null)
            {
                _profileRepo.DeleteProfile(profile);
                return Ok("Profile has been deleted.");
            }
            return BadRequest("Profile not found");

        }
        
    }



}

