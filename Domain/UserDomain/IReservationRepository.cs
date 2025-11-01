using Domain.UserDomain.Commands.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserDomain
{
    public interface IReservationRepository
    {
        public Task<ReservationDTO> GetReservationByIdAsync(int id);
        public Task<List<ReservationDTO>> GetReservationsByClientIdAsync(int clientId);
        public Task<int> AddReservationAsync(CreateReservationDTO dto);
        public Task CancelReservationAsync(int reservationId);

    }
}
