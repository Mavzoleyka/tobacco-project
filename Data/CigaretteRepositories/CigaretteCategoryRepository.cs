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
    public class CigaretteCategoryRepository : ICigaretteCategoryRepository
    {
        private readonly Connection _connection;
        public CigaretteCategoryRepository(Connection connection)
        {
            ArgumentNullException.ThrowIfNull(connection, nameof(connection));
            _connection = connection;
        }
        public async Task<bool> AddAsync(AddCigaretteCategoryDTO entity)
        {
            try
            {
                ProductCategory obj = new ProductCategory();
                obj.Name = entity.Name;
                await _connection.AddAsync(obj);
                await _connection.SaveChangesAsync();
            }
            catch (Exception )
            {
                return false;
            }
            return true;
        }


        public async Task<CigaretteCategoryDTO?> GetByIdAsync(GetByIdDTO obj)=>
            await _connection.CigarettesCategories.FirstOrDefaultAsync(row=>row.Id==obj.Id).ParseAsync();
        
        public async Task<List<CigaretteCategoryDTO>> GetAllAsync()
        {
            var list = await _connection.CigarettesCategories.ToListAsync();

            List<CigaretteCategoryDTO> result = new List<CigaretteCategoryDTO>();
            foreach (var item in list)
            {
                CigaretteCategoryDTO categoryDTO = new CigaretteCategoryDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                result.Add(categoryDTO);
            }
            return result;

        }

        public async Task<bool> DeleteAsync(DeleteCategoryDTO obj)
        {
            if(obj == null)
            {
                return false;
            }
            var deleteItem = await _connection.CigarettesCategories
                                              .FirstOrDefaultAsync(row => row.Id == obj.Id);
            if(deleteItem == null)
            {
                return false;
            }
            deleteItem.IsDelete = true;
            await _connection.SaveChangesAsync();
            return true;
            
        }

        public async Task<bool> UpdateAsync(UpdateCategoryDTO obj)
        {
            var item = await _connection.CigarettesCategories.FirstOrDefaultAsync(row=>row.Id == obj.Id);

            if (item == null)
            {
                return false;
            }
            item.Name = obj.Name;
            _connection.CigarettesCategories.Update(item);
            return await _connection.SaveChangesAsync() > 0;
            
        }
    }
}
