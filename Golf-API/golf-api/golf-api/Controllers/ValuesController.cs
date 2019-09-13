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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            var enteredScore = dbContext.Score.Where(p => p.GameId == id).ToList();
            foreach (var player in enteredScore)
            {
                var playerScore = new DTOPlayerGameScore
                {
                    PlayerName = player.Player.PlayerName + ' ' + player.Player.LastName.Substring(0,1),
                    Score = enteredScore.Where(p => p.PlayerId == player.Player.Id).Sum(s => s.GameScore),
                    Points = enteredScore.Where(p => p.PlayerId == player.Player.Id).Sum(s => s.GamePoints),
                    Thru = 18 - enteredScore.Count(p => p.PlayerId == player.Player.Id && p.GameScore != 0),
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
            var playerScoreCard = dbContext.Score.Include(h => h.Game.)
            foreach (var hole in playerScoreCard)
            {
                var holePlayed = new DTOPlayerScoreCard
                {
                    hole_nr = hole.Game.,
                    Score = hole.GameScore,
                    Points = hole.GamePoints
                };
                scoreCard.Add(holePlayed);
            }

            var sortedList = scoreCard.OrderBy(h => h.hole_nr).ToList();
            return sortedList;
        }

        [HttpPost, Route("StartPlayerGameScoreByGameId")]
        public void StartPlayerGameScoreCard(int id, int playerId)
        {
            var player = dbContext.Player.Where(p => p.Id == playerId).Single();
            var playerExist = dbContext.Score.Where(s => s.Player.Id == playerId && s.GameId == id).FirstOrDefault();
            var course = dbContext.Hole.Where(g => g.CourseId == id).ToList();

            if (playerExist == null)
            {
                foreach (var hole in course)
                {
                    var score = new Score
                    {
                        GameId = id,
                        Player = player,
                        HoleId = hole.Id,
                        GameScore = 0,
                        GamePoints = 0,
                    };
                    dbContext.Add(score);

                }

                dbContext.SaveChanges();
            }
        }

        [HttpPost, Route("UpdateStrokeByGameId")]
        public void UpdatePlayerStroke(int id, DTOPlayerStroke value)
        {
            try
            {
                var playerStroke = dbContext.Score
                    .Where(p => p.Player.Id == value.playerId && p.GameId == id && p.Game.Course.Id == value.holeId).FirstOrDefault();
                playerStroke.GameScore = value.Strokes;

                dbContext.SaveChanges();
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
                var playerStroke = dbContext.Score.Include(g => g.Game)
                    .Where(p => p.Player.Id == value.playerId && p.GameId == id && p.Game.Course.Id == value.holeId ).FirstOrDefault();
                playerStroke.GamePoints = value.Strokes;

                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }


        }
    }
}
