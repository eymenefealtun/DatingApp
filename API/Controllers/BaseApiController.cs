using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController] //If we didn't use this ApiController, we'd have to use validation inside of our Register method instead of adding validation into DTOs.
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
 

}
