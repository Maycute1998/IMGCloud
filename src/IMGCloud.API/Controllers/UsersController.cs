﻿using IMGCloud.Domain.Cores;
using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace IMGCloud.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var data = await _userService.GetUserByIdAsync(id, cancellationToken);
        return Ok(new ApiResult<UserDetail>()
        {
            Context = data,
            IsSucceeded = true,
        });
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateUserInfoAsync([FromBody]UserDetailsRequest userInfo, CancellationToken cancellationToken = default)
    {
        var respone = await _userService.CreateUserInfoAsync(userInfo, cancellationToken);
        return respone.IsSucceeded ? Ok(new ApiResult<UserDetail>() {Message = respone.Message, IsSucceeded = respone.IsSucceeded }) : BadRequest();
    }
}
