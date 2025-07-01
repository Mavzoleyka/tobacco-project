using Domain.CigaretteDomain.CigaretteCategorie;
using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteManufacturer.Commands
{
    public class DeleteManufacturerCommandService : ICommandService<DeleteManufacturerDTO>
    {
        private readonly ICigaretteManufacturerRepository _repository;
        public DeleteManufacturerCommandService(ICigaretteManufacturerRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task Execute(DeleteManufacturerDTO obj)
        {
            if(obj == null)
            {
                ArgumentNullException.ThrowIfNull(obj, nameof(obj));
            }

            await _repository.DeleteAsync(obj);
        }
    }
}
