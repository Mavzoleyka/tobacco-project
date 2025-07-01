using Domain.CigaretteDomain.CigaretteCategorie.Querys.Object;
using Domain.CigaretteDomain.CigarettePhoto.Commands.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigarettePhoto.Querys
{
    public class GetAllPhotosQueryService : IQueryService<All, Task<List<CigarettePhotoDTO>>>
    {
        private readonly ICigarettePhotoRepository _repository;
        public GetAllPhotosQueryService(ICigarettePhotoRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task<List<CigarettePhotoDTO>> Execute(All obj)
        {
            return await _repository.GetAllAsync();
        }
    }
}
