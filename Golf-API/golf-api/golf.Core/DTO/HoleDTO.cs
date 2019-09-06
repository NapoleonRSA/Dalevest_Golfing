using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace golf.Core.DTO
{
    public class HoleDTO
    {
        public int hole_nr { get; set; }
        public int Score { get; set; }
        public int Strokes { get; set; }
        public int PlayerId { get; set; }
    }
}
