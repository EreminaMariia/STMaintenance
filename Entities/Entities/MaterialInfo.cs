using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("to_material_infos")]
    public class MaterialInfo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? InnerArt { get; set; }
        public ICollection<ArtInfo> ArtInfos { get; set; }
        public string? Commentary { get; set; }
        public ICollection<Material> MaterialInUse { get; set; }
        public Unit? Unit { get; set; }
    }
}
