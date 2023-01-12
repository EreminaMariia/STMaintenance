using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("to_controled_parametrs")]
    public class ControledParametr: IPasportable
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Nominal { get; set; }
        public Unit? Unit { get; set; }
        public TechPassport TechPassport { get; set; }
        public ICollection<ControledParametrDateInfo> Episodes { get; set; }
    }
}
