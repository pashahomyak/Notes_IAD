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
using System.Text.RegularExpressions;
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

        public async Task<User> GetById(int id)
        {
            return await _context.User.FindAsync(id);
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

            if (!IsValidEmail(userDto.Email))
            {
                serviceResponce.success = false;
                serviceResponce.message = "Incorrect Email.";
                return serviceResponce;
            }
            
            if (!IsValidPassword(Consts.PASSWORD_REGEX_PATTERN, userDto.Password))
            {
                serviceResponce.success = false;
                serviceResponce.message = "Incorrect Password.";
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
        private bool IsValidEmail(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }
        private bool IsValidPassword(string pattern, string password)
        {
            Regex regex = new Regex(pattern);

            return regex.IsMatch(password);
        }
        public async Task<UserDto> GetProfileData(string inputToken)
        {
            JwtSecurityToken decodedToken = GetDecodedToken(inputToken);

            User user = await GetById(Convert.ToInt32(decodedToken.Claims.First(c => c.Type == "nameid").Value));

            UserDto userDto = new UserDto
            {
                Email = user.Email,
                Login = user.Login
            };

            return userDto;
        }
        public async Task<ServiceResponce> ChangeEmail(ModificationDto modificationDto)
        {
            JwtSecurityToken decodedToken = GetDecodedToken(modificationDto.Token);

            User user = await GetById(Convert.ToInt32(decodedToken.Claims.First(c => c.Type == "nameid").Value));
            
            //https://www.entityframeworktutorial.net/efcore/update-data-in-entity-framework-core.aspx

            ServiceResponce serviceResponce = new ServiceResponce();
            
            if (user.Email != modificationDto.OldValue)
            {
                serviceResponce.success = false;
                serviceResponce.message = "Incorrect current Email";
            }
            else
            {
                if (!IsValidEmail(modificationDto.NewValue))
                {
                    serviceResponce.success = false;
                    serviceResponce.message = "Incorrect new Email";
                    return serviceResponce;
                }
                
                user.Email = modificationDto.NewValue;

                _context.Update<User>(user);
                _context.SaveChanges();

                serviceResponce.success = true;
                serviceResponce.message = "Email change successfully.";
                
                serviceResponce.data = CreateToken(user);
            }

            return serviceResponce;
        }
        public async Task<ServiceResponce> ChangePassword(ModificationDto modificationDto)
        {
            JwtSecurityToken decodedToken = GetDecodedToken(modificationDto.Token);

            User user = await GetById(Convert.ToInt32(decodedToken.Claims.First(c => c.Type == "nameid").Value));
            
            //https://www.entityframeworktutorial.net/efcore/update-data-in-entity-framework-core.aspx

            ServiceResponce serviceResponce = new ServiceResponce();
            
            if (user.Password != GetPasswordHash(modificationDto.OldValue))
            {
                serviceResponce.success = false;
                serviceResponce.message = "Incorrect Password";
            }
            else
            {
                if (!IsValidPassword(Consts.PASSWORD_REGEX_PATTERN, modificationDto.NewValue))
                {
                    serviceResponce.success = false;
                    serviceResponce.message = "Incorrect Password.";
                    return serviceResponce;
                }
                
                user.Password = GetPasswordHash(modificationDto.NewValue);

                _context.Update<User>(user);
                _context.SaveChanges();

                serviceResponce.success = true;
                serviceResponce.message = "Password change successfully.";
                
                serviceResponce.data = CreateToken(user);
            }

            return serviceResponce;
        }

        public async Task<NoteCategoryDto> GetNoteCategories(string inputToken)
        {
            JwtSecurityToken decodedToken = GetDecodedToken(inputToken);

            int idUser = Convert.ToInt32(decodedToken.Claims.First(c => c.Type == "nameid").Value);
            List<UserHasNoteCategory> userHasNoteCategories = await _context.UserHasNoteCategory.Where(u => u.IdUser == idUser).ToListAsync();
            
            NoteCategoryDto noteCategoryDto = new NoteCategoryDto();
            List<string> categories = new List<string>();

            foreach (var userHasNoteCategory in userHasNoteCategories)
            {
                string noteCategory =
                    _context.NoteCategory.FirstAsync(p => p.IdNoteCategory == userHasNoteCategory.IdNoteCategory).Result.Name;
                categories.Add(noteCategory);
            }

            noteCategoryDto.Categories = categories;
            
            return noteCategoryDto;
        }
    }
}
