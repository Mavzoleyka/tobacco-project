using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteProduct.Commands
{
    public class AddProductCommandService : ICommandService<AddCigaretteProductDTO>
    {
        private readonly ICigaretteProductRepository _repository;

        public AddProductCommandService(ICigaretteProductRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task Execute(AddCigaretteProductDTO obj)
        {
            if(obj == null)
            {
                ArgumentNullException.ThrowIfNull(obj, nameof(obj));
            }
            await _repository.AddAsync(obj);
        }
    }
}
