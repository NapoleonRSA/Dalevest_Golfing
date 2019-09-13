using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace golf.Core.Models.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string CourseName { get; set; }
    }
}
