using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tabloid.Repositories;
using Tabloid.Models;

namespace Tabloid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostTagController : ControllerBase
    {
        private readonly IPostTagRepository _postTagRepository;
        public PostTagController(IPostTagRepository postTagRepository)
        {
            _postTagRepository = postTagRepository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postTagRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetPostTagsByPostId(int id)
        {
            var post = _postTagRepository.GetAllByPostId(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }
        
        [HttpPost]
        public IActionResult Post(PostTag postTag)
        {
            _postTagRepository.AddPostTag(postTag);
            return CreatedAtAction(nameof(Get), new { id = postTag.Id }, postTag);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _postTagRepository.DeletePostTag(id);
            return NoContent();
        }

    }
}
