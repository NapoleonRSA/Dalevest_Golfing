using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using golf.Core.DTO.CourseDTO_s;
using golf.Core.Interfaces;
using golf.Core.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace golf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpPost, Route("AddNewCourse")]
        public async Task<IActionResult> AddNewCourse([FromBody] DTONewCourse course)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new BadRequestObjectResult(new { msg = "Invalid model" });
                
                if (await _courseRepository.AddNewCourse(course))
                    return Ok(new { msg = "New Course Added" });
                return new BadRequestObjectResult(new { msg = "Could Not Add Course" });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [HttpGet, Route("GetAllCourses")]
        public async Task<IActionResult> GetAllGames()
        {
            try
            {
                var courseList = await _courseRepository.GetAllCourses();
                return Ok(courseList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
