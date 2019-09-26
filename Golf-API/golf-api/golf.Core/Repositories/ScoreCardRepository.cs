using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using golf.Core.DTO.GameDTO_s;
using golf.Core.Interfaces;
using golf.Core.Models;
using golf.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace golf.Core.Repositories
{
    public class ScoreCardRepository: IScoreCardRepository
    {
        private readonly golfdbContext _context;

        public ScoreCardRepository(golfdbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateNewGameByPlayerId(DTONewPlayerGame newPlayerGame)
        {
            try
            {
                var courseHole = await _context.Course.Include(h => h.Holes).FirstOrDefaultAsync(c => c.Id == newPlayerGame.CourseId);
                var player = await _context.Player.SingleAsync(p => p.Id == newPlayerGame.PlayerId);
                var game = await _context.Game.SingleAsync(g => g.Id == newPlayerGame.GameId);
                var playerHoleScoreList = new List<PlayerHoleScore>();
                var pscors = _context.Score.Where(x => x.Player.Id == newPlayerGame.PlayerId && x.Game.Id == newPlayerGame.GameId).SingleOrDefault();

                if(pscors == null)
                {
                    foreach (var hole in courseHole.Holes)
                    {
                        var playerScore = new PlayerHoleScore
                        {
                            Player = player,
                            Hole = hole,
                            Points = 0,
                            Score = 0
                        };
                        playerHoleScoreList.Add(playerScore);
                    }
                    var score = new Score
                    {
                        Player = player,
                        Game = game,
                        Holes = playerHoleScoreList
                    };

                    await _context.AddAsync(score);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return true;
                }
                
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
