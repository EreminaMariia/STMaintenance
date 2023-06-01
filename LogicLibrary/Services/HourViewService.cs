using Entities;

namespace LogicLibrary.Services
{
    public class HourViewService : ITableViewService<HourView>
    {
        private PassportMaker techPassport;
        private bool canChange = true;
        public HourViewService(PassportMaker passport)
        {
            techPassport = passport;
        }

        public int Add(ITableView view)
        {
            var item = (HourView)view;
            int id = 1;
            if (techPassport.WorkingHours == null)
            {
                techPassport.WorkingHours = new List<HourView>();
            }
            if (techPassport.WorkingHours.Count > 0)
            {
                id = techPassport.WorkingHoursId++;
            }

            techPassport.WorkingHours.Add(new HourView
            {
                Id = id,
                Hours = item.Hours,
                Date = item.Date
            });

            foreach (var m in techPassport.Maintenances)
            {
                techPassport.RecountDateWithEpisodes(m);
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
                var item = (HourView)view;
                var oldItem = techPassport.WorkingHours.First(x => x.Id == item.Id);
                oldItem.Hours = item.Hours;
                oldItem.Date = item.Date;
                oldItem.MarkChanged();
                canChange = true;

                foreach (var m in techPassport.Maintenances)
                {
                    techPassport.RecountDateWithEpisodes(m);
                }
            }
        }
    }
}
