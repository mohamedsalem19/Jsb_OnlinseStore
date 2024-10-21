using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Dtos;
using OnlineStore.Entities;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGRepository<Products> _product;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, IGRepository<Products> product)
        {
            _mapper = mapper;
            _product = product;
        }
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromForm] ProductToDto Dto)
        {
            var product = _mapper.Map<Products>(Dto);
            await _product.Add(product);
            return Ok("Product Added Successfly");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToDto>>> GetAllProducts()
        {
            var products = await _product.GetAllAsync();
            var productMap = _mapper.Map<IEnumerable<ProductToDto>>(products);
            return Ok(productMap);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToDto>> GetProductById(int id)
        {
            var product = await _product.GetByIdAsync(id);
            if (product == null)
                return NotFound("Product  Not Found");

            var productMap = _mapper.Map<ProductToDto>(product);
            return Ok(productMap);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromForm] ProductToDto dto)
        {
            if (dto == null || id < 0)
                return BadRequest("Invalid data");

            var productEntity = await _product.GetByIdAsync(id);
            if (productEntity == null)
                return NotFound("Product not found.");

            _mapper.Map(dto, productEntity);
            _product.Update(productEntity);
            return Ok("Product Updated Successfully");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var productEntity = await _product.GetByIdAsync(id);
            if (productEntity == null)
                return NotFound("Product not found.");

            _product.Delete(productEntity);
            return Ok("Product Deleted Successfully ");
        }
    }
}
