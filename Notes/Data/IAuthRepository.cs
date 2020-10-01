using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notes.Data;
using Notes.Dto;

namespace Notes.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponce> Register(UserDto userDto);
        Task<ServiceResponce> Login(string login, string password);
        Task<bool> UserExists(string login);

    }
}
