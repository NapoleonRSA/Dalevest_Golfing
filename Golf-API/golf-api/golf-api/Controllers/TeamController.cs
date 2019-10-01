using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using golf.Core.DTO;
using golf.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace golf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;
        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewTeam([FromBody] DTONewTeam team)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new BadRequestObjectResult(new { msg = "Invalid model" });

                if (await _teamRepository.CreateNewTeam(team))
                    return Ok(new { msg = "New Team Created" });
                return new BadRequestObjectResult(new { msg = "Could Not Create Team" });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        [HttpGet, Route("AddPlayerToTeam/{teamId}/{playerId}")]
        public async Task<IActionResult> GetScoreBoard(int teamId, int playerId)
        {
            try
            {

                if (await _teamRepository.AddPlayerToTeam(playerId,teamId))
                    return Ok(new { msg = "Added to team" });
                return new BadRequestObjectResult(new { msg = "Could Not Add To Team" });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpGet, Route("RemovePlayerFromTeam/{teamId}/{playerId}")]
        public async Task<IActionResult> RemovePlayerFromTeam(int teamId, int playerId)
        {
            try
            {

                if (await _teamRepository.RemovePlayerFromTeam(playerId, teamId))
                    return Ok(new { msg = "Removed from team" });
                return new BadRequestObjectResult(new { msg = "Could Not Remove From Team" });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpGet, Route("GetTeamsForGame/{gameId}")]
        public  IActionResult GetAllGames(int gameId)
        {
            try
            {
                var gamelist =  _teamRepository.GetTeamsForGame(gameId);
                return Ok(gamelist);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}