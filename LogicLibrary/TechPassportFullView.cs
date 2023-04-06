using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LogicLibrary
{
    public class TechPassportFullView
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string InventoryNumber { get; set; }
        public string Supplier { get; set; }
        public string OperatingHours { get; set; }
        public string Type { get; set; }
        public string DepartmentNumber { get; set; }
        

        public TechPassportFullView (TechPassport passport, string department)
        {
            Name = passport.Name;
            SerialNumber = passport.SerialNumber;
            InventoryNumber = passport.InventoryNumber;
            Supplier = passport.Supplier.Name;
            //OperatingHours = passport.OperatingHours.ToString();
            Type = passport.Type.Type;
            DepartmentNumber = passport.Department.Number + ": " + passport.Department.Name;
            
        }
    }
}
