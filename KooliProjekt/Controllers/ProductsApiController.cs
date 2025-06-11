using KooliProjekt.Data;
using KooliProjekt.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ApplicationDbContext _dbContext;

        public ProductsApiController(IProductService service, ApplicationDbContext dbContext)
        {
            _service = service;
            _dbContext = dbContext;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _service.GetAllProductsAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            product.Category = await _dbContext.ProductCategories.FindAsync(product.CategoryId);
            if (product.Category == null)
                return BadRequest(new { error = "CategoryId is invalid" });
            await _service.CreateProductAsync(product);
            return Ok(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            if (id != product.Id)
                return BadRequest();
            var existing = await _service.GetProductByIdAsync(id);
            if (existing == null)
                return NotFound();
            product.Category = await _dbContext.ProductCategories.FindAsync(product.CategoryId);
            if (product.Category == null)
                return BadRequest(new { error = "CategoryId is invalid" });
            await _service.UpdateProductAsync(product);
            return Ok();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            await _service.DeleteProductAsync(id);
            return Ok();
        }
    }
} 