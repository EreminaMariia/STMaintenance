using Entities;

namespace LogicLibrary.Services
{
    public class ControledParamViewService: ITableViewService<ControledParametrView>
    {
        private PassportMaker techPassport;
        private bool canChange = true;
        public ControledParamViewService(PassportMaker passport)
        {
            techPassport = passport;
        }
        public int Add(ITableView view)
        {
            var item = (ControledParametrView)view;
            int id = 1;

            //return Data.Instance.AddControledParam(passportId, item.Name, item.Nominal);

            if (techPassport.ControledParametrs == null)
            {
                techPassport.ControledParametrs = new List<ControledParametrView>();
            }
            if (techPassport.ControledParametrs.Count > 0)
            {
                id = techPassport.ControledParametrsId++;
            }

            techPassport.ControledParametrs.Add(new ControledParametrView
            {
                Id = id,
                Name = item.Name,
                Nominal = item.Nominal,
                Unit = item.Unit

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
                var item = (ControledParametrView)view;

                var oldItem = techPassport.ControledParametrs.First(x => x.Id == item.Id);
                oldItem.Name = item.Name;
                oldItem.Nominal = item.Nominal;
                oldItem.Unit = item.Unit;
                oldItem.MarkChanged();
                canChange = true;
            }
        }
    }
}
