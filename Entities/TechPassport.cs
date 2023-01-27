using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("to_tech_passports")]
    public class TechPassport
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SerialNumber { get; set; }
        public string? InventoryNumber { get; set; }
        public EquipmentSupplier? Supplier { get; set; }
        public EquipmentType? Type { get; set; }
        public Department? Department { get; set; }
        public DateTime? ReleaseYear { get; set; }
        public DateTime? CommissioningDate { get; set; }
        public DateTime? DecommissioningDate { get; set; }
        public DateTime? GuaranteeEndDate { get; set; }
        public ICollection<MaintenanceInfo>? MaintenanceInfos { get; set; }
        public ICollection<MaintenanceError>? Errors { get; set; }
        public ICollection<Characteristic>? Characteristics { get; set; }
        public ICollection<HoursInfo>? WorkingHours { get; set; }
        public ICollection<ControledParametr>? ControledParametrs { get; set; }
        public ICollection<Instruction>? Instructions { get; set; }
        public ICollection<Instrument>? Instruments { get; set; }
        public ICollection<AdditionalWork>? AdditionalWorks { get; set; }
        public Operator? Operator { get; set; }
        public double? Power { get; set; }
        public ElectroPoint? ElectroPoint { get; set; }
    }
}
