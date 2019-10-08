using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using golf.Core.DTO;
using golf.Core.DTO.CourseDTO_s;
using golf.Core.DTO.GameDTO_s;
using golf.Core.Interfaces;
using golf.Core.Models;
using golf.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace golf.Core.Repositories
{
    public class TeamRepository: ITeamRepository
    {
        private readonly golfdbContext _context;

        public TeamRepository(golfdbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateNewTeam(DTONewTeam team)
        {
            try
            {

                var teamAlreadyCreated = _context.Team.Where(x => x.Captain.Id == team.PlayerId && x.Game.Id == team.GameId).FirstOrDefault();

                if(teamAlreadyCreated != null)
                {
                    return teamAlreadyCreated.Id;
                }
                else
                {
                    var newTeam = new Team()
                    {
                        Captain = _context.Player.Find(team.PlayerId),
                        Game = _context.Game.Find(team.GameId),
                        TeamPlayers = new List<TeamPlayer>(),
                        TeamName = team.TeamName
                    };
                    await _context.Team.AddAsync(newTeam);
                    await _context.SaveChangesAsync();
                    return newTeam.Id;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public async Task<bool> AddPlayerToTeam(int PlayerId, int TeamId)
        {
            try
            {
                //get list of teams to remove from before joining new team

                
                var team = _context.Team.Include("Game.GameType").Include("TeamPlayers").Where(x => x.Id == TeamId).SingleOrDefault();
                var tp = _context.TeamPlayer.Where(x => x.PlayerId == PlayerId && x.TeamId == TeamId).SingleOrDefault();


                if (team.TeamPlayers.Count == team.Game.GameType.TeamSize)
                {
                    return false;
                }

                var existingTeam = _context.Team.Include("TeamPlayers").Where(x => x.TeamPlayers.Any(y => y.PlayerId == PlayerId && y.TeamId == TeamId)).SingleOrDefault();

                if (existingTeam != null)
                {
                    existingTeam.TeamPlayers.Remove(tp);
                }
                if(tp == null)
                {
                    tp = new TeamPlayer()
                    {
                        TeamId = TeamId,
                        PlayerId = PlayerId
                    };
                }


                team.TeamPlayers.Add(tp);

                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> RemovePlayerFromTeam(int PlayerId, int TeamId)
        {
            try
            {
                

               var tp =  _context.TeamPlayer.Where(x => x.PlayerId == PlayerId && x.TeamId == TeamId).SingleOrDefault();
                if(tp != null)
                {
                    _context.TeamPlayer.Remove(tp);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public  List<DTOTeam> GetTeamsForGame(int GameId)
        {
            try
            {
                var teams = _context.Team.Include("Game").Include("TeamPlayers").Include("TeamPlayers.Player").Include("Captain").Where(x => x.Game.Id == GameId).ToList();

                List<DTOTeam> teamlist = new List<DTOTeam>();

                foreach(var team in teams)
                {
                    var teamdto = new DTOTeam()
                    {
                        Captain = new DTOPlayer()
                        {
                            Handicap = team.Captain.HandiCap,
                            PlayerId = team.Captain.Id,
                            PlayerName = team.Captain.PlayerName,
                            PlayerSurname = team.Captain.LastName
                        },
                        Description = team.TeamName,
                        GameId = team.Game.Id,
                        TeamId = team.Id
                    };
                    teamdto.Players = new List<DTOPlayer>();

                    foreach(var player in team.TeamPlayers)
                    {
                        teamdto.Players.Add(new DTOPlayer()
                        {
                            Handicap = player.Player.HandiCap,
                            PlayerId = player.Player.Id,
                            PlayerName = player.Player.PlayerName,
                            PlayerSurname = player.Player.LastName
                        });
                    }
                    teamlist.Add(teamdto);
                }
                return teamlist;
               
            }
            catch (Exception ex)
            {
                return new List<DTOTeam>();
            }
        }

        public DTOTeam GetTeam(int teamId)
        {
            var team = _context.Team.Where(a => a.Id == teamId).Include("TeamPlayers.Player").Include("Game").Include("Captain").FirstOrDefault();

            DTOTeam newTeam = new DTOTeam()
            {
                Captain = new DTOPlayer()
                {
                    Handicap = team.Captain.HandiCap,
                    PlayerId = team.Captain.Id,
                    PlayerName = team.Captain.PlayerName,
                    PlayerSurname = team.Captain.LastName
                },
                Description = team.TeamName,
                GameId = team.Game.Id,
                Players = new List<DTOPlayer>(),
                TeamId = team.Id
            };
            foreach(var player in team.TeamPlayers)
            {
                newTeam.Players.Add(new DTOPlayer()
                {
                    Handicap = player.Player.HandiCap,
                    PlayerId = player.PlayerId,
                    PlayerName = player.Player.PlayerName,
                    PlayerSurname = player.Player.LastName
                });
            }
            return newTeam;

        }
    }
}
