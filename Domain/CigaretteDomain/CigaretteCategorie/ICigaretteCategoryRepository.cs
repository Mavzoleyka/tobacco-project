using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteCategorie
{
    public interface ICigaretteCategoryRepository
    {
        Task<bool> AddAsync(AddCigaretteCategoryDTO entity);
        Task<CigaretteCategoryDTO?> GetByIdAsync(GetByIdDTO id);
        Task<List<CigaretteCategoryDTO>> GetAllAsync();
        Task<bool> DeleteAsync(DeleteCategoryDTO id);

        Task<bool> UpdateAsync(UpdateCategoryDTO obj);
    }
}
