using MusicShareAPI.Contexts;
using MusicShareAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.DataServices
{
    public class LicenseDataService : ILicenseDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public LicenseDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Licenses GetByTitle(string SongTitle)
        {
            var license = _dbContext.Licenses.Where(l => SongTitle.ToLower().Contains(l.Title.ToLower()) &&
            SongTitle.ToLower().Contains(l.Artist.ToLower())).FirstOrDefault();
            return license;
        }
    }
}
