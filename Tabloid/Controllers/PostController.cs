using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Tabloid.Models;
using Tabloid.Repositories;

namespace Tabloid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository; _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postRepository.GetAll());
        }

        [HttpGet("GetApprovedPastPosts")]
        public IActionResult GetPastApprovedPosts()
        {
            return Ok(_postRepository.GetAllPastApprovedPosts());
        }

        // Get post details by post id
        [HttpGet("{id}")]
        public IActionResult GetPublishedPostById(int id)
        {
            var post = _postRepository.GetPublishedPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        // Get posts by user profile
        [HttpGet("GetByUserId/{userId}")]
        public IActionResult GetPostByUserId(int userId)
        {
            return Ok(_postRepository.GetPostByUserId(userId));
        }

        // POST api/<PostController>
        [HttpPost("add")]
        public IActionResult Post(Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }
            _postRepository.Add(post);
            return CreatedAtAction("Get", new { id = post.Id }, post);
        }
    }
}
