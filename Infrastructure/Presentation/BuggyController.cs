using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase 
    {
        [HttpGet("notfound")] //Get/api/Buggy/notfound
        public IActionResult GetNotFoundRequest()
        {
            //code
            return NotFound(); //Status code : 404
        }

        [HttpGet("servererror")] //Get/api/Buggy/servererror
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception();
            return Ok();
        }

        [HttpGet("badrequest")] //Get/api/Buggy/badrequest
        public IActionResult GetBadRequest()
        {
            //code
            return BadRequest(); //400
        }
        
        [HttpGet("badrequest/{id}/{age}")] //Get/api/Buggy/badrequest/ahmed
        public IActionResult GetBadRequest(int id ,int age) //Validation error
        {
            //code
            return BadRequest(); //400
        }

        [HttpGet("unauthorized")] //Get/api/Buggy/unauthorized
        public IActionResult GetUnauthorizedRequest () //Validation error
        {
            //code
            return Unauthorized(); //401
        }
    }
}
