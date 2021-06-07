using App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepo;

        public StudentsController(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}