using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TelefonRehberi.Business.Abstract;
using TelefonRehberi.Business.Dtos;
using TelefonRehberi.Business.ValidationRules;
using TelefonRehberi.DataAccess.Abstract;
using TelefonRehberi.Entities;

namespace TelefonRehberi.Business.Concrete
{
    public class AuthManager : IAuthService
    {

        public IUnitOfWork _unitOfWork;
        UserLoginValidator userLoginValidator=new UserLoginValidator();
        UserCreateValidator userCreateValidator = new UserCreateValidator();
        UserValidator userValidator = new UserValidator();

        public AuthManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Login(UserLoginDto userLoginDto)
        {
            userLoginValidator.ValidateAndThrow(userLoginDto);

            User user = await _unitOfWork.DbContext.Users.FirstOrDefaultAsync(x => x.Email == userLoginDto.Email);

            if (user == null)
            {
                return null;
            }
            else if (!VerifyUserPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            

            return user;
        }

        public async Task<User> Register(UserCreateDto userCreateDto)
        {

            userCreateValidator.ValidateAndThrow(userCreateDto);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userCreateDto.Password, out passwordHash, out passwordSalt);

            User user = new User();

            user.Name = userCreateDto.Name;
            user.Surname= userCreateDto.Surname;
            user.Email = userCreateDto.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return await _unitOfWork.UserRepository.Create(user);

        }
        public async Task<User> GetUserByRefreshToken(String refreshToken)
        {
            return await _unitOfWork.DbContext.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

        }

        public async Task<User> GetUserByEmail(String email)
        {
            return await _unitOfWork.DbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

        }


        public async Task<User> Update(User user)
        {
            userValidator.ValidateAndThrow(user);
            return await _unitOfWork.UserRepository.Update(user);
        }

        public async Task<bool> UserExist(string email)
        {
            if (await _unitOfWork.DbContext.Users.AnyAsync(x => x.Email == email))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyUserPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userPasswordHash[i])
                    {
                        return false;
                    }
                }

                return true;

            }
        }
    }
}
