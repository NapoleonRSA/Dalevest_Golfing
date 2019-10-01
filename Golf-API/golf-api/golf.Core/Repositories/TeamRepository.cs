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

        public async Task<bool> CreateNewTeam(DTONewTeam team)
        {
            try
            {

                var teamAlreadyCreated = _context.Team.Where(x => x.Captain.Id == team.PlayerId && x.Game.Id == team.GameId).SingleOrDefault();

                if(teamAlreadyCreated != null)
                {
                    return true;
                }
                else
                {
                    var newTeam = new Team()
                    {
                        Captain = _context.Player.Find(team.PlayerId),
                        Game = _context.Game.Find(team.GameId),
                        Players = new List<Player>()
                    };
                    await _context.Team.AddAsync(newTeam);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<bool> AddPlayerToTeam(int PlayerId, int TeamId)
        {
            try
            {
                //get list of teams to remove from before joining new team

                
                var team = _context.Team.Include("Game.GameType").Where(x => x.Id == TeamId).SingleOrDefault();
                var player = _context.Player.Find(PlayerId);


                if (team.Players.Count == team.Game.GameType.TeamSize)
                {
                    return false;
                }

                var existingTeam = _context.Team.Include("Players").Include("Game").Where(x => x.Players.Any(y => y.Id == PlayerId) && x.Game.Id == team.Game.Id).FirstOrDefault();
                if (existingTeam != null)
                {
                    existingTeam.Players.Remove(player);
                }


                team.Players.Add(player);

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
                var team = _context.Team.Include("Players").Where(x => x.Id == TeamId).Single();

                team.Players.Remove(_context.Player.Find(PlayerId));
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
                var teams = _context.Team.Include("Game").Include("Players").Include("Captain").Where(x => x.Game.Id == GameId).ToList();

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
                        GameId = team.Game.Id
                    };
                    teamdto.Players = new List<DTOPlayer>();

                    foreach(var player in team.Players)
                    {
                        teamdto.Players.Add(new DTOPlayer()
                        {
                            Handicap = player.HandiCap,
                            PlayerId = player.Id,
                            PlayerName = player.PlayerName,
                            PlayerSurname = player.LastName
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
    }
}
