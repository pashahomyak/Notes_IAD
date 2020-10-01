using Microsoft.EntityFrameworkCore;
using Notes.Dto;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly NotesContext _context;

        public AuthRepository (NotesContext context)
        {
            _context = context;
        }
        
        public async Task<ServiceResponce> Login(string login, string password)
        {
            ServiceResponce serviceResponce = new ServiceResponce();
            User user = await _context.User.FirstOrDefaultAsync(x => x.Login.ToLower().Equals(login.ToLower()));
            if (user == null)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = "User not found.";
            }
            else if (!VerifyPassword(password, user.Password))
            {
                serviceResponce.Success = false;
                serviceResponce.Message = "Wrong password.";
            }
            else
            {
                serviceResponce.Id = user.IdUser;
                serviceResponce.Success = true;
                serviceResponce.Message = "Authorization was successful.";
            }

            return serviceResponce;
        }

        public async Task<ServiceResponce> Register(UserDto userDto)
        {
            ServiceResponce serviceResponce = new ServiceResponce();

            if (await UserExists(userDto.Login))
            {
                serviceResponce.Success = false;
                serviceResponce.Message = "User already exists.";
                return serviceResponce;
            }

            User user = new User();
            user.Login = userDto.Login;
            user.Email = userDto.Email;

            string password = userDto.Password;
            string saltedHash = GetPasswordHash(password);
            user.Password = saltedHash;

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            serviceResponce.Success = true;
            serviceResponce.Message = "Registration was successful.";

            return serviceResponce;
        }
        public async Task<bool> UserExists(string login)
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
        private bool VerifyPassword(string inputPassword, string userHashedPassword)
        {
            string inputHashedPass = GetPasswordHash(inputPassword);

            if (inputHashedPass.Equals(userHashedPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
