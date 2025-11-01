using Domain.UserDomain.Commands.Object;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.UserDomain.Commands.Reservations
{
    public class CreateReservationCommand : IRequest<int>
    {
        public CreateReservationDTO DTO { get; }

        public CreateReservationCommand(CreateReservationDTO dto)
        {
            DTO = dto;
        }
    }
}
