using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController // this is a error controller
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret() //401 Unauthorized
        {
            return "secret text";
        }

        [HttpGet("not-found")] //404 not found
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1); // there is no user with the id of -1. Thus, it is going to throw an exception 
            if (thing == null) return NotFound();

            return thing;
        }

        [HttpGet("server-error")] //500 internal server error
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Users.Find(-1); // is null

            var thingToReturn = thing.ToString(); // throws System.NullReferenceException: Object reference not set to an instance of an object.
            return thingToReturn;

        }


        [HttpGet("bad-request")] //400 bad request
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request"); //this is going to return 400 error
        }




    }
}