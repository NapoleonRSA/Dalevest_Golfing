using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using golf.Core.DTO;
using golf.Core.Models;
using golf.Core.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        // GET api/values
//        [HttpGet]
//        public ActionResult<IEnumerable<string>> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

        [HttpGet, Route("GetAllPlayers")]
        public List<Player> Get()
        {
            var player = dbContext.Player.ToList();
            return player;
        }

        // GET api/values/5
        //        [HttpGet("{id}")]
        //        public ActionResult<string> Get(int id)
        //        {
        //            return "value";
        //        }

        [HttpGet, Route("player/{name}")]
        public List<Player> Get(string name)
        {
            var player = dbContext.Player.Where(e => e.PlayerName == name).ToList();
            return player;
        }


        [HttpGet, Route("hole/{id}")]
        public List<Hole> Get(int id)
        {
            var hole = dbContext.Hole.Where(i => i.Player.Id == id).ToList();
            return hole;
        }

        [HttpGet, Route("GetScore")]
        public List<DTOScoreCard> GetScoreBoard()
        {
            var scoreCard = new List<DTOScoreCard>();
            var scoresEntered = dbContext.Hole.Include(p => p.Player).GroupBy(p => p.Player).ToList();
            foreach (var player in scoresEntered)
            {
                var playerScore = new DTOScoreCard
                {
                    Naam = player.Key.PlayerName + ' ' + player.Key.LastName.Substring(0,1),
                    Points = player.Sum(p => p.Score),
                    Strokes = player.Sum(s => s.Strokes),
                    HolesLeft =  18 - player.Where(s => s.Strokes != 0 && s.Player.Id == player.Key.Id).Count()
                };
                scoreCard.Add(playerScore);
            }
            var list = scoreCard.OrderByDescending(p => p.Points).ToList();
            return list;
        }

        [HttpGet, Route("GetPlayerScoreCard")]
        public List<DTOPlayerScoreCard> GetPlayerScoreCard(int id)
        {
            var scoreCard = new List<DTOPlayerScoreCard>();
            var playerScoreCard = dbContext.Hole.Where(p => p.Player.Id == id).ToList();
            foreach (var hole in playerScoreCard)
            {
                var holePlayed = new DTOPlayerScoreCard
                {
                    hole_nr = hole.hole_nr,
                    Score = hole.Score,
                    Strokes = hole.Strokes,
                };
                scoreCard.Add(holePlayed);
            }

            var sortedList = scoreCard.OrderBy(h => h.hole_nr).ToList();
            return sortedList;
        }

        [HttpPost, Route("addPlayerScores")]
        public void Post([FromBody] HoleDTO value)
        {
            var player = dbContext.Player.Where(p => p.Id == value.PlayerId).Single();
            var playerExist = dbContext.Hole.Where(s => s.Player.Id == value.PlayerId).FirstOrDefault();
            var holePlayed = dbContext.Hole
                .Include(p => p.Player).Where(h => h.Player.Id == value.PlayerId && h.hole_nr == value.hole_nr).SingleOrDefault();

            if (playerExist == null)
            {
                for (int i = 1; i <= 18; i++)
                {
                    var hole = new Hole
                    {
                        Score = 0,
                        Strokes = 0,
                        hole_nr = i,
                        Player = player
                    };
                    dbContext.Hole.Add(hole);
                    dbContext.SaveChanges();
                }

            }
        }

        [HttpPost, Route("UpdateStroke")]
        public void UpdatePlayerStroke(DTOPlayerStroke value)
        {
            try
            {
                var playerStroke = dbContext.Hole.Where(p => p.Player.Id == value.playerId && p.hole_nr == value.hole_nr).FirstOrDefault();
                playerStroke.Strokes = value.Strokes;

                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }

            
        }

        [HttpPost, Route("UpdateScore")]
        public void UpdatePlayerScore(DTOPlayerStroke value)
        {
            try
            {
                var playerStroke = dbContext.Hole.Where(p => p.Player.Id == value.playerId && p.hole_nr == value.hole_nr).FirstOrDefault();
                playerStroke.Score = value.Strokes;

                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }


        }

        [HttpPost, Route("addPlayer")]
        public void Post([FromBody] PlayerDTO value)
        {
            var player = new Player
            {
                PlayerName = value.PlayerName,
                Team_id = value.Team_id,
                TotalScore = value.TotalScore
            };
            dbContext.Player.Add(player);
            dbContext.SaveChanges();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Hole value)
        {
            var holes = dbContext.Hole.Where(i => i.hole_nr == value.hole_nr && i.Player.Id == id).FirstOrDefault();
            var player = dbContext.Player.Where(e => e.Id == id).FirstOrDefault();
            holes.Score = value.Score;
            holes.Strokes = value.Strokes;
            holes.hole_nr = value.hole_nr;
            player.TotalScore += value.Score;
            dbContext.Update(holes);
            dbContext.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
