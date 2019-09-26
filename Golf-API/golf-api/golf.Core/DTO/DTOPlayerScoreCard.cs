using System;
using System.Collections.Generic;
using System.Text;

namespace golf.Core.DTO
{
    public class DTOPlayerScoreCard
    {
        public int hole_nr { get; set; }
        public int Score { get; set; }
        public int Points { get; set; }
        public int holeId { get; set; }
        public int par { get; set; }
        public int stroke { get; set; }
    }
}
