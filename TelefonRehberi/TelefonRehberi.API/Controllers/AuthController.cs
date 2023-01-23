using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TelefonRehberi.API.token;
using TelefonRehberi.Business.Abstract;
using TelefonRehberi.Business.Dtos;
using TelefonRehberi.Entities;

namespace TelefonRehberi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private IConfiguration _configuration;

        public AuthController(IAuthService authService, ILogger<AuthController> logger,IConfiguration configuration)
        {
            _authService = authService;
            _logger = logger;
            _configuration = configuration;
        }

        

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto userCreateDto)
        {
            try
            {
                if(await _authService.UserExist(userCreateDto.Email))
                {
                    throw new Exception(userCreateDto.Email + " already exist!");
                }

                User createdUser = await _authService.Register(userCreateDto);
                return Ok();

            }
            catch (Exception e)
            {
                _logger.LogError("ERROR : " + e.Message);
                return BadRequest(e.Message);
            }


        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            try
            {
                var user = await _authService.Login(userDto);
                if (user == null)
                {
                    
                    _logger.LogError("Unauthorized");
                    return Unauthorized();
                }
                //Token üretiliyor.
                MyTokenHandler tokenHandler = new MyTokenHandler(_configuration);
                MyToken token = tokenHandler.CreateAccessToken(user);

               //Refresh token Users tablosuna işleniyor.
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.Expiration.AddMinutes(5);

                await _authService.Update(user);


                return Ok(token);

            }
            catch (Exception e)
            {
                _logger.LogError("ERROR : " + e.Message);
                return BadRequest(e.Message);
            }


        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] string refreshToken)
        {

            try
            {
                User user = await _authService.GetUserByRefreshToken(refreshToken);

                if (user == null || user.RefreshTokenEndDate < DateTime.Now)
                {
                    throw new Exception("Refresh Token Not Found!");
                }

                MyTokenHandler tokenHandler = new MyTokenHandler(_configuration);
                MyToken token = tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.Expiration.AddMinutes(5);

                await _authService.Update(user);

                return Ok(token);

            }
            catch (Exception e)
            {
                _logger.LogError("ERROR : " + e.Message);
                return BadRequest(e.Message);
            }

            
        }



    }
}
