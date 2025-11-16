using Domain.ReservationDomain.Commands;
using Domain.ReservationDomain.Helpers;
using Domain.ReservationDomain.Notifications;
using Domain.ReservationDomain.Notifications.Object;
using Domain.UserDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Handlers
{
    public class UpdateReservationStatusHandler : IRequestHandler<UpdateReservationStatusCommand, Unit>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly INotificationRepository _notificationRepository;

        public UpdateReservationStatusHandler(
            IReservationRepository reservationRepository,
            INotificationRepository notificationRepository)
        {
            _reservationRepository = reservationRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<Unit> Handle(UpdateReservationStatusCommand request, CancellationToken cancellationToken)
        {
            
            var reservation = await _reservationRepository.GetReservationByIdAsync(request.ReservationId);
            if (reservation == null)
                throw new KeyNotFoundException($"Бронь с ID {request.ReservationId} не найдена.");

           
            if (!ReservationStatusRules.CanTransition(reservation.ReservationStatus, request.NewStatus))
                throw new InvalidOperationException(
                    $"Недопустимое изменение статуса с {reservation.ReservationStatus} на {request.NewStatus}");

            
            await _reservationRepository.UpdateReservationStatusAsync(
                request.ReservationId,
                request.NewStatus,
                request.ChangedBy);

            
            try
            {
                var message = $"Статус вашей брони №{reservation.Id} изменён на {request.NewStatus}";
                var notification = new NotificationDTO
                {
                    Message = message,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false,
                    ClientId = reservation.ClientId,
                    ReservationId = reservation.Id
                };

                await _notificationRepository.AddAsync(notification);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось сохранить уведомление: {ex.Message}");
            }

            return Unit.Value;
        }
    }
}
