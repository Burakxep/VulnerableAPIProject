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

namespace VulnerableAPIProject.Controllers

 
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepo _profileRepo;
        private readonly IMapper _mapper;
        private readonly IAccountRepo _accountRepo;

        public ProfileController(IProfileRepo profileRepo, IMapper mapper, IAccountRepo accountRepo)
        {
            _profileRepo = profileRepo;
            _mapper = mapper;
            _accountRepo = accountRepo;
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetProfile([FromQuery] string email)
        {
            // input validation
            // dışarıdan email gelirse
            // regex ile email pattern kontrol edilece
            // eğer doğru ise akış devam edilecek diper türlü geçerli email giriniz
            var profile = _profileRepo.GetProfile(email);
            

                if (profile == null)
            {
                return BadRequest("Profile not found.");
            }

            return Ok(profile);
        }



        [AllowAnonymous]
        [HttpPost]
        public ActionResult CreateProfile([FromBody] ProfileRequest request , [FromQuery] MailCheck check)
        {

            var profile =  _profileRepo.GetProfile(request.email);
            var account = _accountRepo.GetAccountByMail(check.email);
            

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
        
        [AllowAnonymous]
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

