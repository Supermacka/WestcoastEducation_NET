using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using App.Data;
using App.Entities;
using App.Interfaces;
using App.Models;
using App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseService _courseService;

        public CoursesController(IUnitOfWork unitOfWork, ICourseService courseService)
        {
            _unitOfWork = unitOfWork;
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var result = await _courseService.GetCoursesAsync();
            var result = await _courseService.GetSearchCoursesAsync();
            var model = new SearchCourseViewModel
            {
                Courses = result
            };
            return View("Index", model);
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

            var course = new CourseModel
            {
                CourseNumber = data.CourseNumber,
                Title = data.Title,
                Description = data.Description,
                Length = data.Length,
                Difficulty = data.Difficulty,
                Status = data.Status
            };

            try
            {
                if(await _courseService.AddCourse(course)) return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return View("Error", course);
            }
            
            return View("Error", course);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CourseModel course = await _courseService.GetCourseAsync(id);

            var model = new EditCourseViewModel
            {
                CourseNumber = course.CourseNumber,
                Title = course.Title,
                Description = course.Description,
                Length = course.Length,
                Difficulty = course.Difficulty,
                Status = course.Status
            };

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseViewModel model)
        {
            //Get current course model
            var currentCourseModel = await _courseService.GetCourseAsync(model.Id);

            // Create new Course of input values
            var courseModel = new CourseModel
            {
                CourseNumber = model.CourseNumber,
                Title = model.Title,
                Description = model.Description,
                Length = model.Length,
                Difficulty = model.Difficulty,
                Status = model.Status
            };

            try
            {
                if(await _courseService.UpdateCourse(currentCourseModel.CourseNumber, courseModel)) return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return View("Error");
            }
            
            return View("Error");
        } 

        [HttpGet]
        public async Task<IActionResult> Search(string courseNumber)
        {
            if (string.IsNullOrWhiteSpace(courseNumber))
            {
                //var result = await _courseService.GetCoursesAsync();
                var resultIndex = await _courseService.GetSearchCoursesAsync();
                var model = new SearchCourseViewModel
                {
                    Courses = resultIndex
                };
                return View("Index", model);                
            }
            
            // Get course from API
            try
            {
                var result = await _courseService.GetCourseByCourseNumberAsync(courseNumber);   

                SearchCourseViewModel courseModel = new SearchCourseViewModel()
                {
                    CourseNumber = result.CourseNumber,
                    Courses = new List<CourseModel>()
                };

                courseModel.Courses.Add(result);

                // Return View with result model
                return View("Index", courseModel);
            }
            catch (Exception)
            {
                return NoContent();
            }
            
        }

        [HttpGet]
         public async Task<IActionResult> Delete(int id)
         {
             var currentCourseModel = await _courseService.GetCourseAsync(id);

            try
            {
                if(await _courseService.DeleteCourse(currentCourseModel.CourseNumber)) return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return View("Error");
            }

            return View("Error");
         }
    }
    
}