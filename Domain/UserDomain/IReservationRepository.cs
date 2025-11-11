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
        public Task<List<ReservationDTO>> GetAllReservationsAsync();
        public Task UpdateReservationStatusAsync(int  reservationId, ReservationStatus newStatus, string? changedBy);
        public Task<List<ReservationDTO>> GetReservationsByStatusAsync(ReservationStatus? status);
        public Task<List<ReservationDTO>> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate);
        public Task<List<ReservationDTO>> GetReservationsAsync(
        ReservationStatus? status, DateTime? startDate, DateTime? endDate);
    }
}
