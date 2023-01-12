using Entities;

namespace LogicLibrary.Services
{
    public class InstructionViewService: ITableViewService<InstructionView>
    {
        private PassportMaker techPassport;
        private bool canChange = true;
        public InstructionViewService(PassportMaker passport)
        {
            techPassport = passport;
        }

        public int Add(ITableView view)
        {
            var item = (InstructionView)view;
            int id = 1;
            if (techPassport.Instructions == null)
            {
                techPassport.Instructions = new List<InstructionView>();
            }
            if (techPassport.Instructions.Count > 0)
            {
                //id = techPassport.Instructions.Any() ? techPassport.Instructions.Max(x => x.Id) + 1 : 1;
                id = techPassport.InstructionsId++;
            }
            techPassport.Instructions.Add(new InstructionView
            {
                Id = id,
                Name = item.Name,

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
                var item = (InstructionView)view;
                var oldItem = techPassport.Instructions.First(x => x.Id == item.Id);
                oldItem.Name = item.Name;
                oldItem.MarkChanged();
                canChange = true;
            }
        }
    }
}
