using AutoMapper;
using FirstASPDotNetCoreWebAPIProject.DB;
using FirstASPDotNetCoreWebAPIProject.DTOs;
using FirstASPDotNetCoreWebAPIProject.Helpers;
using FirstASPDotNetCoreWebAPIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstASPDotNetCoreWebAPIProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly TestDbContext _context;
        private IMapper _mapper;
        public CategoriesController(TestDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Category>? categories = _context.Categories.ToList();
            if (!categories.Any())
                return NotFound(ApiResponse.Fail("No any category record found."));

            return Ok(_context.Categories.ToList());
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {

            if (!int.TryParse(id, out int categoryId))
                return BadRequest(ApiResponse.Fail($"Id {id} is invalid category Id."));

            if (categoryId <= 0)
                return BadRequest(ApiResponse.Fail("Invalid Category Id is entered."));

            Category? category = _context.Categories.FirstOrDefault(e => e.Id == categoryId);

            if (category == null)
                return NotFound(ApiResponse.Fail($"Category with Id {categoryId} is not found."));

            return Ok(category);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateUpdateCategoryDTO createCategory)
        {
            if (string.IsNullOrWhiteSpace(createCategory.Name))
                return BadRequest(ApiResponse.Fail("Category name must be entered"));

            Category category = _mapper.Map<Category>(createCategory);
            _context.Categories.Add(category);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
        }

        // POST api/<CategoriesController>
        [HttpPost("bulk")]
        public IActionResult Post([FromBody] List<CreateUpdateCategoryDTO> createCategories)
        {
            List<Category> categories = new List<Category>();

            foreach (CreateUpdateCategoryDTO createCategory in createCategories)
            {
                if (string.IsNullOrWhiteSpace(createCategory.Name))
                    return BadRequest(ApiResponse.Fail("Category name must be entered"));

                Category category = _mapper.Map<Category>(createCategory); 
                
                categories.Add(category);
            }


            _context.Categories.AddRange(categories);
            _context.SaveChanges();

            return Ok(ApiResponse.Ok($"{categories.Count} categories have been added successfully", categories));
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] CreateUpdateCategoryDTO updateCategory)
        {
            if (!int.TryParse(id, out int categoryId))
                return BadRequest(ApiResponse.Fail($"Id {id} is invalid category Id."));

            if (categoryId == 0)
                return BadRequest(ApiResponse.Fail("Category Id must be a positive number."));


            if (string.IsNullOrWhiteSpace(updateCategory.Name))
                return BadRequest(ApiResponse.Fail("Category name must be entered."));

            Category? categoryToUpdate = _context.Categories.FirstOrDefault(x => x.Id == categoryId);

            if (categoryToUpdate == null)
                return NotFound(ApiResponse.Fail($"Category with Id {categoryId} is not found."));

            categoryToUpdate = _mapper.Map<Category>(updateCategory);

            _context.Categories.Update(categoryToUpdate);

            return CreatedAtAction(nameof(Get), new { id = categoryToUpdate.Id }, categoryToUpdate);
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!int.TryParse(id, out int categoryId))
                return BadRequest(ApiResponse.Fail($"Id {id} is invalid category Id."));

            if (categoryId == 0)
                return BadRequest(ApiResponse.Fail($"Category Id must be a positive number."));

            Category? category = _context.Categories.FirstOrDefault(e => e.Id == categoryId);

            if (category == null)
                return NotFound(ApiResponse.Fail($"Category with Id {categoryId} is not found."));

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok(ApiResponse.Ok($"Category with Id {categoryId} has been deleted successfully.", null));
        }
    }
}
