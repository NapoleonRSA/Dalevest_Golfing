using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using golf.Core.DTO.CourseDTO_s;
using golf.Core.Interfaces;
using golf.Core.Models;
using golf.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace golf.Core.Repositories
{
    public class CourseRepository: ICourseRepository
    {
        private readonly golfdbContext _context;

        public CourseRepository(golfdbContext context)
        {
            _context = context;
        }

        public async Task AddNewCourse(DTONewCourse course)
        {
            try
            {
               var courseId =  await _context.Course.AddAsync(course);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
