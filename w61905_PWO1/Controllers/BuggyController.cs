using Microsoft.AspNetCore.Mvc;

namespace w61905_PWO1.Controllers;

public class BuggyController : BaseApiController
{
    [HttpGet("not-found")]
    public ActionResult GetNotFound()
    {
        return NotFound();
    }
    
    [HttpGet("bad-request")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ProblemDetails{Title = "Błąd żądania do serwera"});
    }
    
    [HttpGet("unauthorised")]
    public ActionResult GetUnauthorised()
    {
        return Unauthorized();
    }
    
    [HttpGet("validation-error")]
    public ActionResult GetValidationError()
    {
        ModelState.AddModelError("Problem1", "To jest pierwszy błąd");
        ModelState.AddModelError("Problem2", "To jest drugi błąd");
        return ValidationProblem();
    }
    
    [HttpGet("server-error")]
    public ActionResult GetServerError()
    {
        throw new Exception("Błąd serwerowy");
    }
}