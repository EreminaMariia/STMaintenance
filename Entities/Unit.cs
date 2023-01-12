﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("to_units")]
    public class Unit
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FullName { get; set; }
        //public ICollection<MaterialInfo> Materials { get; set; }
        //public ICollection<Characteristic> Characteristics { get; set; }
    }
}
