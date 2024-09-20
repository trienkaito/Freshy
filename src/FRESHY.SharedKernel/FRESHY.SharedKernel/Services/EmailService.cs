using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using FRESHY.SharedKernel.Interfaces;
using static System.Net.WebRequestMethods;

namespace FRESHY.SharedKernel.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public EmailService(string smtpHost, int smtpPort, string smtpUsername, string smtpPassword)
        {
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }

        public async Task<int> SendPasswordResetEmailAsync(string email)
        {
            int OTP = 0;
            try
            {
                int OPT = new Random().Next(100000, 1000000);
                var fromAddress = new MailAddress("trien112345@gmail.com", "MinhTrien");
                var toAddress = new MailAddress(email);
                const string subject = "Password Reset";

                string body = @"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>OTP Verification</title>
            <style>
                body {
                    font-family: Arial, sans-serif;
                    line-height: 1.6;
                }
                .container {
                    max-width: 600px;
                    margin: auto;
                    padding: 20px;
                }
                .header {
                    text-align: center;
                    margin-bottom: 20px;
                }
                .footer {
                    margin-top: 20px;
                    font-size: 0.8em;
                }
                .otp {
                    color: red;
                    font-size: 24px;
                    font-weight: bold;
                }
            </style>
            </head>
            <body>
            <div class='container'>
                <div class='header'>
                    <h2>OTP Verification</h2>
                </div>
                <p>Thank you, in order to proceed to the next steps you will need to verify your account.</p>
                <p class='otp'>Your OTP: " + OTP + @"</p>
                <p>If you didn't request this code, you can safely ignore this email. Someone else might have typed your email address by mistake.</p>
                <div class='footer'>
                    <p>Your Company Name Team</p>
                </div>
            </div>
            </body>
            </html>";

                var smtp = new SmtpClient
                {
                    Host = _smtpHost,
                    Port = _smtpPort,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_smtpUsername, _smtpPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    await smtp.SendMailAsync(message);
                }
                return OTP;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ khi gửi email thất bại
                throw;
            }
        }
    }
}
