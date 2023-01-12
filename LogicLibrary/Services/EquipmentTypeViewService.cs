using Entities;

namespace LogicLibrary.Services
{
    public class EquipmentTypeViewService : ITableViewService<EquipmentTypeView>
    {
        public int Add(ITableView view)
        {
            var type = (EquipmentTypeView)view;
            return Data.Instance.AddType(type.Type);
        }

        public bool Delete(int id)
        {
            return Data.Instance.DeleteEType(id);
        }

        public void Update(ITableView view)
        {
            var type = (EquipmentTypeView)view;
            Data.Instance.EditType(type.Id,type.Type);
        }
    }
}
