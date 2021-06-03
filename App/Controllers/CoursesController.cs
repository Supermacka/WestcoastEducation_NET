using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using App.Data;
using App.Entities;
using App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    public class CoursesController : Controller
    {
        private readonly DataContext _context;

        public CoursesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _context.Courses.ToListAsync();
            return View("Index", result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCourseViewModel data)
        {
            if(!ModelState.IsValid) return View("Add", data);

            var course = new Course
            {
                CourseNumber = data.CourseNumber,
                Title = data.Title,
                Description = data.Description,
                Length = data.Length,
                Difficulty = data.Difficulty,
                Status = data.Status
            };

            _context.Courses.Add(course);
            var result = await _context.SaveChangesAsync();
            return RedirectToAction("Index", course);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            var courseModel = new EditCourseViewModel
            {
                CourseNumber = course.CourseNumber,
                Title = course.Title,
                Description = course.Description,
                Length = course.Length,
                Difficulty = course.Difficulty,
                Status = course.Status
            };

            return View("Edit", courseModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseViewModel data)
        {
            var course = await _context.Courses.FindAsync(data.Id);

            course.CourseNumber = data.CourseNumber;
            course.Title = data.Title;
            course.Description = data.Description;
            course.Length = data.Length;
            course.Difficulty = data.Difficulty;
            course.Status = data.Status;

            _context.Courses.Update(course);
            var result = await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        } 
    }
    
}