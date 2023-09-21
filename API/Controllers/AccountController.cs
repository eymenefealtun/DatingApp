using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    // POST: api/account/register?username=sam&password=123  //this part is going to be updated because it not suitable to use query string like this
    // api/account/register  //we are going to send the username and password for register inside of the body of the request
    [HttpPost("register")] //endpoint // we're creating a resourceon the server that's why this is a HttpPost
    // public async Task<ActionResult<AppUser>> Register([FromBody]RegisterDto registerDto)
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username))
            return BadRequest("Username is taken");

        using var hmac = new HMACSHA512(); //this random key is going to be our password salt

        var user = new AppUser
        {
            UserName = registerDto.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key // hmac.Key gets the reandom key
        };
        _context.Add(user);
        await _context.SaveChangesAsync();
        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user),
        };

    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

        if (user == null)
            return Unauthorized("invalid username");// this is a HTTP response and we need ActionResult in method in order to use HTTP repsonse

        using var hmac = new HMACSHA512(user.PasswordSalt); // this returns a byte[]

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
            if (computedHash[i] != user.PasswordHash[i])
                return Unauthorized("invalid password");

        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user),
        };
    }




    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        // return await _context.Users.AnyAsync(x => x.UserName == username);
    }


}
