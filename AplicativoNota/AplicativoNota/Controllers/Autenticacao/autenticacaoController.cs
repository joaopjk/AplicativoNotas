using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.DTO;
using AutoMapper;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace AplicativoNota.Controllers.Autenticacao
{
    [Route("api/[controller]")]
    [ApiController]
    public class autenticacaoController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public autenticacaoController(IConfiguration config,
            UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        [HttpGet("GetUser")]
        [Authorize]
        public async Task<IActionResult> GetUser(AutenticaoRequest userDto)
        {
            return Ok(userDto);
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AutenticaoRequest userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);
                var userReturn = _mapper.Map<AutenticaoRequest>(user);
                if (result.Succeeded)
                {
                    return Created("GetUser", userReturn);
                }
                return BadRequest(result.Errors);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest userDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userDto.UserName);
                var result = await _signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);
                if (result.Succeeded)
                {
                    var appUser = await _userManager.Users
                        .FirstOrDefaultAsync(u => u.NormalizedUserName == userDto.UserName.ToUpper());
                    var userReturn = _mapper.Map<LoginRequest>(appUser);
                    return Ok(new
                    {
                        token = GenerateIwtToken(appUser).Result,
                        user = userReturn
                    });
                }
                return Unauthorized();
            }
            catch (Exception ex )
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<string> GenerateIwtToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHadler = new JwtSecurityTokenHandler();
            var token = tokenHadler.CreateToken(tokenDescriptor);

            return tokenHadler.WriteToken(token);
        }
    }
}