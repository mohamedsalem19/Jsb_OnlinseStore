using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Dtos;
using OnlineStore.Entities;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IGRepository<Orders> _order;
        private readonly IMapper _mapper;
        private readonly IGRepository<Products> _product;

        public OrderController(IGRepository<Orders> order, IMapper mapper, IGRepository<Products> product)
        {
            _order = order;
            _mapper = mapper;
            _product = product;
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder([FromForm] OrderToDto dto)
        {
            if (dto == null)
                return BadRequest("No Data");

            var order = new Orders
            {
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.Now,
            };

            foreach (var productId in dto.ProductId)
            {
                var orderProduct = new OrdersProducts
                {
                    ProductId = productId,
                    OrderId = order.Id,
                };
                order.OrdersProducts.Add(orderProduct);
            }
            var products = await _product.GetAllAsync();
            var productPrice = products.Where(x => dto.ProductId.Contains(x.Id)).Select(x => x.Price).ToList();
            productPrice.ForEach(x => order.TotalAmount += x);

            await _order.Add(order);

            return Ok("Order Added Successfully");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToDto>>> GetAllOrders()
        {
            var orders = await _order.GetAllAsync();
            var orderMap = _mapper.Map<IEnumerable<GetAllOrderDto>>(orders);

            return Ok(orderMap);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToDto>> GetOrderById(int id)
        {
            var order = await _order.GetByIdAsync(id);
            if (order == null)
                return NotFound("not found");

            var orderMap = _mapper.Map<GetAllOrderDto>(order);
            return Ok(orderMap);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromQuery] OrderToDto dto)
        {
            if (dto == null)
                return BadRequest("No Data");

            var existingOrder = await _order.GetByIdAsync(id);
            if (existingOrder == null)
                return NotFound("not found");

            existingOrder.CustomerId = dto.CustomerId;
            existingOrder.OrderDate = DateTime.UtcNow;
            existingOrder.OrdersProducts.Clear();
            foreach (var productId in dto.ProductId)
            {
                var orderProduct = new OrdersProducts
                {
                    ProductId = productId,
                    OrderId = existingOrder.Id
                };
                existingOrder.OrdersProducts.Add(orderProduct);
            }

            var products = await _product.GetAllAsync();
            var productPrices = products.Where(x => dto.ProductId.Contains(x.Id)).Select(x => x.Price).ToList();
            existingOrder.TotalAmount = productPrices.Sum();

            _order.Update(existingOrder);

            return Ok("Order Updated Successfully");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var existingOrder = await _order.GetByIdAsync(id);
            if (existingOrder == null)
                return NotFound("not found");

            _order.Delete(existingOrder);
            return Ok("Order deleted successfully.");
        }
    }
}
