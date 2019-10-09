using System;
using System.Collections.Generic;
using System.Text;

namespace golf.Core.DTO
{
    public class DTOLeaderBoard
    {
        public int Position { get; set; }
        public string TeamName { get; set; }
        public int Points { get; set; }
        public int Strokes { get; set; }
        public int HolesNotScored { get; set; }

    }
}
