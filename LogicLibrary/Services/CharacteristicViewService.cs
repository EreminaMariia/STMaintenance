using Entities;

namespace LogicLibrary.Services
{
    public class CharacteristicViewService: ITableViewService<CharacteristicView>
    {
        private PassportMaker techPassport;
        private bool canChange = true;
        public CharacteristicViewService(PassportMaker passport)
        {
            techPassport = passport;
        }

        public int Add(ITableView view)
        {
            var item = (CharacteristicView)view;
            int id = 1;
            if (techPassport.Characteristics == null)
            {
                techPassport.Characteristics = new List<CharacteristicView>();
            }
            if (techPassport.Characteristics.Count > 0)
            {
                //id = techPassport.Characteristics.Any() ? techPassport.Characteristics.Max(x => x.Id) + 1 : 1;
                id = techPassport.CharacteristicsId++;
            }

            techPassport.Characteristics.Add(new CharacteristicView
            {
                Id = id,
                Name = item.Name,
                Count = item.Count,
                Commentary = item.Commentary,
                
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
                var item = (CharacteristicView)view;
                var oldItem = techPassport.Characteristics.First(x => x.Id == item.Id);
                oldItem.Name = item.Name;
                oldItem.Count = item.Count;
                oldItem.Commentary = item.Commentary;
                oldItem.MarkChanged();
                canChange = true;
            }
        }
    }
}
