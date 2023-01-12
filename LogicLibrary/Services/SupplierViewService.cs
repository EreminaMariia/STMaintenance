using Entities;

namespace LogicLibrary.Services
{
    public class SupplierViewService : ITableViewService<SupplierView>
    {
        public int Add(ITableView view)
        {
            var sup = (SupplierView)view;
            return Data.Instance.AddSupplier(sup.Name, sup.Address, sup.PhoneNumber, sup.AddPhoneNumber, sup.Email, sup.Person, sup.Comment);
        }

        public bool Delete(int id)
        {
            return Data.Instance.DeleteSupplier(id);
        }

        public void Update(ITableView view)
        {
            var sup = (SupplierView)view;
            Data.Instance.EditSupplier(sup.Id, sup.Name, sup.Address, sup.PhoneNumber, sup.AddPhoneNumber, sup.Email, sup.Person, sup.Comment);
        }
    }
}
