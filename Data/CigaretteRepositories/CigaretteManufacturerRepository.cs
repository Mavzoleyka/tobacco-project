using Data.CigaretteTables;
using Domain.CigaretteDomain.CigaretteCategorie;
using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.CigaretteRepositories
{
    public class CigaretteManufacturerRepository : ICigaretteManufacturerRepository
    {
        private readonly Connection _connection;
        public CigaretteManufacturerRepository(Connection connection)
        {
            ArgumentNullException.ThrowIfNull(connection, nameof(connection));
            _connection = connection;
        }
        public async Task<bool> AddAsync(AddCigaratteManufacturerDTO obj)
        {
            try
            {
                Manufacturer manufacturer = new Manufacturer();
                manufacturer.Name = obj.Name;
                manufacturer.Country = obj.Country;

                await _connection.CigarettesManufacturer.AddAsync(manufacturer);
                await _connection.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<CigaretteManufacturerDTO?> GetByIdAsync(GetByIdDTO obj) =>
            await _connection.CigarettesManufacturer.FirstOrDefaultAsync(row=>row.Id ==obj.Id).ParseAsync();

        public async Task<List<CigaretteManufacturerDTO>> GetAllAsync()
        {
            var list = await _connection.CigarettesManufacturer.AsNoTracking().ToListAsync();
            List<CigaretteManufacturerDTO> result = new List<CigaretteManufacturerDTO>();
            foreach (var item in list)
            {
                CigaretteManufacturerDTO manufacturerDTO = new CigaretteManufacturerDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Country = item.Country,
                };
                result.Add(manufacturerDTO);
            }
            return result;
        }

        public async Task<bool> DeleteAsync(DeleteManufacturerDTO obj)
        {
            if(obj == null)
            {
                return false;
            }
            var deleteItem = await _connection.CigarettesManufacturer
                                              .FirstOrDefaultAsync(row => row.Id == obj.Id);
            if (deleteItem == null)
            {
                return false;
            }
            deleteItem.IsDelete = true; 
            await _connection.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateManufacturerDTO obj)
        {
            var item = await _connection.CigarettesManufacturer.FirstOrDefaultAsync(row=>row.Id == obj.Id);
            if (item == null)
            {
                return false;
            }
            item.Name = obj.Name;
            item.Country = obj.Country;
            _connection.CigarettesManufacturer.Update(item);
            return await _connection.SaveChangesAsync() > 0;
        }
    }
}
