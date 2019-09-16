using System;
using System.Collections.Generic;
using System.Text;

namespace golf.Core.DTO.GameDTO_s
{
    public class DTONewGame
    {
        public string GameName  { get; set; }
        public string GamePassword { get; set; }
        public int  CourseId { get; set; }
    }
}
