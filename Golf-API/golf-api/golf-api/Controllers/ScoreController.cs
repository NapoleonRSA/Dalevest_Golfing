using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using golf.Core.DTO.GameDTO_s;
using golf.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace golf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreCardRepository _scoreRepository;
        public ScoreController(IScoreCardRepository scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }

        [HttpPost, Route("CreateNewPlayerGame")]
        public async Task<IActionResult> CreateNewPlayerGame([FromBody] DTONewPlayerGame newPlayerGame)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new BadRequestObjectResult(new { msg = "Invalid model" });
                if (await _scoreRepository.CreateNewGameByPlayerId(newPlayerGame))
                    return Ok(new { msg = "Player Game Created" });
                return new BadRequestObjectResult(new { msg = "Could Create Game" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
