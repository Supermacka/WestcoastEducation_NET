using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using App.Interfaces;
using App.Models;

namespace App.Services
{
    public class CourseService : ICourseService
    {
        public async Task<List<CourseModel>> GetCoursesAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:5001/api/courses");

                if (response.IsSuccessStatusCode) 
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<CourseModel>>(data, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return result;
                }
                else
                {
                    throw new Exception("Something went wrong.");
                }
            }
        }
    }
}