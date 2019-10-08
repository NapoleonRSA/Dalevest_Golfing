using golf.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace golf.Core.Interfaces
{
    public interface ITeamRepository
    {
     Task<int> CreateNewTeam(DTONewTeam team);
     Task<bool> AddPlayerToTeam(int PlayerId, int TeamId);
     Task<bool> RemovePlayerFromTeam(int PlayerId, int TeamId);
        List<DTOTeam> GetTeamsForGame(int GameId);
        DTOTeam GetTeam(int teamId);
    }
}
