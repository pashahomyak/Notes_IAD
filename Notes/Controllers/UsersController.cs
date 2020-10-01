using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Dto;
using Notes.Models;

namespace Notes.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NotesContext _context;

        public UsersController(NotesContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost("signUp")]
        public async Task<ActionResult> SignUp(UserDto userDto)
        {
            if (await UserExists(userDto.Login))
            {
                return Ok("loginExist");
            }
            
            User user = new User();
            user.Login = userDto.Login;
            user.Email = userDto.Email;
            
            string password = userDto.Password;
            string saltedHash = GetPasswordHash(password);
            user.Password = saltedHash;
            
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            
            //добавить сохранение в куки
            Response.Cookies.Append(
                "Login",
                (user.Login).ToString(),
                new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false
                });
            Response.Cookies.Append(
                "Password",
                (saltedHash).ToString(),
                new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false
                });

            return Ok();
        }

        private async Task<bool> UserExists(string login)
        {
            if (await _context.User.AnyAsync(e => e.Login.ToLower() == login.ToLower()))
            {
                return true;
            }
            return false;
        }
        
        private string GetPasswordHash(string inputPassword)
        {
            var md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));

            byte[] salt = Encoding.UTF8.GetBytes("sflp49f2");

            hash.Concat(salt);

            string saltedHash = Convert.ToBase64String(md5.ComputeHash(hash));

            return saltedHash;
        }
    }
}
