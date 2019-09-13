using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using golf.Core.SeedWork;

namespace golf.Core.Models.Entities
{
    public class Course: BaseEntity
    {
        public string CourseName { get; set; }
        public List<Hole> Holes { get; set; }
    }
}
