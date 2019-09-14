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
                await _courseRepository.AddNewCourse(course);

                if ()
                    return Ok(new { msg = result.Message });
                return new BadRequestObjectResult(new { msg = result.Message });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
