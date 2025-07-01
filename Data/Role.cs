using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Role
    {
        [Key, Required]
        public int Id { get; set; }

        public string Value { get; set; } = null!;

        public ICollection<Client> Clients { get; set; } = null!;
    }
}
