using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Dto;
using Notes.Models;

namespace Notes.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public UsersController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("signUp")]
        public async Task<ActionResult> SignUp(UserDto userDto)
        {
            ServiceResponce serviceResponce = await _authRepo.Register(userDto);

            return Ok(serviceResponce);
        }

        [HttpPost("signIn")]
        public async Task<ActionResult> SignIn(UserDto userDto)
        {
            ServiceResponce serviceResponce = await _authRepo.Login(userDto.Login, userDto.Password);

            return Ok(serviceResponce);
        }
        
        [Authorize]
        [HttpPost("getProfileData")]
        public async Task<ActionResult> GetProfileData(TokenDto tokenDto)
        {
            UserDto userDto = await _authRepo.GetProfileData(tokenDto.Data);

            return Ok(userDto);
        }
        
        [Authorize]
        [HttpPost("changeEmail")]
        public async Task<ActionResult> ChangeEmail(ModificationDto modificationDto)
        {
            ServiceResponce serviceResponce = await _authRepo.ChangeEmail(modificationDto);

            return Ok(serviceResponce);
        }
        
        [Authorize]
        [HttpPost("changePassword")]
        public async Task<ActionResult> ChangePassword(ModificationDto modificationDto)
        {
            ServiceResponce serviceResponce = await _authRepo.ChangePassword(modificationDto);

            return Ok(serviceResponce);
        }
    }
}
