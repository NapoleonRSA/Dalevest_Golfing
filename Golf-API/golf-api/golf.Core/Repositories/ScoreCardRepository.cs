using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using golf.Core.DTO;
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

        public DTOLeaderBoard GetLeader(int gameId)
        {
            var details = GetLeaderBoard(gameId);

          var leader =   details.FirstOrDefault();

            if(leader == null)
            {
                return new DTOLeaderBoard();
            }
            else
            {
                return leader;
            }
        }
        public List<DTOLeaderBoard> GetLeaderBoard(int gameId)
        {
            var game = _context.Game.Include("GameType").Where(x => x.Id == gameId).Single();

            var teams = _context.Team.Include(x => x.TeamPlayers).Where(x => x.Game.Id == gameId).ToList();

            var score = _context.Score.Include("Holes.Hole").Include("Holes.Player").Where(x => x.Game.Id == gameId).ToList();
           
            List<DTOLeaderBoard> leaders = new List<DTOLeaderBoard>();
            if(game.GameType.Id == 2 || game.GameType.Id == 1)
            {
                int teamScore = 0;
                foreach (var team in teams)
                {
                    var teamScoreCount = 1;
                    if(game.GameType.Id == 2)
                    {
                        teamScoreCount = 1;
                    }
                    if(game.GameType.Id == 1)
                    {
                        teamScoreCount = 2;
                    }
                    var playersInTeam = team.TeamPlayers.Select(x => x.PlayerId);
                    var scores = _context.PlayerHoleScore.Where(x => playersInTeam.Contains(x.Player.Id)).ToList();
                    if (playersInTeam.Count() > 0)
                    {
                        //hierdie een van elke span vir sy eie mo
                       
                            foreach (var hole in score.First().Holes.Select(x => x.Hole.hole_nr).Distinct())
                            {
                                var teamScores = scores.Where(x => x.Hole.hole_nr == hole).ToList();
                                teamScore += Convert.ToInt32(teamScores.OrderByDescending(y => y.Points).Select(x => x.Points).Take(teamScoreCount).Sum());
                            }
                            DTOLeaderBoard leaderboardItem = new DTOLeaderBoard()
                            {
                                HolesNotScored = scores.Where(x => x.ScoreUpdated == null).Select(y => y.Hole).Distinct().Count(),
                                Points = teamScore,
                                Position = 0,
                                Strokes = 0,
                                TeamName = team.TeamName
                            };
                            leaders.Add(leaderboardItem);
                        
                    }
                    
                }
            
               
                
            }
            else
            {
                
                foreach (var player in score.Select(y => y.Player).Distinct())
                {
                    var sc = score.Where(y => y.Player.Id == player.Id).Single();
                    int teamScore = 0;
                    int strokes = 0;
                    foreach (var hole in sc.Holes)
                    {
                        teamScore += hole.Points;
                        strokes += hole.Score;
                    }
                    DTOLeaderBoard leaderboardItem = new DTOLeaderBoard()
                    {
                        HolesNotScored = sc.Holes.Where(y => y.ScoreUpdated == null).Select(x => x.Hole.hole_nr).Count(),
                        Points = teamScore,
                        Position = 0,
                        Strokes = strokes,
                        TeamName = player.PlayerName + " " + player.LastName
                    };
                    leaders.Add(leaderboardItem);
                }
            }
            leaders = leaders.OrderByDescending(y => y.Points).ToList();
            int pos = 1;
            foreach (var item in leaders)
            {
                item.Position = pos;
                pos++;
            }
            return leaders;
        }

        public async Task<bool> CreateNewGameByPlayerId(DTONewPlayerGame newPlayerGame)
        {
            try
            {
                var game = await _context.Game.Include("Course.Holes").SingleAsync(g => g.Id == newPlayerGame.GameId);
            //    var courseHole = await _context.Course.Include(h => h.Holes).FirstOrDefaultAsync(c => c.Id == game.Course);
                var player = await _context.Player.SingleAsync(p => p.Id == newPlayerGame.PlayerId);
              
                var playerHoleScoreList = new List<PlayerHoleScore>();
                var pscors = _context.Score.Where(x => x.Player.Id == newPlayerGame.PlayerId && x.Game.Id == newPlayerGame.GameId).SingleOrDefault();

                if(pscors == null)
                {
                    foreach (var hole in game.Course.Holes)
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
