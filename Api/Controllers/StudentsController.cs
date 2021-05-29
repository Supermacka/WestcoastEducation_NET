using Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        public StudentsController()
        {
        }

        [HttpPost()]
        public ActionResult AddStudent(Student student)
        {
            return Ok(student);
        }
    }
}