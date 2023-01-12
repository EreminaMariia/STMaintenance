using Entities;

namespace LogicLibrary.Services
{
    public class PointViewService : ITableViewService<PointView>
    {
        public int Add(ITableView view)
        {
            var point = (PointView)view;
            return Data.Instance.AddPoint(point.Name, point.Description);
        }

        public bool Delete(int id)
        {
            return Data.Instance.DeletePoints(id);
        }

        public void Update(ITableView view)
        {
            var point = (PointView)view;
            Data.Instance.EditPoint(point.Id, point.Name, point.Description);
        }
    }
}
