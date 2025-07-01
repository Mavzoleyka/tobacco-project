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
    public class AddManufacturerCommandService : ICommandService<AddCigaratteManufacturerDTO>
    {
        private readonly ICigaretteManufacturerRepository _repository;
        public AddManufacturerCommandService(ICigaretteManufacturerRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task Execute(AddCigaratteManufacturerDTO obj)
        {
            if(obj == null)
            {
                ArgumentNullException.ThrowIfNull(obj, nameof(obj));
            }

            await _repository.AddAsync(obj);
        }
    }
}
