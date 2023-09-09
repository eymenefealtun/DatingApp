using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] //https://localhost:5001/api/users
public class UsersController : Controller
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet] // endpoit  // Http request
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() // Task represents an asynchronous operation that can retun a value.
    {
        return await _context.Users.ToListAsync();
    }

    [HttpGet("{id}")] // endpoit     //https://localhost:5001/api/users/2
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        return await _context.Users.FindAsync(id);
    }

}
