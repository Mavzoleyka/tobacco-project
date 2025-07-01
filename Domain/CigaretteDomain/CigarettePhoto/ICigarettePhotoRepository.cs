using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigarettePhoto.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigarettePhoto
{
    public interface ICigarettePhotoRepository
    {
        Task<bool> AddAsync(AddCigarettePhotoDTO entity);
        Task<CigarettePhotoDTO?> GetByIdAsync(GetByIdDTO id);
        Task<List<CigarettePhotoDTO>> GetAllAsync();
        Task<bool> DeleteAsync(DeletePhotoDTO id);

        Task<bool> UpdateAsync(UpdatePhotoDTO obj);
        Task<bool> AddMainPhoto(GetByIdPhotoDTO obj);
    }
}
