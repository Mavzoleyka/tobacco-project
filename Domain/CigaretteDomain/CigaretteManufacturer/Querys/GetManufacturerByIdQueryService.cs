using Domain.CigaretteDomain.CigaretteCategorie;
using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteManufacturer.Querys
{
    public class GetManufacturerByIdQueryService : IQueryService<GetByIdDTO, Task<CigaretteManufacturerDTO>>
    {
        private readonly ICigaretteManufacturerRepository _repository;
        public GetManufacturerByIdQueryService(ICigaretteManufacturerRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task<CigaretteManufacturerDTO?> Execute(GetByIdDTO obj)
        {
            if(obj == null)
            {
                ArgumentNullException.ThrowIfNull(obj, nameof(obj));
            }

            return await _repository.GetByIdAsync(obj);
        }
    }
}
