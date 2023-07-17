using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using Tabloid.Models;
using Tabloid.Repositories;



namespace Tabloid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository) {

            _categoryRepository = categoryRepository;

        }
        // GET: api/<CategoryController>
        [HttpGet]
        public IActionResult Get()
        {
            var categories = _categoryRepository.GetAll();

            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }
        // POST api/<TagController>
        [HttpPost("add")]
        public IActionResult Post(Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            _categoryRepository.AddCategory(category);
            return CreatedAtAction("Get", new { id = category.Id }, category);
        }
        
    }

}

//need to complete CRUD !!!