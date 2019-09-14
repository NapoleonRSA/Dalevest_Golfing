using System.Threading.Tasks;
using golf.Core.DTO.GameDTO_s;

namespace golf.Core.Interfaces
{
    public interface IScoreCardRepository
    {
        Task<bool> CreateNewGameByPlayerId(DTONewPlayerGame newPlayerGame);
    }
}
