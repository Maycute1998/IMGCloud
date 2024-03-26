using IMGCloud.Domain.Cores;
using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Requests;
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
    private readonly ICollectionService _collectionService;


    public PostsController(IPostService postService, ICollectionService collectionService)
    {
        _postService = postService;
        _collectionService = collectionService;
    }

    [HttpGet]
    [Route("all-collections")]
    public async Task<IActionResult> GetAllCollectionsAsync(CancellationToken cancellationToken = default)
    {
        var respone = await _collectionService.GetAllCollectionsAsync(cancellationToken);
        return Ok(respone);

    }

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [Route("all-posts")]
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

    [HttpGet]
    [Route("collection-id")]
    public async Task<IActionResult> GetPostByCollectionIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var respone = await _postService.GetByCollectionIdAsync(id, cancellationToken);
        return Ok(respone);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostRequest post, CancellationToken cancellationToken = default)
    {
        await _postService.CreateAsync(post, true, cancellationToken);
        return Ok();
    }

}
