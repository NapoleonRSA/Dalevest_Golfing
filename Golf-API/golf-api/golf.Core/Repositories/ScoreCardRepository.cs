using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using golf.Core.Interfaces;
using golf.Core.Models;

namespace golf.Core.Repositories
{
    public class ScoreCardRepository: IScoreCardRepository
    {
        private readonly golfdbContext _context;

        public ScoreCardRepository(golfdbContext context)
        {
            _context = context;
        }
        public async Task AddNewCourse()
        {

        }
    }
}
