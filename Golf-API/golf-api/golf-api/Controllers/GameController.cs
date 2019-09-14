using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using golf.Core.DTO.GameDTO_s;
using golf.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace golf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpPost, Route("AddNewGame")]
        public async Task<IActionResult> AddNewGame([FromBody] DTONewGame game)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new BadRequestObjectResult(new { msg = "Invalid model" });

                if (await _gameRepository.CreateNewGame(game))
                    return Ok(new { msg = "New Game Created" });
                return new BadRequestObjectResult(new { msg = "Could Not Create Game" });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet, Route("GetAllGames")]
        public async Task<IActionResult> GetAllGames()
        {
            try
            {
                var gameList = await _gameRepository.GetAllGames();
                return Ok(gameList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
