﻿using IMGCloud.Domain.Cores;
using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace IMGCloud.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserByIdAsync(string userName)
    {
        var data = await _userService.GetUserDetailByUserNameAsync(userName);
        return Ok(new ApiResult<UserDetail>()
        {
            Context = data,
            IsSucceeded = true
        });
    }
}
