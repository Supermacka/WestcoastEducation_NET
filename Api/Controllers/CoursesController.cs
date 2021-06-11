using System;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Interfaces;
using Api.DTOs;
using App.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Det skall gå att skapa nya kurser som lagras i en databas(SQLite)
        [HttpPost()]
        public async Task<ActionResult> AddCourse(CourseDto courseDto)
        {
            try
            {
                var course = new Course
                {
                    CourseNumber = courseDto.CourseNumber,
                    Title = courseDto.Title,
                    Description = courseDto.Description,
                    Length = courseDto.Length,
                    Difficulty = courseDto.Difficulty,
                    Status = courseDto.Status
                };
                await _unitOfWork.CourseRepository.AddAsync(course);

                if(await _unitOfWork.Complete()) return StatusCode(201);

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
            var courses  = await _unitOfWork.CourseRepository.GetCoursesAsync();
            var coursesDto = courses.Select(c => new CourseDto
            {
                CourseNumber = c.CourseNumber,
                Title = c.Title,
                Description = c.Description,
                Length = c.Length,
                Difficulty = c.Difficulty,
                Status = c.Status
            });

            return Ok(courses);
        }

        // Det skall gå att söka efter en kurs på kursens kursnummer
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCourseById(int id)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(id);
                var courseDto = new CourseDto
                {
                    CourseNumber = course.CourseNumber,
                    Title = course.Title,
                    Description = course.Description,
                    Length = course.Length,
                    Difficulty = course.Difficulty,
                    Status = course.Status
                };

                if (course == null) return NotFound();

                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("find/{courseNumber}")]
        public async Task<ActionResult> GetCourseByCourseNumber(string courseNumber)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseByCourseNumberAsync(courseNumber);
                var courseDto = new CourseDto
                {
                    CourseNumber = course.CourseNumber,
                    Title = course.Title,
                    Description = course.Description,
                    Length = course.Length,
                    Difficulty = course.Difficulty,
                    Status = course.Status
                };

                if (course == null) return NotFound();

                return Ok(course);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // Det skall gå att ändra kurser i databasen
        [HttpPut("{courseNumber}")]
        public async Task<ActionResult> UpdateCourse(string courseNumber, CourseDto courseDto)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseByCourseNumberAsync(courseNumber);
            
            course.CourseNumber = courseDto.CourseNumber;
            course.Title = courseDto.Title;
            course.Description = courseDto.Description;
            course.Length = courseDto.Length;
            course.Difficulty = courseDto.Difficulty;
            course.Status = courseDto.Status;

            _unitOfWork.CourseRepository.Update(course);
            var result = await _unitOfWork.Complete();
            return NoContent();
        }

        [HttpDelete("{courseNumber}")]
        public async Task<ActionResult> DeleteCourse(string courseNumber)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseByCourseNumberAsync(courseNumber);
            
            _unitOfWork.CourseRepository.Delete(course);
            var result = await _unitOfWork.Complete();
            return NoContent();
        }
    }
}