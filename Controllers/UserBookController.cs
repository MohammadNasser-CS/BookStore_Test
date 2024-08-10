using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiExample.Interfaces;
using ApiExample.Mapper;
using ApiExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBookController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserBookRepository _userBookRepository;

        public UserBookController(UserManager<User> userManager, IUserBookRepository userBookRepository)
        {
            this._userBookRepository = userBookRepository;
            this._userManager = userManager;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserBooks()
        {
            var userName = User.FindFirstValue(ClaimTypes.GivenName);
            if (userName == null) return Unauthorized("User UnAuthenticated");
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return Unauthorized("Invalid User");
            var userBooks = await _userBookRepository.GetUserBooks(user);
            var booksDto = userBooks.Select(S => S.ToBookDto());
            return Ok(new { userName = userName, books = booksDto });
        }
    }
}