using System;
using System.Collections.Generic;
using System.Text;

namespace golf.Core.DTO.CourseDTO_s
{
    public class DTONewCourse
    {
        public string CourseName { get; set; }
        public List<DTONewCourseHole> Holes { get; set; }
    }
}
