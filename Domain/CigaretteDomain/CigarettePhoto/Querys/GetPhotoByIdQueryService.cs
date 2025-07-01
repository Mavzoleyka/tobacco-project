using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigarettePhoto.Commands.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigarettePhoto.Querys
{
    public class GetPhotoByIdQueryService : IQueryService<GetByIdDTO, Task<CigarettePhotoDTO>>
    {
        private readonly ICigarettePhotoRepository _repository;
        public GetPhotoByIdQueryService(ICigarettePhotoRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task<CigarettePhotoDTO?> Execute(GetByIdDTO obj)
        {
           if(obj == null)
           {
                ArgumentNullException.ThrowIfNull(obj, nameof(obj));
           }
            return await _repository.GetByIdAsync(obj);

        }
    }
}
