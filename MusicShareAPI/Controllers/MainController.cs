using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MusicShareAPI.DataServices;
using MusicShareAPI.Enums;
using MusicShareAPI.Models;

namespace MusicShareAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {

        private readonly ILogger<MainController> _logger;
        private readonly ISongDataService _songDataService;
        private readonly IVerifiedUserDataService _verifiedUserDataService;
        private readonly ILicenseDataService _licenseDataService;
        private readonly IEmailDataService _emailDataService;
        private readonly IConfiguration _configuration;

        public MainController(ILogger<MainController> logger,
            ISongDataService songDataService,
            IVerifiedUserDataService verifiedUserDataService,
            ILicenseDataService licenseDataService,
            IEmailDataService emailDataService,
            IConfiguration configuration)
        {
            _logger = logger;
            _songDataService = songDataService;
            _verifiedUserDataService = verifiedUserDataService;
            _licenseDataService = licenseDataService;
            _emailDataService = emailDataService;
            _configuration = configuration;
            RecurringJob.AddOrUpdate("verifySongs", () => this.VerifySongsCopyright(), _configuration["Hangfire:Timespan"]);
            RecurringJob.AddOrUpdate("sendEmails", () => this.SendEmails(), _configuration["Hangfire:Timespan"]);
        }

        [HttpPost]
        [Route("verify")]
        public void VerifySong([FromBody] SongObject song)
        {

            _songDataService.CreateSongToVerify(song);
            _logger.LogInformation("Song {id} was submitted for verification.", song.Id);
        }

        public void VerifySongsCopyright()
        {
            var songToVerify = _songDataService.GetSongToVerify();
            if (songToVerify != null)
            {
                var verifiedUser = _verifiedUserDataService.Get(songToVerify.UserId);
                if (verifiedUser == null)
                {
                    var license = _licenseDataService.GetByTitle(songToVerify.Title);
                    if (license != null)
                    {
                        _songDataService.UpdateSongVerificationResult(songToVerify.SongId, songToVerify.UserId, false);
                        _emailDataService.CreateVerificationFailedEmail(songToVerify.Username, songToVerify.Email, songToVerify.Title);
                        _songDataService.DeleteSongToVerify(songToVerify.Id);
                        return;
                    }
                }
                _songDataService.UpdateSongVerificationResult(songToVerify.Id, songToVerify.UserId, true);
                _songDataService.DeleteSongToVerify(songToVerify.Id);
            }

        }

        public void SendEmails()
        {
            var emailToSend = _emailDataService.GetEmailToSend();
            if (emailToSend != null)
            {
                switch (emailToSend.Type)
                {
                    case (int)EmailType.SongVerificationFailed:
                        this.SendSongFailedVerificationEmail(emailToSend);
                        break;
                    default:
                        break;
                }

                _emailDataService.CreateSentEmail(emailToSend);
                _emailDataService.DeleteEmailToSend(emailToSend.Id);
            }
        }

        public void SendEmail(string email, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress(_configuration["Mail:Address"], _configuration["Mail:DisplayName"], Encoding.UTF8);
            mail.Subject = subject;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(_configuration["Mail:Address"], _configuration["Mail:Password"]);
            client.Port = 587;
            client.Host = "smtp.mail.yahoo.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public void SendSongFailedVerificationEmail(EmailsToSend email)
        {
            var subject = "Song failed Copyright verification";
            var body = $@"<html><body><p>Hello <b>{email.Username}</b>,</p><p>Your song with title <b>{email.Details}</b> has been found to violate"
                + @" our Copyright policies and thus we have removed it from our system.</p>" +
                @"<p>Please don't hesitate to contact us if you have any questions or inquiries.</p><br></br><br></br><p>MusicShare Team</p></body></html>";
            this.SendEmail(email.Email, subject, body);
        }
    }
}
