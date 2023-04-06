using Entities.Entities;

namespace LogicLibrary.Services
{
    public class MaterialInfoViewService: ITableViewService<MaterialInfoView>
    {
        public int Add(ITableView view)
        {
            var material = (MaterialInfoView)view;
            return Data.Instance.AddMaterialInfo(material.Name, material.InnerArt, material.OriginalArt, new List<int>(), material.Commentary, 0, material.CodeId);
        }

        public bool Delete (int id)
        {
            return Data.Instance.DeleteMaterialInfo(id);
        }

        public void Update (ITableView view)
        {
            var material = (MaterialInfoView)view;
            Data.Instance.EditMaterialInfo(material.Id, material.Name, material.InnerArt, material.OriginalArt, material.Commentary);
        }
    }
}
