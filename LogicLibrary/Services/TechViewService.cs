using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary.Services
{
    public class TechViewService : ITableViewService<TechView>
    {
        public int Add(ITableView view)
        {
            var tech = (TechView)view;
            if (string.IsNullOrEmpty(tech.Name) && string.IsNullOrEmpty(tech.SerialNumber) && string.IsNullOrEmpty(tech.InventoryNumber))
            {
                return 0;
            }
            else
            {
                string number = "";
                if (!string.IsNullOrEmpty(tech.Department))
                {
                    number = tech.Department.Split(": ")[0];
                }
                return Data.Instance.AddTechPassport(tech.Name, tech.SerialNumber, tech.InventoryNumber, number);
            }
        }

        public bool Delete(int id)
        {
            return Data.Instance.DeletePassport(id);
        }

        public void Update(ITableView view)
        {
            var tech = (TechView)view;
            string number = "";
            if (!string.IsNullOrEmpty(tech.Department))
            {
                number = tech.Department.Split(": ")[0];
            }
            Data.Instance.EditTechPassport(tech.Id, tech.Name, tech.SerialNumber, tech.InventoryNumber, number);
        }
    }
}
