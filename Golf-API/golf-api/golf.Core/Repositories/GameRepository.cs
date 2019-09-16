using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using golf.Core.DTO.CourseDTO_s;
using golf.Core.DTO.GameDTO_s;
using golf.Core.Interfaces;
using golf.Core.Models;
using golf.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace golf.Core.Repositories
{
    public class GameRepository: IGameRepository
    {
        private readonly golfdbContext _context;

        public GameRepository(golfdbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateNewGame(DTONewGame game)
        {
            try
            {
                var course = await _context.Course.SingleAsync(c => c.Id == game.CourseId);
                var newGame = new Game
                {
                    GameName = game.GameName,
                    Course = course,
                    // Hash Password ?
                    Password = game.GamePassword
                };
                await _context.Game.AddAsync(newGame);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<List<Game>> GetAllGames()
        {
            try
            {
                var gameList = await _context.Game.ToListAsync();
                foreach (var game in gameList)
                {
                    game.Password = null;
                }

                return gameList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
