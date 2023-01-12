using Entities;

namespace LogicLibrary.Services
{
    public class MaintenanceViewService: ITableViewService<MaintenanceNewView>
    {
        private PassportMaker techPassport;
        private bool canChange = true;
        public MaintenanceViewService(PassportMaker passport)
        {
            techPassport = passport;
        }

        public int Add(ITableView view)
        {
            var item = (MaintenanceNewView)view;
            int id = 1;
            if (item.GetDaysIntervalTime() == 0 && item.GetHoursIntervalTime() != 0)
            {
                item.IsFixed = false;
            }
            if (item.GetDaysIntervalTime() != 0 && item.GetHoursIntervalTime() == 0)
            {
                item.IsFixed = true;
            }
            double interval = item.IsFixed ? item.GetDaysIntervalTime() : item.GetHoursIntervalTime();
            DateTime? futureDate = item.IsDateChanged() ? item.FutureDate : null;

            if (techPassport.Maintenances == null)
            {
                techPassport.Maintenances = new List<MaintenanceNewView>();
            }
            if (techPassport.Maintenances.Count > 0)
            {
                id = techPassport.MaintenancesId++;
            }
            item.Id = id;
            techPassport.Maintenances.Add(item);
            item.MarkChanged();
            return id;
        }

        public bool Delete(int id)
        {
            var maintensnce = techPassport.Maintenances.FirstOrDefault(m => m.Id == id);
            if (maintensnce != null)
            {
                maintensnce.ChangeIfInWork();
                maintensnce.MarkChanged();
            }
            return true;
        }

        public void Update(ITableView view)
        {
            if (canChange)
            {
                canChange = false;
                var item = (MaintenanceNewView)view;
            if (item.GetDaysIntervalTime() == 0 && item.GetHoursIntervalTime() != 0)
            {
                item.IsFixed = false;
            }
            if (item.GetDaysIntervalTime() != 0 && item.GetHoursIntervalTime() == 0)
            {
                item.IsFixed = true;
            }
            double interval = item.IsFixed ? item.GetDaysIntervalTime() : item.GetHoursIntervalTime();
            DateTime? futureDate = item.IsDateChanged() ? item.FutureDate : null;
            var oldItem = techPassport.Maintenances.First(x => x.Id == item.Id);

                oldItem.Name = item.Name;
                if (oldItem.HoursIntervalTime != item.HoursIntervalTime)
                {
                    oldItem.HoursIntervalTime = item.HoursIntervalTime;
                }
                if (oldItem.DaysIntervalTime != item.DaysIntervalTime)
                {
                    oldItem.DaysIntervalTime = item.DaysIntervalTime;
                }
                oldItem.WorkingHours = item.WorkingHours;
                oldItem.FutureDate = item.FutureDate;
                oldItem.Machine = item.Machine;
                oldItem.MachineId = item.MachineId;
                if (oldItem.IsFixed != item.IsFixed)
                {
                    oldItem.IsFixed = item.IsFixed;
                }
                oldItem.MarkChanged();
                canChange = true;
            }
        }
    }
}
