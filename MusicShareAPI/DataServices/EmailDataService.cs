using MusicShareAPI.Contexts;
using MusicShareAPI.Enums;
using MusicShareAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.DataServices
{
    public class EmailDataService: IEmailDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public EmailDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateVerificationFailedEmail(string Username, string Email, string Title)
        {
            var email = new EmailsToSend
            {
                Username = Username,
                Email = Email,
                Type = (int)EmailType.SongVerificationFailed,
                Details = Title
            };
            _dbContext.EmailsToSend.Add(email);
            _dbContext.SaveChanges();
        }

        public void CreateSentEmail(EmailsToSend Email)
        {
            var email = new EmailsSent
            {
                Username = Email.Username,
                Email = Email.Email,
                Type = Email.Type,
                Sent = DateTime.Now,
                Details = Email.Details
            };
            _dbContext.EmailsSent.Add(email);
            _dbContext.SaveChanges();
        }
        
        public EmailsToSend GetEmailToSend()
        {
            return _dbContext.EmailsToSend.Where(e => e.Id != 0).FirstOrDefault();
        }

        public void DeleteEmailToSend(int Id)
        {
            var email = _dbContext.EmailsToSend.Where(e => e.Id == Id).FirstOrDefault();
            if (email != null)
            {
                _dbContext.EmailsToSend.Remove(email);
                _dbContext.SaveChanges();
            }
        }
    }
}
