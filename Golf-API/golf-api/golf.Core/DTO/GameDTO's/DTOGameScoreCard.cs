using System;
using System.Collections.Generic;
using System.Text;
using golf.Core.DTO.GameDTO_s;

namespace golf.Core.DTO
{
    public class DTOGameScoreCard
    {
        public List<DTOPlayerGameScore> PlayerScores  { get; set; }
    }
}
