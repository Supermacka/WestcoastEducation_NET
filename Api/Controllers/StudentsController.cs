using System;
using System.Threading.Tasks;
using Api.Entities;
using Api.Interfaces;
using App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Deltagare skall kunna registrera sig och uppgifterna skall lagras i databasen.
        [HttpPost()]
        public async Task<ActionResult> AddStudent(Student student)
        {
            try
            {
                await _unitOfWork.StudentRepository.AddAsync(student);
                
                if (await _unitOfWork.Complete()) return StatusCode(201);

                return StatusCode(500, "something went wrong");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Deltagare skall kunna uppdatera sina uppgifter
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent(int id, Student studentModel)
        {
            var student = await _unitOfWork.StudentRepository.GetStudentById(id);
            
            student.FirstName = studentModel.FirstName;
            student.LastName = studentModel.LastName;
            student.Email = studentModel.Email;
            student.PhoneNumber = studentModel.PhoneNumber;
            student.Adress = studentModel.Adress;

            _unitOfWork.StudentRepository.Update(student);
            var result = await _unitOfWork.Complete();
            return NoContent();
        }

        // Det skall gå att lista alla deltagare
        [HttpGet()]
        public async Task<ActionResult> GetStudents()
        {
            var students = await _unitOfWork.StudentRepository.GetStudentsAsync();
            return Ok(students);
        }

        // Det skall gå söka efter deltagare på e-post adress
        [HttpGet("{email}")]
        public async Task<ActionResult> GetStudent(string email)
        {
            try
            {
                var student = await _unitOfWork.StudentRepository.GetStudentByEmailAsync(email);

                if(student == null) return NotFound();
                
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}