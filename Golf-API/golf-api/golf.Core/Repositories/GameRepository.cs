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
                var gameType = await _context.GameType.SingleAsync(g => g.Id == game.GameTypeId);
                var newGame = new Game
                {
                    GameName = game.GameName,
                    Course = course,
                    // Hash Password ?
                    Password = game.GamePassword,
                    GameType = gameType,
                    CreatedOn = DateTime.Now
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
                DateTime daysPassed = DateTime.Now.AddDays(-2);
                var gameList = await _context.Game.Where(b => b.CreatedOn > daysPassed).OrderBy(a=> a.GameName).Include(a => a.Course).Include(x => x.GameType).ToListAsync();
       
                return gameList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Game>> GetAllGames48Hours()
        {
            try
            {
                var gameList = await _context.Game.OrderBy(a => a.GameName).Include(a => a.Course).Include(x => x.GameType).ToListAsync();

                return gameList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<GameType>> GetAllGameTypes() {
            try
            {
                var gameList = await _context.GameType.ToListAsync();

                return gameList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
