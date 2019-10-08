using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using golf.Core.DTO.GameDTO_s;
using golf.Core.Models.Entities;

namespace golf.Core.Interfaces
{
    public interface IGameRepository
    {
        Task<bool> CreateNewGame(DTONewGame game);
        Task<List<Game>> GetAllGames();
        Task<List<GameType>> GetAllGameTypes();

    }
}
