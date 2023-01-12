using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MaterialInfo
    {
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
        public string Name { get; set; }
        public double Count { get; set; }
    }
}
