using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary.Services
{
    public class ArtInfoViewService : ITableViewService<ArtInfoView>
    {
        public int Add(ITableView view)
        {
            var art = (ArtInfoView)view;
            return Data.Instance.AddArtInfo(art.GetMaterialId(), art.Art, art.GetSupId());
        }

        public bool Delete(int id)
        {
            return false;
        }

        public void Update(ITableView view)
        {
            var art = (ArtInfoView)view;
            Data.Instance.EditArtInfo(art.Id, art.Art, art.GetSupId());
        }
    }
}
