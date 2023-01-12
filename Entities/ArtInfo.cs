using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("to_art_infos")]
    public class ArtInfo
    {
        public int Id { get; set; }
        public string Art { get; set; }
        public bool IsOriginal { get; set; }
        public EquipmentSupplier? Supplier { get; set; }
        public MaterialInfo? Material { get; set; }
    }
}
