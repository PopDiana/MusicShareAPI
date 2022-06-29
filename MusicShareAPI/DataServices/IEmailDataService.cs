using MusicShareAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.DataServices
{
    public interface IEmailDataService
    {
        void CreateVerificationFailedEmail(string Username, string Email, string Title);
        void CreateSentEmail(EmailsToSend Email);
        EmailsToSend GetEmailToSend();
        void DeleteEmailToSend(int Id);
    }
}
