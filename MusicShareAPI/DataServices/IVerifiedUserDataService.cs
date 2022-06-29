using MusicShareAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.DataServices
{
    public interface IVerifiedUserDataService
    {
        VerifiedUsers Get(int id);
    }
}
