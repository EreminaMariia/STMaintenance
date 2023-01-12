namespace LogicLibrary
{
    public interface ITableViewService<T>
    {
        bool Delete(int id);
        int Add(ITableView view);
        void Update(ITableView view);
    }
}
