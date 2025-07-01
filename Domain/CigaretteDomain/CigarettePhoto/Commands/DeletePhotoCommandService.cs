using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigarettePhoto.Commands
{
    public class DeletePhotoCommandService : ICommandService<DeletePhotoDTO>
    {
        private readonly ICigarettePhotoRepository _repository;
        public DeletePhotoCommandService(ICigarettePhotoRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task Execute(DeletePhotoDTO obj)
        {
            if(obj == null)
            {
                ArgumentNullException.ThrowIfNull(obj, nameof(obj));
            }
            await _repository.DeleteAsync(obj);
        }
    }
}
