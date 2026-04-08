using AutoMapper;
using FirstASPDotNetCoreWebAPIProject.DB;
using FirstASPDotNetCoreWebAPIProject.DTOs;
using FirstASPDotNetCoreWebAPIProject.Helpers;
using FirstASPDotNetCoreWebAPIProject.Models;
using FirstASPDotNetCoreWebAPIProject.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstASPDotNetCoreWebAPIProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly TestDbContext _context;
        private IMapper _mapper;
        public ProductsController(TestDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get()
        {
            //List<Product>? products = _context.Products.Include(p=>p.Category).ToList();

            //var products = _context.Products
            //    .Include(p => p.Category)
            //    .Select(p => new ProductDto
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        CategoryId = p.CategoryId,
            //        CategoryName = p.Category.Name
            //    })
            //    .ToList();

            /*throw new Exception("This is to test my custom error handling middleware"); */    //This line is to test my custom error handling middleware

            var products = _context.Products.Include(p=>p.Category).ToList();

            if (!products.Any())
                return NotFound(ApiResponse.Fail("No any product record found."));

            var result = _mapper.Map<List<ProductDto>>(products);

            return Ok(result);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {

            if (!int.TryParse(id, out int productId))
                return BadRequest(ApiResponse.Fail($"Id {id} is invalid product Id."));

            if (productId <= 0)
                return BadRequest(ApiResponse.Fail("Invalid Product Id is entered."));

            Product? product = _context.Products.Include(p=>p.Category).FirstOrDefault(e => e.Id == productId);

            if (product == null)
                return NotFound(ApiResponse.Fail($"Product with Id {productId} is not found."));

            var result = _mapper.Map<ProductDto>(product);

            return Ok(result);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateOrUpdateProductDTO createProduct)
        {
            if (string.IsNullOrWhiteSpace(createProduct.Name))
                return BadRequest(ApiResponse.Fail("Product name must be entered"));

            Product product = _mapper.Map<Product>(createProduct);

            _context.Products.Add(product);
            _context.SaveChanges();

            Product? productWithCategory = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == product.Id);

            var productDTO = _mapper.Map<ProductDto>(productWithCategory);

            return CreatedAtAction(nameof(Get), new { id = product.Id }, productDTO);
        }

        // POST api/<ProductsController>
        [HttpPost("bulk")]
        public IActionResult Post([FromBody] List<CreateOrUpdateProductDTO> products)
        {
            foreach (CreateOrUpdateProductDTO product in products)
                if (string.IsNullOrWhiteSpace(product.Name))
                    return BadRequest(ApiResponse.Fail("Product name must be entered"));

            var createProducts = _mapper.Map<List<Product>>(products);

            _context.Products.AddRange(createProducts);
            _context.SaveChanges();

            // Get the IDs of newly inserted products
            var newIds = createProducts.Select(p => p.Id).ToList();

            // Reload with Category included
            var productWithCategories = _context.Products
                .Include(p => p.Category)
                .Where(p => newIds.Contains(p.Id))
                .ToList();

            var productDtos = _mapper.Map<List<ProductDto>>(productWithCategories);
            return Ok(ApiResponse.Ok($"{products.Count} products have been added successfully", productDtos));
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] CreateOrUpdateProductDTO updateProductDTO)
        {
            if (!int.TryParse(id, out int productId))
                return BadRequest(ApiResponse.Fail($"Id {id} is invalid product Id."));

            if (productId == 0)
                return BadRequest(ApiResponse.Fail("Product Id must be a positive number."));


            if (string.IsNullOrWhiteSpace(updateProductDTO.Name))
                return BadRequest(ApiResponse.Fail("Product name must be entered."));

            Product? productToUpdate = _context.Products.FirstOrDefault(x => x.Id == productId);

            if (productToUpdate == null)
                return NotFound(ApiResponse.Fail($"Product with Id {productId} is not found."));

            _mapper.Map(updateProductDTO, productToUpdate);
            _context.Products.Update(productToUpdate);
            _context.SaveChanges();

            var updatedProductWithCategory = _context.Products.Include(p=>p.Category).FirstOrDefault(x => x.Id == productId);

            var result = _mapper.Map<ProductDto>(updatedProductWithCategory);

            return CreatedAtAction(nameof(Get), new { id = productToUpdate.Id }, result);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!int.TryParse(id, out int productId))
                return BadRequest(ApiResponse.Fail($"Id {id} is invalid product Id."));

            if (productId == 0)
                return BadRequest(ApiResponse.Fail($"Product Id must be a positive number."));

            Product? product = _context.Products.FirstOrDefault(e => e.Id == productId);

            if (product == null)
                return NotFound(ApiResponse.Fail($"Product with Id {productId} is not found."));

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok(ApiResponse.Ok($"Product with Id {productId} has been deleted successfully.", null));
        }
    }
}
