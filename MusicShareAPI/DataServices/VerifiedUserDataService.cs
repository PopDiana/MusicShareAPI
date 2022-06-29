using MusicShareAPI.Contexts;
using MusicShareAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.DataServices
{
    public class VerifiedUserDataService: IVerifiedUserDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public VerifiedUserDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public VerifiedUsers Get(int id)
        {
            return _dbContext.VerifiedUsers.Where(u => u.UserId == id).FirstOrDefault();
        }
    }
}
