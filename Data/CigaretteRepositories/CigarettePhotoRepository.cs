using Data.CigaretteTables;
using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigarettePhoto;
using Domain.CigaretteDomain.CigarettePhoto.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.CigaretteRepositories
{
    public class CigarettePhotoRepository : ICigarettePhotoRepository
    {
        private readonly Connection _connection;
        public CigarettePhotoRepository(Connection connection)
        {
            ArgumentNullException.ThrowIfNull(connection, nameof(connection));
            _connection = connection;
        }
        public async Task<bool> AddAsync(AddCigarettePhotoDTO entity)
        {
            if (entity == null || entity.ProductId <= 0)
            {
                return false;
            }

            var photo = new ProductPhoto
            {
                ImageURL = entity.ImageURL,
                Caption = entity.Caption,
                IsMain = entity.IsMain,
                ProductId = entity.ProductId
            };

            await _connection.CigarettesPhotos.AddAsync(photo);
            await _connection.SaveChangesAsync(); 
            return true;
        }


        public async Task<CigarettePhotoDTO?> GetByIdAsync(GetByIdDTO obj)
        {
            return await _connection.CigarettesPhotos
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(row => row.Id == obj.Id)
                                    .ParseAsync();
        }

        public async Task<List<CigarettePhotoDTO>> GetAllAsync()
        {
            var list = await _connection.CigarettesPhotos
                                        .AsNoTracking()
                                        .Where(p => !p.IsDelete)
                                        .ToListAsync();

            var cigarettes = list.Select(item => new CigarettePhotoDTO
            {
                Id = item.Id,
                ImageURL = item.ImageURL,
                Caption = item.Caption,
                IsMain = item.IsMain,
                ProductId = item.ProductId,

            }).ToList();

            return cigarettes;
        }

        public async Task<bool> DeleteAsync(DeletePhotoDTO obj)
        {
            if (obj == null) return false;

            var deleteItem = await _connection.CigarettesPhotos.FirstOrDefaultAsync(row => row.Id == obj.Id);
            if (deleteItem == null) return false;

            deleteItem.IsDelete = true;
            await _connection.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(UpdatePhotoDTO obj)
        {
            var item = await _connection.CigarettesPhotos.FirstOrDefaultAsync(row => row.Id == obj.Id);
            if (item == null) return false;

            item.ImageURL = obj.ImageURL;
            item.Caption = obj.Caption;
            item.IsMain = obj.IsMain;


            await _connection.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddMainPhoto(GetByIdPhotoDTO obj)
        {
            var entity = await _connection.CigarettesPhotos.Where(row=>row.Id == obj.Id).FirstOrDefaultAsync();

            var list = await _connection.CigarettesPhotos.Where(row=>row.ProductId== entity.ProductId).ToListAsync();
            foreach (var item in list)
            {
                item.IsMain = false;
            }
            var photo = await _connection.CigarettesPhotos.FirstOrDefaultAsync(row=>row.Id == obj.Id);
            if (photo == null) return false;
            photo.IsMain = true;
            await _connection.SaveChangesAsync();
            return true;
        }
    }
}
