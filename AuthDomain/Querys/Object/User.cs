using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDomain.Querys.Object
{
    public class User : IQuery
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public IEnumerable<string> Rules { get; set; } = null!;
    }
}
