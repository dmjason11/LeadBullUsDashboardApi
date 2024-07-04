using Api.DTOS;
using Api.Errors;
using Core.Identity;
using Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Mail;

namespace Api.Controllers
{
   
    public class AcountController : BaseController
    {
        private readonly UserManager<AppUser>_userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJWTService _jWTService;
        public AcountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager,
            IJWTService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jWTService = jwtService;
        }
        [HttpPost("register")]
        public async Task<ActionResult> register(RegisterDto model)
        {
            var userModel = await _userManager.FindByEmailAsync(model.Email);
            if(userModel != null)
            {
                return BadRequest(new ApiResponse(400, "email already exist"));
            }
            var user = new AppUser()
            {
                Email = model.Email,
                UserName = new MailAddress(model.Email).User
            };

            var res = await _userManager.CreateAsync(user,model.Password);
            if (!res.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserInfoDto>> login(UserLogin model)
        {
            var user = await _userManager.Users.Include(x=>x.ServiceProfiles).Where(x=>x.Email == model.Email).FirstOrDefaultAsync();
            if(user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var res = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!res.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }
            return Ok(getUserInfo(user));
        }
        private UserInfoDto getUserInfo(AppUser user)
        {
            return new UserInfoDto()
            {
                Id = user.Id,
                Token = _jWTService.GetJWT(user),
                userName = user.UserName,
                serviceProfiles = user.ServiceProfiles.Select(x => x.ServiceName).ToList()
            };
        }
    }
}
