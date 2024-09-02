using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public IActionResult getAllStudent()
        {
            string[] students = new string[] { "Anton", "Virat", "Dhoni" };

            return Ok(students);
        }
    }
}
