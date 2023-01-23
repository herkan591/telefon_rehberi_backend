using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TelefonRehberi.Business.Dtos;
using TelefonRehberi.Entities;

namespace TelefonRehberi.Business.Abstract
{
    public interface IAuthService
    {
        Task<User> Register(UserCreateDto userCreateDto);

        Task<User> Login(UserLoginDto userLoginDto);

        Task<bool> UserExist(string userName);

        Task<User> Update(User user);

        Task<User> GetUserByRefreshToken(String refreshToken);

        Task<User> GetUserByEmail(String email);
    }
}
