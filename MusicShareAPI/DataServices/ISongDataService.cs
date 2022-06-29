using MusicShareAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.DataServices
{
    public interface ISongDataService
    {
        SongsToVerify GetSongToVerify();
        void CreateSongToVerify(SongObject songObject);
        void DeleteSongToVerify(int Id);
        void UpdateSongVerificationResult(int Id, int UserId, bool Result);
    }
}
