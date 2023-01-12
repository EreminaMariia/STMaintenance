using Entities;

namespace LogicLibrary.Services
{
    public class MaintenanceTypeViewService : ITableViewService<MaintenanceTypeView>
    {
        public int Add(ITableView view)
        {
            var type = (MaintenanceTypeView)view;
            return Data.Instance.AddMaintenanceType(type.Name, type.Description);
        }

        public bool Delete(int id)
        {
            return Data.Instance.DeleteMType(id);
        }

        public void Update(ITableView view)
        {
            var type = (MaintenanceTypeView)view;
            Data.Instance.EditMaintenanceType(type.Id, type.Name, type.Description);
        }
    }
}
