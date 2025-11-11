using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserDomain.Commands.Reservations
{
    public class CreateReservationHandler : IRequestHandler<CreateReservationCommand, int>
    {
        private readonly IReservationRepository _repository;

        public CreateReservationHandler(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var dto = request.DTO;

            
            if (dto.PickupDateTime < DateTime.UtcNow.AddMinutes(30))
                throw new InvalidOperationException("Бронь должна быть минимум за 30 минут.");

            
            dto.TotalPrice = dto.Items.Sum(i => i.PriceAtBooking * i.Quantity);

            
            var reservationId = await _repository.AddReservationAsync(dto);

            return reservationId;
        }
    }
}
