using Entities;

namespace LogicLibrary.Services
{
    public class OperatorViewService : ITableViewService<OperatorView>
    {
        public int Add(ITableView view)
        {
            var op = (OperatorView)view;
            return Data.Instance.AddOperator(op.Name, op.Position);
        }

        public bool Delete(int id)
        {
            return Data.Instance.DeleteOperator(id);
        }

        public void Update(ITableView view)
        {
            var op = (OperatorView)view;
            Data.Instance.EditOperator(op.Id, op.Name, op.Position);
        }
    }
}
