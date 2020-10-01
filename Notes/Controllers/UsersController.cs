using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

            Response.Cookies.Append(
                "Login",
                (userDto.Login).ToString(),
                new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false
                });
            Response.Cookies.Append(
                "Password",
                (userDto.Password).ToString(),
                new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false
                });

            return Ok(serviceResponce);
        }

        [HttpPost("signIn")]
        public async Task<ActionResult> SignIn(UserDto userDto)
        {
            ServiceResponce serviceResponce = await _authRepo.Login(userDto.Login, userDto.Password);

            if (serviceResponce.Success)
            {
                Response.Cookies.Append(
                "IdUser",
                (serviceResponce.Id).ToString(),
                new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false
                });
                Response.Cookies.Append(
                "Login",
                (userDto.Login).ToString(),
                new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false
                });
                Response.Cookies.Append(
                "Password",
                (userDto.Password).ToString(),
                new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false
                });
            }

            return Ok(serviceResponce);
        }
    }
}
