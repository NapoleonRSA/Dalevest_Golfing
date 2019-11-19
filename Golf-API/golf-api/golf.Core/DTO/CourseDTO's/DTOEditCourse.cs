using System;
using System.Collections.Generic;

namespace golf.Core.DTO.CourseDTO_s
{
    public class DTOEditCourse
    {
        public int Id { get; set; }  
        public string CourseName { get; set; }
        public List<DTONewCourseHole> Holes { get; set; }
        
    }
}
