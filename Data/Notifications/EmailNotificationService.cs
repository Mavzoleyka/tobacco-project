using Domain.ReservationDomain.Notifications;
using Domain.UserDomain.Commands.Object;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Serilog;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Data.Notifications
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailNotificationService> _logger;

        public EmailNotificationService(IConfiguration config, ILogger<EmailNotificationService> logger)
        {
            _config = config;
            _logger = logger; 
        }

        public async Task SendReservationStatusChangedEmailAsync(string toEmail, int reservationId, ReservationStatus newStatus, CancellationToken ct = default)
        {
            
            _logger.LogInformation("Начало отправки письма о изменении статуса брони для пользователя {ToEmail}, бронь №{ReservationId}, новый статус: {NewStatus}.", toEmail, reservationId, newStatus);

            if (string.IsNullOrWhiteSpace(toEmail))
            {
                _logger.LogWarning("Email адрес пустой. Письмо не будет отправлено.");
                return;
            }

            var smtpUser = _config["Email:Username"];
            var smtpPass = _config["Email:Password"];

            if (string.IsNullOrWhiteSpace(smtpUser) || string.IsNullOrWhiteSpace(smtpPass))
            {
                _logger.LogError("Не указаны данные для SMTP (пользователь или пароль). Письмо не будет отправлено.");
                return;
            }

            try
            {
                using var client = new MailKit.Net.Smtp.SmtpClient();
                _logger.LogInformation("Подключаемся к SMTP серверу...");
                await client.ConnectAsync("smtp.yandex.ru", 587, MailKit.Security.SecureSocketOptions.StartTls);
                _logger.LogInformation("Успешно подключились к SMTP серверу.");

                await client.AuthenticateAsync(smtpUser, smtpPass);
                _logger.LogInformation("Аутентификация на SMTP сервере прошла успешно.");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Your Company", smtpUser));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = $"Статус вашей брони №{reservationId} обновлён";

                var bodyBuilder = new BodyBuilder
                {
                    TextBody = $"Здравствуйте!\n\nСтатус вашей брони №{reservationId} был изменён.\nНовый статус: {newStatus}.\n\nСпасибо, что пользуетесь нашими услугами!"
                };

                message.Body = bodyBuilder.ToMessageBody();

                _logger.LogInformation("Отправка письма для брони №{ReservationId} на адрес {ToEmail}...", reservationId, toEmail);
                await client.SendAsync(message, ct);
                _logger.LogInformation("Письмо для брони №{ReservationId} успешно отправлено на адрес {ToEmail}.", reservationId, toEmail);

                await client.DisconnectAsync(true);
                _logger.LogInformation("Успешно отключились от SMTP сервера.");
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Ошибка при отправке письма для брони №{ReservationId} на адрес {ToEmail}.", reservationId, toEmail);
                throw;  
            }
        }
    }
}
