using Domain.CigaretteDomain.CigarettePhoto.Commands.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigarettePhoto.Commands
{
    public class AddPhotoCommandService : ICommandService<AddCigarettePhotoDTO>
    {
        private readonly ICigarettePhotoRepository _repository;
        public AddPhotoCommandService(ICigarettePhotoRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task Execute(AddCigarettePhotoDTO obj)
        {
            if(obj == null)
            {
                ArgumentNullException.ThrowIfNull(obj, nameof(obj));
            }
            await _repository.AddAsync(obj);
        }
    }
}
