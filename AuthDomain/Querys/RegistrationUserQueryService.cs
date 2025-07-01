using AuthDomain.Querys.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDomain.Querys
{
    public class RegistrationUserQueryService : IQueryService<RegistrationDTO, User?>
    {
        private readonly IAuthRepository _authRepository;

        public RegistrationUserQueryService(IAuthRepository authRepository)
        {
            ArgumentNullException.ThrowIfNull(authRepository, nameof(authRepository));
            _authRepository = authRepository;
        }

        public User? Execute(RegistrationDTO obj)
        {
            if (obj == null || !obj.Password.Equals(obj.PasswordAgain))
            {
                return null;
            }
            return _authRepository.Registration(obj.Name, obj.Password);
        }
    }
}
