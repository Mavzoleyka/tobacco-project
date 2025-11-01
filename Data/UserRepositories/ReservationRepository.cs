using Domain.UserDomain;
using Domain.UserDomain.Commands.Object;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UserRepositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly Connection _connection;

        public ReservationRepository(Connection connection)
        {
            ArgumentNullException.ThrowIfNull(connection, nameof(connection));
            _connection = connection;
        }

        public async Task<int> AddReservationAsync(CreateReservationDTO dto)
        {
            var reservation = new Reservation
            {
                ClientId = dto.ClientId,
                PickupDateTime = dto.PickupDateTime,
                Comment = dto.Comment,
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDelete = false,
                Items = dto.Items.Select(i => new ReservationItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    PriceAtBooking = i.PriceAtBooking ?? 0m,
                    IsDelete = false
                }).ToList()
            };

            _connection.Reservations.Add(reservation);
            await _connection.SaveChangesAsync();

            return reservation.Id;
        }

        public async Task CancelReservationAsync(int reservationId)
        {
            var reservation = await _connection.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                reservation.Status = ReservationStatus.Canceled;
                reservation.UpdatedAt = DateTime.UtcNow;
                _connection.Reservations.Update(reservation);
                await _connection.SaveChangesAsync();
            }
        }

        public async Task<ReservationDTO> GetReservationByIdAsync(int id)
        {
            var reservation = await _connection.Reservations
           .Where(r => r.Id == id)
           .Select(r => new ReservationDTO
           {
               Id = r.Id,
               PickupDateTime = r.PickupDateTime,
               Comment = r.Comment,
               ReservationStatus = r.Status,
               TotalPrice = r.TotalPrice,//НУЖЕН НОРМАЛЬНЫЙ МАППИНГ ИЛИ ЧТО
               ClientId = r.ClientId,
               IsDelete = r.IsDelete,
               Items = r.Items.Select(i => new ReservationItemDTO
               {
                   Id = i.Id,
                   ReservationId = i.ReservationId,
                   ProductId = i.ProductId,
                   ProductName = i.Product.Name, // Предполагаем, что есть связь с Product
                   Quantity = i.Quantity,
                   PriceAtBooking = i.PriceAtBooking
               }).ToList()
           })
           .FirstOrDefaultAsync();

            return reservation;
        }

        public async Task<List<ReservationDTO>> GetReservationsByClientIdAsync(int clientId)
        {
            return await _connection.Reservations
           .Where(r => r.ClientId == clientId)
           .Select(r => new ReservationDTO
           {
               Id = r.Id,
               PickupDateTime = r.PickupDateTime,
               Comment = r.Comment,
               ReservationStatus = r.Status,
               TotalPrice = r.TotalPrice,
               ClientId = r.ClientId,
               IsDelete = r.IsDelete,
               Items = r.Items.Select(i => new ReservationItemDTO
               {
                   Id = i.Id,
                   ReservationId = i.ReservationId,
                   ProductId = i.ProductId,
                   ProductName = i.Product.Name,
                   Quantity = i.Quantity,
                   PriceAtBooking = i.PriceAtBooking
               }).ToList()
           })
           .ToListAsync();
        }
    }
}
