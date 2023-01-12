using Entities;

namespace LogicLibrary.Services
{
    public class DepartmentViewService : ITableViewService<DepartmentView>
    {
        public int Add(ITableView view)
        {
            var department = (DepartmentView)view;
            return Data.Instance.AddDepartment(department.Name, department.FullName);
        }

        public bool Delete(int id)
        {
            return Data.Instance.DeleteDepartment(id);
        }

        public void Update(ITableView view)
        {
            var department = (DepartmentView)view;
            Data.Instance.EditDepartment(department.Id, department.Name, department.FullName);
        }
    }
}
