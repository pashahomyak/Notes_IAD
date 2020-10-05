using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notes.Data;
using Notes.Dto;
using Notes.Models;

namespace Notes.Data
{
    public interface IAuthRepository
    {
        public User GetById(int id);
        Task<ServiceResponce> Register(UserDto userDto);
        Task<ServiceResponce> Login(string login, string password);
        Task<bool> UserExists(string login);

    }
}
