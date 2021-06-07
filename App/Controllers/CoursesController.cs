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
            var result = await _courseService.GetCoursesAsync();
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

            _unitOfWork.CourseRepository.Add(course);

            if(await _unitOfWork.Complete()) return RedirectToAction("Index");

            return View("Error", course);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string courseNumber)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"https://localhost:5001/api/courses/{courseNumber}");

            CourseModel course = null;
            if(response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                course = JsonSerializer.Deserialize<CourseModel>(data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

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
            // Get course object
            var client = new HttpClient();
            var response = await client.GetAsync($"https://localhost:5001/api/courses/{model.CourseNumber}");

            CourseModel courseModel = null;
            if(response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                courseModel = JsonSerializer.Deserialize<CourseModel>(data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            // Update current object with new valuees
            courseModel.CourseNumber = model.CourseNumber;
            courseModel.Title = model.Title;
            courseModel.Description = model.Description;
            courseModel.Length = model.Length;
            courseModel.Difficulty = model.Difficulty;
            courseModel.Status = model.Status;

            response = await client.GetAsync($"https://localhost:5001/api/courses/{model.Id}");
            if(response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                JsonSerializer.Serialize(courseModel);
                return View("Index");
            }
            
            return View("Error");
        } 

        // public async Task<IActionResult> Delete(int id)
        // {
        //     var course = await _courseRepo.GetCourseByIdAsync(id);
        //     _courseRepo.Delete(course);

        //     if (await _courseRepo.SaveAllAsync()) return RedirectToAction("Index");
            
        //     return View("Error");
        // }
    }
    
}