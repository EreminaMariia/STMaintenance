using Entities;

namespace LogicLibrary.Services
{
    public class MaterialViewService : ITableViewService<MaterialView>
    {
        private int maintenanceId;
        private bool isAdditional;
        private PassportMaker techPassport;
        private bool canChange = true;
        public MaterialViewService(PassportMaker passport, int maintenanceId, bool isAdditional)
        {
            this.maintenanceId = maintenanceId;
            this.isAdditional = isAdditional;
            techPassport = passport;
        }

        public int Add(ITableView view)
        {
            var material = (MaterialView)view;
            double.TryParse(material.Count, out double count);
            int id = 1;
            if (techPassport.Materials == null)
            {
                techPassport.Materials = new List<MaterialView>();
            }
            if (techPassport.Materials.Count > 0)
            {
                //id = techPassport.Materials.Any() ? techPassport.Materials.Max(x => x.Id) + 1 : 1;
                id = techPassport.MaterialsId++;

            }
            techPassport.Materials.Add(new MaterialView
            {
                Id = id,
                InfoId = material.InfoId,
                Count = material.Count
            });
            if (isAdditional)
            {
                AdditionalWorkView addWork = techPassport.Additionals.FirstOrDefault(x => x.Id == maintenanceId);
                if (addWork != null)
                {
                    addWork.materialIds.Add(material.Id);
                }
            }
            else
            {
                MaintenanceNewView maintenance = techPassport.Maintenances.FirstOrDefault(x => x.Id == maintenanceId);
                if (maintenance != null)
                {
                    maintenance.materialIds.Add(material.Id);
                }
            }           
            return id;
        }

        public bool Delete(int id)
        {
            return false;
        }

        public void Update(ITableView view)
        {
            if (canChange)
            {
                canChange = false;
                var material = (MaterialView)view;
                var oldItem = techPassport.Materials.First(x => x.Id == material.Id);
                oldItem.InfoId = material.InfoId;
                oldItem.Count = material.Count;
                oldItem.MarkChanged();
                canChange = true;
            }
        }
    }
}
