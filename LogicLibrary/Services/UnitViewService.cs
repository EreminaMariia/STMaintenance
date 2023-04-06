using Entities.Entities;

namespace LogicLibrary.Services
{
    public class UnitViewService : ITableViewService<UnitView>
    {
        public int Add(ITableView view)
        {
            var unit = (UnitView)view;
            return Data.Instance.AddUnit(unit.Name, unit.FullName);
        }

        public bool Delete(int id)
        {
            return Data.Instance.DeleteUnit(id);
        }

        public void Update(ITableView view)
        {
            var unit = (UnitView)view;
            Data.Instance.EditUnit(unit.Id, unit.Name, unit.FullName);
        }
    }
}
