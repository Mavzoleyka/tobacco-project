using Data.LogTables;
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
                TotalPrice = dto.TotalPrice,
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDelete = false,
                Items = dto.Items.Select(i => new ReservationItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    PriceAtBooking = i.PriceAtBooking * i.Quantity,
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

        public async Task<List<ReservationDTO>> GetAllReservationsAsync()
        {
            return await _connection.Reservations
                                    .Include(r => r.Client)
                                    .Include(r => r.Items)
                                    .ThenInclude(i => i.Product)
                                    .Select(r => new ReservationDTO
        {
            Id = r.Id,
            PickupDateTime = r.PickupDateTime,
            Comment = r.Comment ?? "-",
            ReservationStatus = r.Status,
            TotalPrice = r.TotalPrice ?? 0,
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
        .OrderByDescending(r => r.PickupDateTime)
        .ToListAsync();
        }

        public async Task<ReservationDTO> GetReservationByIdAsync(int id)
        {
            var reservation = await _connection.Reservations
            .Include(r => r.Client)
            .Include(r => r.Items)
            .ThenInclude(i => i.Product)
        .Include(r => r.StatusLogs) 
        .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null) return null;

            return new ReservationDTO
            {
                Id = reservation.Id,
                PickupDateTime = reservation.PickupDateTime,
                Comment = reservation.Comment,
                ReservationStatus = reservation.Status,
                TotalPrice = reservation.TotalPrice,
                ClientId = reservation.ClientId,
                ClientEmail = reservation.Client.Email,
                IsDelete = reservation.IsDelete,

                Items = reservation.Items.Select(i => new ReservationItemDTO
                {
                    Id = i.Id,
                    ReservationId = i.ReservationId,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    PriceAtBooking = i.PriceAtBooking
                }).ToList(),

                Logs = reservation.StatusLogs
                    .OrderBy(log => log.ChangedAt)
                    .Select(log => new ReservationStatusLogDTO
                    {
                        Id = log.Id,
                        ReservationId = log.ReservationId,
                        OldStatus = log.OldStatus,
                        NewStatus = log.NewStatus,
                        ChangedAt = log.ChangedAt,
                        ChangedBy = log.ChangedBy
                    }).ToList()
            };
        }

        public async Task<List<ReservationDTO>> GetReservationsAsync(
    ReservationStatus? status, DateTime? startDate, DateTime? endDate)
        {
            var query = _connection.Reservations
                .Include(r => r.Client)
                .Include(r => r.Items)
                .ThenInclude(i => i.Product)
                .AsQueryable();

            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value);

            if (startDate.HasValue && endDate.HasValue)
            {
                var inclusiveEnd = endDate.Value.AddDays(1);
                query = query.Where(r => r.PickupDateTime >= startDate.Value &&
                                         r.PickupDateTime < inclusiveEnd);
            }

            return await query
                .Select(r => new ReservationDTO
                {
                    Id = r.Id,
                    PickupDateTime = r.PickupDateTime,
                    Comment = r.Comment,
                    ReservationStatus = r.Status,
                    TotalPrice = r.TotalPrice,
                    ClientId = r.ClientId,
                    Items = r.Items.Select(i => new ReservationItemDTO
                    {
                        ProductName = i.Product.Name,
                        Quantity = i.Quantity,
                        PriceAtBooking = i.PriceAtBooking
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<ReservationDTO>> GetReservationsByClientIdAsync(int clientId)
        {
             return await _connection.Reservations
                .Include(r => r.Client)
                .Include(r => r.Items)
                .ThenInclude(i => i.Product)
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

        public async Task<List<ReservationDTO>> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _connection.Reservations
                                    .Where(r => r.PickupDateTime >= startDate && r.PickupDateTime <= endDate)
                                    .Select(r => new ReservationDTO
                                    {
                                        Id = r.Id,
                                        PickupDateTime = r.PickupDateTime,
                                        Comment = r.Comment,
                                        ReservationStatus = r.Status,
                                        TotalPrice = r.TotalPrice,
                                        ClientId = r.ClientId
                                    })
           .ToListAsync();
        }

        public async Task<List<ReservationDTO>> GetReservationsByStatusAsync(ReservationStatus? status)
        {
            return await _connection.Reservations
                        .Include(r => r.Client)
                        .Include(r => r.Items).ThenInclude(i => i.Product)
                        .Where(r => !status.HasValue || r.Status == status.Value)
                        .OrderByDescending(r => r.PickupDateTime)
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

        public async Task UpdateReservationStatusAsync(int reservationId, ReservationStatus newStatus, string? changedBy)
        {
            var reservation = await _connection.Reservations
        .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null)
                throw new InvalidOperationException($"Бронь с ID {reservationId} не найдена.");

            var oldStatus = reservation.Status;

            
            reservation.Status = newStatus;
            reservation.UpdatedAt = DateTime.UtcNow;

           
            var log = new ReservationStatusLog
            {
                ReservationId = reservation.Id,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                ChangedAt = DateTime.UtcNow,
                ChangedBy = changedBy ?? "System"
            };

            _connection.ReservationStatusLogs.Add(log);

            await _connection.SaveChangesAsync();
        }
    }
}
