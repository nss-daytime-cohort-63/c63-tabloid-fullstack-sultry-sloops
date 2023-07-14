using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
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

    }
}

//need to complete CRUD