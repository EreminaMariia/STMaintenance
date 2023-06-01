using Entities;

namespace LogicLibrary.Services
{
    public class ErrorViewService: ITableViewService<ErrorNewView>
    {
        private PassportMaker techPassport;
        private bool canChange = true;
        public ErrorViewService(PassportMaker passport)
        {
            techPassport = passport;
        }

        public int Add(ITableView view)
        {
            var item = (ErrorNewView)view;
            int id = 1;
            bool isWorking = item.IsWorking == "да";
            if (techPassport.Errors == null)
            {
                techPassport.Errors = new List<ErrorNewView>();
            }
            if (techPassport.Errors.Count > 0)
            {
                id = techPassport.ErrorsId++;
            }
            item.Id = id;
            item.MarkChanged();
            techPassport.Errors.Add(item);
            return id;
        }

        public bool Delete(int id)
        {
            var error = techPassport.Errors.FirstOrDefault(m => m.Id == id);
            if (error != null)
            {
                error.ChangeIfInWork();
                error.MarkChanged();
            }
            return true;
        }

        public void Update(ITableView view)
        {
            if (canChange)
            {
                canChange = false;
                var item = (ErrorNewView)view;
                bool isWorking = item.IsWorking == "да";
                var oldItem = techPassport.Errors.First(x => x.Id == item.Id);
                oldItem.Name = item.Name;
                oldItem.Date = item.Date;
                oldItem.Code = item.Code;
                oldItem.Name = item.Name;
                oldItem.Description = item.Description;
                oldItem.Comment = item.Comment;
                oldItem.DateOfSolving = item.DateOfSolving;
                oldItem.MarkChanged();
                canChange = true;
            }
        }
    }
}
