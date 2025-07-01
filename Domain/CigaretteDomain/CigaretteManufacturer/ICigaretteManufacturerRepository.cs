using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteCategorie
{
    public interface ICigaretteManufacturerRepository
    {
        Task<bool> AddAsync(AddCigaratteManufacturerDTO manufacturerDTO);
        Task<CigaretteManufacturerDTO?> GetByIdAsync(GetByIdDTO id);

        Task<List<CigaretteManufacturerDTO>> GetAllAsync();
        Task<bool> DeleteAsync(DeleteManufacturerDTO id);
        Task<bool> UpdateAsync(UpdateManufacturerDTO obj);
    }
}
