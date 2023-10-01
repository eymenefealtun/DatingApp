using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// [ApiController]  // We no longer need this part aftrer we created our baseapicontroller and derived from it 
// [Route("api/[controller]")] //https://localhost:5001/api/users
[Authorize] //if user wants to use the below methods they're only be allowed only if they're signed in 
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    // private readonly DataContext _context;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        // _context = context;
    }
    // [AllowAnon ymous] //Although we used [Authorize] above if we set [AllowAnonymous] to this method it is also going to be shown to non-signed users. (You can not AllowAnonymous at the highest level and then try to set [Authorize] to below methods)
    [HttpGet] // endpoit  // Http request
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() // Task represents an asynchronous operation that can retun a value.
    {
        // return await _context.Users.ToListAsync();
        var users = await _userRepository.GetMembersAsync();
        // var usersToREturn = _mapper.Map<IEnumerable<MemberDto>>(users);

        // return Ok(usersToREturn);
        return Ok(users);
    }

    [HttpGet("{username}")] // endpoit     //https://localhost:5001/api/users/2
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        return await _userRepository.GetMemberAsync(username);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // var username = User.Identity.Name; //another way of doing the above thing
        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null) return NotFound();

        _mapper.Map(memberUpdateDto, user); //this line of code is going to update the user. Note: nothing is saved into database here yet just updated the user from memberUpdateDto.


        if (await _userRepository.SaveAllAsync()) return NoContent(); //returns 201 code

        return BadRequest("Failed to update user"); 
    }

}
