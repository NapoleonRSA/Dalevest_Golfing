using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using golf.Core.DTO;
using golf.Core.DTO.GameDTO_s;
using golf.Core.Models;
using golf.Core.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace golf.Core.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private golfdbContext dbContext {get;}
        public ValuesController(golfdbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet, Route("GetAllPlayers")]
        public List<Player> Get()
        {
            var player = dbContext.Player.ToList();
            return player;
        }

        [HttpGet, Route("player/{name}")]
        public List<Player> Get(string name)
        {
            var player = dbContext.Player.Where(e => e.PlayerName == name).ToList();
            return player;
        }


        [HttpGet, Route("GetGameScoreCardByGameId")]
        public DTOGameScoreCard GetScoreBoard(int id)
        {
            var scoreCard = new List<DTOPlayerGameScore>();
            var enteredScore = dbContext.Score.Include(p => p.Holes).Where(p => p.Game.Id == id).ToList();
            foreach (var player in enteredScore)
            {
                var playerScore = new DTOPlayerGameScore
                {
                    PlayerName = player.Player.PlayerName + ' ' + player.Player.LastName.Substring(0,1),
                    Score = player.Holes.Sum(s => s.Score),
                    Points = player.Holes.Sum(p => p.Points),
                    Thru = 18 - player.Holes.Count(t => t.Score != 0)
                };
                scoreCard.Add(playerScore);
            }

            var list = scoreCard.OrderByDescending(p => p.Points).ToList();

            DTOGameScoreCard gameScore = new DTOGameScoreCard
            {
                PlayerScores = list
            };
            
            return gameScore;
        }

        [HttpGet, Route("GetPlayerScoreCardByGameId")]
        public List<DTOPlayerScoreCard> GetPlayerScoreCard(int id, int gameId)
        {
            var scoreCard = new List<DTOPlayerScoreCard>();
            var playerScoreCard = dbContext.Score.Include(x => x.Holes).Include("Holes.Hole").FirstOrDefault(p => p.Game.Id == gameId && p.Player.Id == id);
            foreach (var hole in playerScoreCard.Holes)
            {
                var holePlayed = new DTOPlayerScoreCard
                {
                    hole_nr = hole.Hole.hole_nr,
                    Score = hole.Score,
                    Points = hole.Points
                };
                scoreCard.Add(holePlayed);
            }

            var sortedList = scoreCard.OrderBy(h => h.hole_nr).ToList();
            return sortedList;
        }

        [HttpPost, Route("UpdateStrokeByGameId")]
        public void UpdatePlayerStroke(int id, DTOPlayerStroke value)
        {
            try
            {
                var playerStroke = dbContext.PlayerHoleScore
                    .Single(p => p.Player.Id == value.playerId && p.Hole.Id == value.holeId);
                if (playerStroke == null)
                {
                    playerStroke.Score = value.Strokes;
                    dbContext.Add(playerStroke);
                    dbContext.SaveChanges();
                }
                else
                {
                    playerStroke.Score = value.Strokes;
                    dbContext.Update(playerStroke);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost, Route("UpdateScore")]
        public void UpdatePlayerScore(int id, DTOPlayerStroke value)
        {
            try
            {
                var playerStroke = dbContext.PlayerHoleScore
                    .Single(p => p.Player.Id == value.playerId && p.Hole.Id == value.holeId);
                if (playerStroke == null)
                {
                    playerStroke.Points = value.Strokes;
                    dbContext.Add(playerStroke);
                    dbContext.SaveChanges();
                }
                else
                {
                    playerStroke.Points = value.Strokes;
                    dbContext.Update(playerStroke);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
