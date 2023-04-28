using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary.Services
{
    public class RepairingViewService : ITableViewService<RepairingView>
    {
        private int errorId;
        private PassportMaker techPassport;
        private bool canChange = true;
        public RepairingViewService(PassportMaker passport, int errorId)
        {
            this.errorId = errorId;
            techPassport = passport;
        }

        public int Add(ITableView view)
        {
            var rep = (RepairingView)view;
            int id = 1;
            if (techPassport.Repairings == null)
            {
                techPassport.Repairings = new List<RepairingView>();
            }
            if (techPassport.Repairings.Count > 0)
            {
                id = techPassport.RepairingsId++;

            }
            techPassport.Repairings.Add(new RepairingView
            {
                Id = id,
                Name = rep.Name,
                Date = rep.Date,
                InfoId = errorId
            });
            ErrorNewView error = techPassport.Errors.FirstOrDefault(x => x.Id == errorId);
            if (error != null)
            {
                error.repairingIds.Add(rep.Id);
            }
            return id;
        }

        public bool Delete(int id)
        {
            return false;
        }

        public void Update(ITableView view)
        {
            if (canChange)
            {
                canChange = false;
                var rep = (RepairingView)view;
                var oldItem = techPassport.Repairings.First(x => x.Id == rep.Id);
                oldItem.Name = rep.Name;
                oldItem.Date = rep.Date;
                oldItem.MarkChanged();
                canChange = true;
            }
        }
    }
}
