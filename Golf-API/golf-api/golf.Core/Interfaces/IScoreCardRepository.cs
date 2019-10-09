using System.Collections.Generic;
using System.Threading.Tasks;
using golf.Core.DTO;
using golf.Core.DTO.GameDTO_s;

namespace golf.Core.Interfaces
{
    public interface IScoreCardRepository
    {
        Task<bool> CreateNewGameByPlayerId(DTONewPlayerGame newPlayerGame);
        List<DTOLeaderBoard> GetLeaderBoard(int gameId);
        DTOLeaderBoard GetLeader(int gameId);
    }

}
