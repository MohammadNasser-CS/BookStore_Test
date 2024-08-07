using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiExample.Dtos.Account;
using ApiExample.Interfaces;
using ApiExample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenServices _tokenServices;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, ITokenServices tokenServices, SignInManager<User> signInManager)
        {
            this._userManager = userManager;
            this._tokenServices = tokenServices;
            this._signInManager = signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var user = new User
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };
                var createdUser = await _userManager.CreateAsync(user, registerDto.Password!);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new
                            {
                                Message = "User created!",
                                UserName = user.UserName,
                                Email = user.Email,
                                Token = _tokenServices.createToken(user),
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.Users.FirstOrDefaultAsync(U => U.UserName!.ToLower() == loginDto.UserName!.ToLower());
            if (user == null) return Unauthorized("Invalid UserName");
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password!, false);
            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");
            return Ok(
                new
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenServices.createToken(user)
                }
            );
        }
    }
}