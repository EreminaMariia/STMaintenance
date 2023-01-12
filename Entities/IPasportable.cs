using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public interface IPasportable
    {
        public int Id { get; set; }
        public TechPassport TechPassport { get; set; }
    }
}
