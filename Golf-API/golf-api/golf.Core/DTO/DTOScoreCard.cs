using System;
using System.Collections.Generic;
using System.Text;

namespace golf.Core.DTO
{
    public class DTOScoreCard
    {
        public string Naam { get; set; }
        public int Points { get; set; }
        public int Strokes { get; set; }
        public int HolesLeft { get; set; }
    }
}
