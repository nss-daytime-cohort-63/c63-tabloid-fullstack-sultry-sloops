using Microsoft.AspNetCore.Mvc;
using Tabloid.Models;
using Tabloid.Repositories;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tabloid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        // POST api/<CommentController>
        [HttpPost("add")]
        public IActionResult Post(Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }
            _commentRepository.Add(comment);
            return CreatedAtAction("Get", new { id = comment.Id }, comment);
        }

        // PUT api/<CommentController>/5
        [HttpPut("update/{id}")]
        public IActionResult Put(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }
            _commentRepository.Update(comment);
            return NoContent();
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _commentRepository.Delete(id);
            return NoContent();
        }
    }
}
