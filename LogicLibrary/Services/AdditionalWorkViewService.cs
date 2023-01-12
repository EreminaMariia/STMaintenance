using Entities;

namespace LogicLibrary.Services
{
    public class AdditionalWorkViewService: ITableViewService<AdditionalWorkView>
    {
        private PassportMaker techPassport;
        private bool canChange = true;
        public AdditionalWorkViewService(PassportMaker passport)
        {
            techPassport = passport;
        }

        public int Add(ITableView view)
        {
            var item = (AdditionalWorkView)view;
            int id = 1;
            if (techPassport.Additionals == null)
            {
                techPassport.Additionals = new List<AdditionalWorkView>();
            }
            if (techPassport.Additionals.Count > 0)
            {
                //id = techPassport.Additionals.Any() ? techPassport.Additionals.Max(x => x.Id) + 1 : 1;
                id = techPassport.AdditionalsId++;
            }

            techPassport.Additionals.Add(new AdditionalWorkView
            {
                Id = id,
                Name = item.Name,
                Comment = item.Comment,
                FutureDate = item.FutureDate,
                DateFact = item.DateFact,
                WorkingHours = item.WorkingHours
            });
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
                var item = (AdditionalWorkView)view;
                var oldItem = techPassport.Additionals.First(x => x.Id == item.Id);
                oldItem.Name = item.Name;
                oldItem.Comment = item.Comment;
                oldItem.FutureDate = item.FutureDate;
                oldItem.DateFact = item.DateFact;
                oldItem.WorkingHours = item.WorkingHours;
                oldItem.SetWorkingHoursFact(item.GetWorkingHoursFact());
                oldItem.MarkChanged();
                canChange = true;
            }
        }
    }
}
