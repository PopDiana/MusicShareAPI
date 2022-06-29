using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.Enums
{
    public enum EmailType
    {
        Unknown = 0,
        Registration = 1,
        PasswordChange = 2,
        PasswordReset = 3,
        SongVerificationFailed = 4,
    }
}
