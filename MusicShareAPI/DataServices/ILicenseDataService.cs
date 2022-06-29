using MusicShareAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.DataServices
{
    public interface ILicenseDataService
    {
        Licenses GetByTitle(string SongTitle);
    }
}
