using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Notes.Dto;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly NotesContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository (NotesContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public User GetById(int id)
        {
            return _context.User.Find(id);
        }
        public async Task<ServiceResponce> Login(string login, string password)
        {
            ServiceResponce serviceResponce = new ServiceResponce();
            User user = await _context.User.FirstOrDefaultAsync(x => x.Login.ToLower().Equals(login.ToLower()));
            if (user == null)
            {
                serviceResponce.success = false;
                serviceResponce.message = "User not found.";
            }
            else if (!VerifyPassword(password, user.Password))
            {
                serviceResponce.success = false;
                serviceResponce.message = "Wrong password.";
            }
            else
            {
                serviceResponce.data = CreateToken(user);
                serviceResponce.success = true;
                serviceResponce.message = "Authorization was successful.";
            }

            return serviceResponce;
        }
        public async Task<ServiceResponce> Register(UserDto userDto)
        {
            ServiceResponce serviceResponce = new ServiceResponce();

            if (await UserExists(userDto.Login))
            {
                serviceResponce.success = false;
                serviceResponce.message = "User already exists.";
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

            serviceResponce.data = CreateToken(user);
            serviceResponce.success = true;
            serviceResponce.message = "Registration was successful.";

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
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Hash, user.Password)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
        private JwtSecurityToken GetDecodedToken(string inputToken)
        {
            var jwt = inputToken;
            var handler = new JwtSecurityTokenHandler();
            var resultToken = handler.ReadJwtToken(jwt);

            return resultToken;
        }
        
        public async Task<UserDto> GetProfileData(string inputToken)
        {
            JwtSecurityToken decodedToken = GetDecodedToken(inputToken);

            User user = GetById(Convert.ToInt32(decodedToken.Claims.First(c => c.Type == "nameid").Value));

            UserDto userDto = new UserDto
            {
                Email = user.Email,
                Login = user.Login
            };

            return userDto;
        }
    }
}
