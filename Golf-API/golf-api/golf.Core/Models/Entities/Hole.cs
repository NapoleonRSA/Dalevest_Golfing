using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using golf.Core.SeedWork;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace golf.Core.Models.Entities
{
    public class Hole: BaseEntity
    {
        [Range(1,18)]
        public int hole_nr { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [Range(1,6)]
        public int Par { get; set; }
        [Range(1,18)]
        public int Stroke { get; set; }

    }
}
