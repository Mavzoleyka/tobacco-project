using AuthDomain.Querys.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDomain.Querys
{
    public class AuntificationQueryService : IQueryService<EntryDTO, User?>
    {
        private readonly IAuthRepository _authRepository;

        public AuntificationQueryService(IAuthRepository authRepository)
        {
            ArgumentNullException.ThrowIfNull(authRepository, nameof(authRepository));
            _authRepository = authRepository;
        }

        public User? Execute(EntryDTO obj)
        {
            if (obj == null)
                return null;

            return _authRepository.Auntification(obj.Login, obj.Password);
        }
    }
}
