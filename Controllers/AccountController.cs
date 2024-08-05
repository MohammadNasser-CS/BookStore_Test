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
using Microsoft.Extensions.Logging;

namespace ApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenServices tokenServices;

        public AccountController(UserManager<User> userManager, ITokenServices tokenServices)
        {
            this.userManager = userManager;
            this.tokenServices = tokenServices;
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
                var createdUser = await userManager.CreateAsync(user, registerDto.Password!);
                if (createdUser.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new
                            {
                                Message = "User created!",
                                UserName = user.UserName,
                                Email = user.Email,
                                Token = tokenServices.createToken(user),
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
    }
}