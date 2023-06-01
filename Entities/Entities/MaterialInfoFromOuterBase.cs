using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class MaterialInfoFromOuterBase
    {
        //артикул
        public string? SKLN_Cd { get; set; }

        //название
        public string? SklN_Nm { get; set; }

        //на складе
        public decimal? EndQt { get; set; }

        //в работе
        public decimal? QtRes { get; set; }
    }
}
