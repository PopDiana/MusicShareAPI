using MusicShareAPI.Contexts;
using MusicShareAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.DataServices
{
    public class SongDataService : ISongDataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MusicShareDbContext _musicShareDbContext;

        public SongDataService(ApplicationDbContext dbContext, MusicShareDbContext musicShareDbContext)
        {
            _dbContext = dbContext;
            _musicShareDbContext = musicShareDbContext;
        }

        public SongsToVerify GetSongToVerify()
        {
            return _dbContext.SongsToVerify.Where(s => s.Id != 0).FirstOrDefault();
        }

        public void CreateSongToVerify(SongObject songObject)
        {
            var song = new SongsToVerify
            {
                Email = songObject.Email,
                SongId = songObject.Id,
                UserId = songObject.UserId,
                Title = songObject.Title,
                Username = songObject.Username,
            };
            _dbContext.SongsToVerify.Add(song);
            _dbContext.SaveChanges();
        }

        public void DeleteSongToVerify(int Id)
        {
            var song = _dbContext.SongsToVerify.Where(s => s.Id == Id).FirstOrDefault();
            if (song != null)
            {
                _dbContext.SongsToVerify.Remove(song);
                _dbContext.SaveChanges();
            }
        }

        public void UpdateSongVerificationResult(int Id, int UserId, bool Result)
        {
            var song = _musicShareDbContext.Songs.Where(s => s.Id == Id).FirstOrDefault();
            if (song != null)
            {
                song.Available = Result;
                _musicShareDbContext.Songs.Update(song);
                _musicShareDbContext.SaveChanges();
                var verificationResult = new VerificationResults
                {
                    SongId = song.Id,
                    UserId = UserId,
                    Approved = Result
                };
                _dbContext.VerificationResults.Add(verificationResult);
                _dbContext.SaveChanges();
            }    
        }
    }
}
