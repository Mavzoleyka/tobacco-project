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
            if (request.DTO.PickupDateTime < DateTime.UtcNow.AddMinutes(30))
                throw new InvalidOperationException("Бронь должна быть минимум за 30 минут.");

            var reservationId = await _repository.AddReservationAsync(request.DTO);
            return reservationId;
        }
    }
}
