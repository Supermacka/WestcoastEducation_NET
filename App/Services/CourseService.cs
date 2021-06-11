using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using App.Interfaces;
using App.Models;
using App.ViewModels;
using Microsoft.Extensions.Configuration;

namespace App.Services
{
    public class CourseService : ICourseService
    {
        private readonly string _baseUrl;
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _options;

        public CourseService(IConfiguration config, HttpClient http)
        {
            _baseUrl = config.GetSection("api:baseUrl").Value + "courses";
            _http = http;

            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<bool> AddCourse(CourseModel model)
        {
            try
            {
                var url = _baseUrl;
                var data = JsonSerializer.Serialize(model);

                var response = await _http.PostAsync(url, new StringContent(data, Encoding.Default, "application/json"));
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var error = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCourse(int id)
        {
            try
            {
                var response = await _http.GetAsync($"{_baseUrl}/delete/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return true;    
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CourseModel> GetCourseAsync(int id)
        {
            var response = await _http.GetAsync($"{_baseUrl}/{id}");

            if(response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CourseModel>(data, _options);

                return result;
            }
            else
            {
                throw new Exception("Something went wrong.");
            } 
        }

        public async Task<CourseModel> GetCourseByCourseNumberAsync(string courseNumber)
        {
            var response = await _http.GetAsync($"{_baseUrl}/find/{courseNumber}");

            if (response.IsSuccessStatusCode) 
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CourseModel>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Something went wrong.");
            }
        }

        public async Task<List<CourseModel>> GetCoursesAsync()
        {
            var response = await _http.GetAsync($"{_baseUrl}");

            if (response.IsSuccessStatusCode) 
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<CourseModel>>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Something went wrong.");
            }
        }

        public async Task<List<CourseModel>> GetSearchCoursesAsync()
        {
            var response = await _http.GetAsync($"{_baseUrl}");

            var data = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<CourseModel>>(data, _options);
            return result;
        }

        public async Task<bool> UpdateCourse(string courseNumber, CourseModel model)
        {
            try
            {
                var url = $"{_baseUrl}/{courseNumber}";
                var data = JsonSerializer.Serialize(model);
                var response = await _http.PutAsync(url, new StringContent(data, Encoding.Default, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return true;    
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

//'CourseService' does not implement interface member 'ICourseService.GetSearchCoursesAsync()'. 'CourseService.GetSearchCoursesAsync()' cannot implement 'ICourseService.GetSearchCoursesAsync()' because it does not have the matching return type of 'Task<List<CourseModel>>'. [C:\Users\Colin\.vscode\.NET\WestcoastEducation_NET\App\App.csproj]