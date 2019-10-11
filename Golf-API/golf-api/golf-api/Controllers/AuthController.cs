using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using golf.Core.DTO;
using golf.Core.Models;
using golf.Core.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace golf.Core.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private golfdbContext dbContext { get; }
        private UserManager<IdentityUser> _userManager;
        private IPasswordHasher<IdentityUser> _hasher;
        public AuthController(golfdbContext dbContext, UserManager<IdentityUser> userManager, IPasswordHasher<IdentityUser> hasher)
        {
            this.dbContext = dbContext;
            _userManager = userManager;
            _hasher = hasher;
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO user)
        {
            //string email = user.Email;
            var identityUser = await _userManager.FindByNameAsync(user.Email);
            if (identityUser == null)
            {
                return BadRequest("Invalid client request");
            }
            var password = _hasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, user.Password);
            if (identityUser != null && password == PasswordVerificationResult.Success)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
               
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44314/",
                    audience: "https://localhost:44314/",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(180),
                    signingCredentials: signinCredentials

                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                var player = dbContext.Player.Where(p => p.Email == user.Email).First();


                return Ok(new { Token = tokenString , UserId = player.Id, Handicap = player.HandiCap, NameSurname = player.PlayerName + " " + player.LastName});
            }
            else
            {
                return BadRequest("Invalid client request");
            }
        }

        [AllowAnonymous]
        [HttpPost, Route("registerUser")]
        public async Task<IActionResult> Post([FromBody] PlayerDTO value)
        {
            try
            {
                var identityUser = await _userManager.FindByNameAsync(value.Email);
                if (identityUser != null)
                {
                    return Ok(new { msg = "User already exists!" });
                }

                if (identityUser == null)
                {
                    if (value.Email != "")
                    {
                        identityUser = new IdentityUser(value.Email);
                        var result = await _userManager.CreateAsync(identityUser, value.Password);
                        var player = await dbContext.Player.AddAsync(new Player()
                        {
                            PlayerName = value.PlayerName,
                            LastName = value.LastName,
                            Cellphone = value.CellPhone,
                            Email = value.Email,
                            HandiCap = 0,
                        });
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                return Ok(new { msg = e.Message });
            }

            return Ok(new { msg = "Success, please login." });
        }

    }
}
