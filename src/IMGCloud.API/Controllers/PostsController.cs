﻿using IMGCloud.Domain.Cores;
using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace IMGCloud.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PostsController : ControllerBase
{
    private readonly IPostService _postService;


    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [Route("all-post")]
    public async Task<IActionResult> GetAllPostAsync(CancellationToken cancellationToken = default)
    {
        var respone = await _postService.GetAllPostsAsync(cancellationToken);
        return Ok(respone);

    }

    [HttpGet]
    [Route("id")]
    public async Task<IActionResult> GetPostByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var respone = await _postService.GetByIdAsync(id, cancellationToken);
        return Ok(respone);

    }

}
