using System;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _repo;

        public CoursesController(ICourseRepository repo)
        {
            _repo = repo;
        }

        // Det skall gå att skapa nya kurser som lagras i en databas(SQLite)
        [HttpPost()]
        public async Task<ActionResult> AddCourse(Course course)
        {
            try
            {
                await _repo.AddAsync(course);

                if(await _repo.SaveAllChanges()) return StatusCode(201);

                return StatusCode(500, "something went wrong");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Det skall gå att hämta alla kurser
        [HttpGet()]
        public async Task<ActionResult> GetCourses()
        {
            var result  = await _repo.GetCoursesAsync();
            return Ok(result);
        }

        // Det skall gå att söka efter en kurs på kursens kursnummer
        [HttpGet("{courseNumber}")]
        public async Task<ActionResult> GetCourse(string courseNumber)
        {
            try
            {
                var course = await _repo.GetCourseByCourseNumberAync(courseNumber);

                if (course == null) return NotFound();

                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Det skall gå att ändra kurser i databasen
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(int id, Course courseModel)
        {
            var course = await _repo.GetCourseByIdAync(id);
            
            course.Title = courseModel.Title;
            course.Description = courseModel.Description;
            course.Length = courseModel.Length;
            course.Difficulty = courseModel.Difficulty;
            course.Status = courseModel.Status;

            _repo.Update(course);
            var result = await _repo.SaveAllChanges();
            return NoContent();
        }

        // Det skall gå att markera en kurs som pensionerad
        [HttpPut("{id}/{status}")]
        public async Task<ActionResult> SetStatus(int id, string status)
        {
            var course = await _repo.GetCourseByIdAync(id);
            course.Status = status;
        
            _repo.Update(course);
            var result = await _repo.SaveAllChanges();
            return NoContent();
        }
    }
}