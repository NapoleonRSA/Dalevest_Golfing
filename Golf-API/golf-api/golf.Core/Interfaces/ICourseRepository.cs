using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using golf.Core.DTO.CourseDTO_s;
using golf.Core.Models.Entities;

namespace golf.Core.Interfaces
{
    public interface ICourseRepository
    {
        Task AddNewCourse(DTONewCourse course);
    }
}
