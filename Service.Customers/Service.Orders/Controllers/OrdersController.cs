using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Service.Orders.Controllers
{
    public record OrderDto(int Id, int CustomerId, decimal Amount);
    public record CustomerDto(int Id, string Name, string Email);
    public record OrderWithCustomerDto(int Id, decimal Amount, CustomerDto Customer);

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static readonly List<OrderDto> _orders = new()
        {
            new OrderDto(1, 1, 99.95m),
            new OrderDto(2, 2, 15.50m)
        };

        private readonly IHttpClientFactory _httpClientFactory;
        public OrdersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order is null) return NotFound();

            var client = _httpClientFactory.CreateClient("customers");
            var resp = await client.GetAsync($"/api/customers/{order.CustomerId}");
            if (!resp.IsSuccessStatusCode) return StatusCode((int)resp.StatusCode, "Error calling customers service");

            using var stream = await resp.Content.ReadAsStreamAsync();
            var customer = await JsonSerializer.DeserializeAsync<CustomerDto>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var result = new OrderWithCustomerDto(order.Id, order.Amount, customer!);
            return Ok(result);
        }
    }
}
