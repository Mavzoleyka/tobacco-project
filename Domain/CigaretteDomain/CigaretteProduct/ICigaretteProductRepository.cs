using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct.Querys.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteProduct
{
    public interface ICigaretteProductRepository
    {
        Task<bool> AddAsync(AddCigaretteProductDTO entity);
        Task<CigaretteProductDTO?> GetByIdAsync(GetByIdDTO id);
        Task<List<CigaretteProductDTO>> GetAllAsync();
        Task<bool> DeleteAsync(DeleteCigaretteProductDTO obj);
        Task<bool> UpdateAsync(UpdateCigaretteProductDTO obj);
        Task<List<CigaretteProductDTO>> GetProductsByCategoryAsync(GetByIdDTOforQuery obj);
        Task<NameOfProductCategoryDTO> GetNameOfProductCategoryAsync(GetByIdDTOforQuery obj);
        Task<NameOfProductManufacturerDTO> GetNameOfProductManufacturerAsync(GetByIdDTOforQuery obj);
    }
}
