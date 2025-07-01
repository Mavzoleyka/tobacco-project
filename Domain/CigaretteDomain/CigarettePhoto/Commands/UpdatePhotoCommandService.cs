using Domain.CigaretteDomain.CigarettePhoto.Commands.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigarettePhoto.Commands
{
    public class UpdatePhotoCommandService : ICommandService<UpdatePhotoDTO>
    {
        private readonly ICigarettePhotoRepository _repository;
        public UpdatePhotoCommandService(ICigarettePhotoRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task Execute(UpdatePhotoDTO obj)
        {
            if(obj == null)
            {
                ArgumentNullException.ThrowIfNull(obj, nameof(obj));
            }

            await _repository.UpdateAsync(obj);
        }
    }
}
