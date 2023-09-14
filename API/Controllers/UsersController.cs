using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// [ApiController]  // We no longer need this part aftrer we created our baseapicontroller and derived from it 
// [Route("api/[controller]")] //https://localhost:5001/api/users
[Authorize] //if user wants to use the below methods they're only be allowed only if they're signed in 
public class UsersController : BaseApiController
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }
    [AllowAnonymous] //Although we used [Authorize] above if we set [AllowAnonymous] to this method it is also going to be shown to non-signed users. (You can not AllowAnonymous at the highest level and then try to set [Authorize] to below methods)
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
