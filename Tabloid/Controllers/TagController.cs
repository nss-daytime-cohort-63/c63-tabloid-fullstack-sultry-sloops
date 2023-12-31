﻿using Microsoft.AspNetCore.Mvc;
using Tabloid.Repositories;
using Tabloid.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tabloid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        // GET: api/<TagController>
        [HttpGet]
        public IActionResult Get()
        {
            var tags = _tagRepository.GetAll();
            if (tags == null)
            {
                return NotFound();
            }
            return Ok(tags);
        }

        // GET api/<TagController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TagController>
        [HttpPost("add")]
        public IActionResult Post(Tag tag)
        {
            if (tag == null)
            {
                return BadRequest();
            }
            _tagRepository.Add(tag);
            return CreatedAtAction("Get", new { id = tag.Id }, tag);
        }

        // PUT api/<TagController>/5
        [HttpPut("update/{id}")]
        public IActionResult Put(int id, Tag tag)
        {
            if (tag.Id != id)
            {
                return BadRequest();
            }
            _tagRepository.Update(tag);
            return NoContent();
        }

        // DELETE api/<TagController>/5
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _tagRepository.Delete(id);
            return NoContent();
        }
    }
}
