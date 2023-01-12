using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("to_controled_parametrs_episodes")]
    public class ControledParametrDateInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Count { get; set; }
        public ControledParametr? ControledParametr { get; set; }
    }
}
