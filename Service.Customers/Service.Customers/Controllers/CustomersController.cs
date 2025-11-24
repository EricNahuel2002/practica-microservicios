using Microsoft.AspNetCore.Mvc;

namespace Service.Customers.Controllers;

public record CustomerDto(int Id, string Name, string Email);

[ApiController]
[Route("api/[controller]")]
public class CustomersController : Controller
{
    private static readonly List<CustomerDto> _customers = new()
        {
            new CustomerDto(1, "Ana Perez", "ana@example.com"),
            new CustomerDto(2, "Luis Gómez", "luis@example.com")
        };

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var c = _customers.FirstOrDefault(x => x.Id == id);
        return c is not null ? Ok(c) : NotFound();
    }
}
