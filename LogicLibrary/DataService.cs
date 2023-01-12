using Entities;
using System.Text.Json;

namespace LogicLibrary
{
    public class DataService
    {
        //public void GetData()
        //{
        //    Data.Instance.GetData();
        //}
        //public List<TechView> GetTechViews()
        //{
        //    var list = Data.Instance.GetTechPassports();
        //    List<TechView> result = new List<TechView>();
        //    foreach (TechPassport item in list)
        //    {
        //        result.Add(new TechView(item));
        //    }
        //    return result;
        //}

        public List<TechView> GetTechViews(bool isOld)
        {
            var list = Data.Instance.GetTechPassports();
            List<TechView> result = new List<TechView>();
            foreach (TechPassport item in list)
            {
                if ((item.DecommissioningDate != DateTime.MinValue && item.DecommissioningDate < DateTime.Today) == isOld)
                {
                    result.Add(new TechView(item));
                }
            }
            return result;
        }

        public List<MaterialInfoView> GetMaterialInfoViews()
        {
            var list = Data.Instance.GetMaterialInfos();
            var outerList = Data.Instance.MaterialsInfoFromOuterBase();
            List<MaterialInfoView> result = new List<MaterialInfoView>();
            foreach (var item in list)
            {
                var artInfos = Data.Instance.GetArtInfos().Where(x => x.Material != null && x.Material.Id == item.Id);
                string bdForm = "";
                if (item.InnerArt != null)
                {
                    bdForm = item.InnerArt.Replace(" ", "").ToLower();
                }               
                var outerInfo = outerList.FirstOrDefault(x => x.SKLN_Cd == bdForm);
                result.Add(new MaterialInfoView(item, artInfos, outerInfo));
            }
            return result;
        }

        public List<ArtInfoView> GetArtInfoViews()
        {
            var list = Data.Instance.GetArtInfos();
            List<ArtInfoView> result = new List<ArtInfoView>();
            foreach (var item in list)
            {
                result.Add(new ArtInfoView(item));
            }
            return result;
        }

        public List<ArtInfoView> GetNotOriginalArtInfoViews(int materialId)
        {
            var list = Data.Instance.GetArtInfos().Where(x =>
                x.Material == null ||
                (x.IsOriginal != true &&
                x.Material != null &&
                x.Material.Id == materialId));
            List<ArtInfoView> result = new List<ArtInfoView>();
            foreach (var item in list)
            {
                result.Add(new ArtInfoView(item));
            }
            return result;
        }

        public List<SupplierView> GetSupplierViews()
        {
            var list = Data.Instance.GetSuppliers();
            List<SupplierView> result = new List<SupplierView>();
            foreach (var item in list)
            {
                result.Add(new SupplierView(item));
            }
            return result;
        }

        public List<MaterialView> GetMaterialViews()
        {
            var list = Data.Instance.GetMaterials();
            List<MaterialView> result = new List<MaterialView>();
            foreach (var item in list)
            {
                result.Add(new MaterialView(item));
            }
            return result;
        }

        public List<ErrorCodeView> GetErrorCodeViews()
        {
            var list = Data.Instance.GetErrorCodes();
            List<ErrorCodeView> result = new List<ErrorCodeView>();
            foreach (var item in list)
            {
                result.Add(new ErrorCodeView(item));
            }
            return result;
        }

        public List<PointView> GetPointViews()
        {
            var list = Data.Instance.GetPoints();
            List<PointView> result = new List<PointView>();
            foreach (var item in list)
            {
                result.Add(new PointView(item));
            }
            return result;
        }


        public List<SupplierView> GetSupViews()
        {
            var list = Data.Instance.GetSuppliers();
            List<SupplierView> result = new List<SupplierView>();
            foreach (EquipmentSupplier item in list)
            {
                result.Add(new SupplierView(item));
            }
            return result;
        }

        public List<MaintenanceNewView> GetMaintenanceNewViews()
        {
            var list = Data.Instance.GetMaintenance();
            List<MaintenanceNewView> result = new List<MaintenanceNewView>();
            foreach (MaintenanceInfo item in list)
            {
                result.Add(new MaintenanceNewView(item));
            }
            return result;
        }

        public List<AdditionalWorkView> GetAdditionalWorkViews()
        {           
            var list = Data.Instance.GetAdditionalWorks();
            List<AdditionalWorkView> result = new List<AdditionalWorkView>();
            foreach (var item in list)
            {
                result.Add(new AdditionalWorkView(item));
            }
            return result;
        }

        public List<MaintenanceEpisodeView> GetMaintenanceEpisodeViews()
        {
            var list = Data.Instance.GetMaintananceEpisodes();
            List<MaintenanceEpisodeView> result = new List<MaintenanceEpisodeView>();
            foreach (var item in list)
            {
                result.Add(new MaintenanceEpisodeView(item));
            }
            return result;
        }

        

        public MaintenanceNewView GetMaintenanceNewById(int id)
        {
            var list = Data.Instance.GetMaintenance();
            MaintenanceInfo m = list.FirstOrDefault(x => x.Id == id);
            return new MaintenanceNewView(m);
        }

        public MaintenanceError GetErrorById(int id)
        {
            var list = Data.Instance.GetErrors();
            return list.FirstOrDefault(x => x.Id == id);
        }

        public List<EquipmentTypeView> GetEquipmentTypeViews()
        {
            var types = Data.Instance.GetTypes();
            List<EquipmentTypeView> result = new List<EquipmentTypeView>();
            foreach (EquipmentType item in types)
            {
                result.Add(new EquipmentTypeView(item));
            }
            return result;
        }

        public List<UnitView> GetUnitViews()
        {
            var units = Data.Instance.GetUnitTypes();
            List<UnitView> result = new List<UnitView>();
            foreach (var item in units)
            {
                result.Add(new UnitView(item));
            }
            return result;
        }

        public List<DepartmentView> GetDepartmentViews()
        {
            var units = Data.Instance.GetDepartmentTypes();
            List<DepartmentView> result = new List<DepartmentView>();
            foreach (var item in units)
            {
                result.Add(new DepartmentView(item));
            }
            return result;
        }

        public List<MaintenanceTypeView> GetMaintenanceTypeViews()
        {
            var types = Data.Instance.GetMaintenanceTypes();
            List<MaintenanceTypeView> result = new List<MaintenanceTypeView>();
            foreach (MaintenanceType item in types)
            {
                result.Add(new MaintenanceTypeView(item));
            }
            return result;
        }

        public List<OperatorView> GetOperatorViews()
        {
            var operators = Data.Instance.GetOperators();
            List<OperatorView> views = new List<OperatorView>();
            if (operators != null)
            {
                foreach (Operator item in operators)
                {
                    views.Add(new OperatorView(item));
                }
            }
            return views;
        }

        public List<Material> GetMaterials()
        {
            return Data.Instance.GetMaterials();
        }

        public EquipmentSupplier GetSupplierById(int id)
        {
            return Data.Instance.GetSuppliers().FirstOrDefault(x => x.Id == id);

        }

        public EquipmentType GetTypeById(int id)
        {
            return Data.Instance.GetTypes().FirstOrDefault(x => x.Id == id);

        }

        public ElectroPoint GetPointById(int id)
        {
            return Data.Instance.GetPoints().FirstOrDefault(x => x.Id == id);

        }

        public Operator GetOperatorById(int id)
        {
            return Data.Instance.GetOperators().FirstOrDefault(x => x.Id == id);

        }

        public Department GetDepartmentById(int id)
        {
            return Data.Instance.GetDepartmentTypes().FirstOrDefault(x => x.Id == id);

        }

        //public void EditCharacteristicsByUnit(int id, int unitId)
        //{
        //    Characteristic? characteristic = Data.Instance.GetCharacteristic(id);
        //    Unit? unit = Data.Instance.GetUnitTypes().FirstOrDefault(c => c.Id == unitId);
        //    if (unit != null && characteristic != null)
        //    {
        //        characteristic.Unit = unit;
        //    }            
        //}

        public void EditMaterialInfoByUnit(int id, int unitId)
        {
            Data.Instance.EditMaterialInfoByUnit(id, unitId);
        }

        public void EditMaterialInfoBySupplier(int id, int supId)
        {
            Data.Instance.EditMaterialInfoBySupplier(id, supId);
        }

        public void EditDepartmentByOperator(int id, int unitId)
        {
            Data.Instance.EditDepartmentByOperator(id, unitId);
        }

        public void EditErrorWorking (int id, bool isWorking)
        {
            Data.Instance.EditErrorWorking(id, isWorking);
        }

        

        public void EditMaterialByInfo(int id, int infoId)
        {
            Data.Instance.EditMaterialByInfo(id, infoId);
        }

        public void EditMaterialByArts(int id, List<int> artIds)
        {
            Data.Instance.EditMaterialByArts(id, artIds);
        }

        public int AddMaterial(int infoid, double count, int workId, bool isAdditional)
        {
            return Data.Instance.AddMaterial(infoid, count, workId, isAdditional);
        }

        public CharacteristicView GetCharacteristicViewById(int id)
        {
            return new CharacteristicView(Data.Instance.GetCharacteristic(id));
        }

        public AdditionalWorkView GetAdditionalWorkById(int id)
        {
            return new AdditionalWorkView(Data.Instance.GetAdditionalWorks().FirstOrDefault(x => x.Id == id));
        }

        public ErrorNewView GetErrorNewViewById(int id)
        {
            return new ErrorNewView(GetErrorById(id));
        }

        public int AddTechPassport (TechPassport passport)
        {
            return Data.Instance.AddTechPassport(passport);
        }

        public void EditTechPassport(TechPassport passport)
        {
            Data.Instance.EditTechPassport(passport);
        }

        public int AddTechPassportBaseInfo(TechPassport passport)
        {
            return Data.Instance.AddTechPassportBaseInfo(passport);
        }

        public void EditTechPassportBaseInfo(TechPassport passport)
        {
            Data.Instance.EditTechPassportBaseInfo(passport);
        }

        //public void SaveCharacteristics(int techPassportId, List<CharacteristicView> Characteristics)
        //{
        //    var techPassport = Data.Instance.GetPassportById(techPassportId);
        //    if (techPassport != null)
        //    {
        //        foreach (CharacteristicView cV in Characteristics)
        //        {
        //            if (cV != null)
        //            {
        //                if (techPassport.Characteristics != null)
        //                {
        //                    var ch = techPassport.Characteristics.FirstOrDefault(x => x.Id == cV.Id);
        //                    if (ch != null)
        //                    {
        //                        Data.Instance.EditCharacteristic(techPassportId, cV.Id, cV.Name, cV.GetCount(), cV.Commentary);
        //                        Data.Instance.EditCharacteristicsByUnit(cV.Id, cV.GetUnitId());
        //                    }
        //                    else
        //                    {
        //                        int id = Data.Instance.AddCharacteristic(techPassportId, cV.Name, cV.GetCount(), cV.Commentary);
        //                        Data.Instance.EditCharacteristicsByUnit(id, cV.GetUnitId());
        //                    }
        //                }
        //                else
        //                {
        //                    int id = Data.Instance.AddCharacteristic(techPassportId, cV.Name, cV.GetCount(), cV.Commentary);
        //                    Data.Instance.EditCharacteristicsByUnit(id, cV.GetUnitId());
        //                }
        //            }
        //        }
        //    }
        //}

        public TechView GetPassportTechViewById(int id)
        {
            TechPassport passport = GetPassportById(id);
            return new TechView(passport);
        }

        public List<MaintenanceNewView> GetMaintenanceViewsByInfos(ICollection<MaintenanceInfo> infos)
        {
            List<MaintenanceNewView> views = new List<MaintenanceNewView>();
            foreach (var info in infos)
            {
                info.MaintenanceType = Data.Instance.GetMaintenanceTypeByMaintenanceId(info.Id);
                views.Add(new MaintenanceNewView(info));
            }
            return views;
        }

        public List<ControledParametrView> GetControlViewsByInfos(ICollection<ControledParametr> infos)
        {
            List<ControledParametrView> views = new List<ControledParametrView>();
            foreach (var info in infos)
            {
                views.Add(new ControledParametrView(info));
            }
            return views;
        }

        public List<ControledParametrEpisodeView> GetControlEpisodeViewsByInfos(ICollection<ControledParametrDateInfo> infos)
        {
            List<ControledParametrEpisodeView> views = new List<ControledParametrEpisodeView>();
            foreach (var info in infos)
            {
                views.Add(new ControledParametrEpisodeView(info));
            }
            return views;
        }

        public List<CharacteristicView> GetCharacteristicViewsByInfos(ICollection<Characteristic> infos)
        {
            List<CharacteristicView> views = new List<CharacteristicView>();
            foreach (var info in infos)
            {
                views.Add(new CharacteristicView(info));
            }
            return views;
        }

        public List<InstructionView> GetInstructionViewsByInfos(ICollection<Instruction> infos)
        {
            List<InstructionView> views = new List<InstructionView>();
            foreach (var info in infos)
            {
                views.Add(new InstructionView(info));
            }
            return views;
        }

        public List<ErrorNewView> GetErrorViewsByInfos(ICollection<MaintenanceError> infos)
        {
            List<ErrorNewView> views = new List<ErrorNewView>();
            foreach (var info in infos)
            {
                views.Add(new ErrorNewView(info));
            }
            return views;
        }

        public List<AdditionalWorkView> GetAdditionalWorkViewsByInfos(ICollection<AdditionalWork> infos)
        {
            List<AdditionalWorkView> views = new List<AdditionalWorkView>();
            foreach (var info in infos)
            {
                views.Add(new AdditionalWorkView(info));
            }
            return views;
        }

        public List<HourView> GetHourViewsByInfos(ICollection<HoursInfo> infos)
        {
            List<HourView> views = new List<HourView>();
            foreach (var info in infos)
            {
                views.Add(new HourView(info));
            }
            return views;
        }

        public List<OuterArchiveView> GetAllArchiveViews()
        {
            return GetOuterArchiveViewsByInfos(Data.Instance.GetAdditionalWorks(), Data.Instance.GetErrors(), Data.Instance.GetMaintenance());
        }

        public List<OuterArchiveView> GetOuterArchiveViewsByInfos(ICollection<AdditionalWork> additional,
            ICollection<MaintenanceError> errors, ICollection<MaintenanceInfo> maintenance)
        {
            DateTime date = DateTime.Now;
            List<OuterArchiveView> views = new List<OuterArchiveView>();
            foreach (var ad in additional)
            {
                if (ad.DateFact != null && ad.DateFact <= date && ad.DateFact > DateTime.MinValue)
                {
                    views.Add(new OuterArchiveView(ad));
                }
            }
            foreach (var error in errors)
            {
                if (error.DateOfSolving != null && error.DateOfSolving <= date && error.DateOfSolving > DateTime.MinValue)
                {
                    if (error.Repairings != null)
                    {
                        foreach (var rep in error.Repairings)
                        {
                            views.Add(new OuterArchiveView(rep));
                        }
                    }
                }
            }
            foreach (var info in maintenance)
            {
                if (info.Episodes != null)
                {
                    foreach (var ep in info.Episodes)
                    {
                        if (ep.Date != null && ep.Date <= date && ep.Date > DateTime.MinValue)
                        {
                            views.Add(new OuterArchiveView(ep));
                        }
                    }
                }
            }

            return views;
        }

        

        public List<PlannedView> GetPlannedViewsByInfos(ICollection<AdditionalWork> additional,ICollection<MaintenanceInfo> maintenance)
        {
            DateTime date = DateTime.Now;
            List<PlannedView> views = new List<PlannedView>();
            foreach (var ad in additional)
            {
                if ((ad.DateFact == null || ad.DateFact >= date) && (ad.PlannedDate != null && ad.PlannedDate >=date))
                {
                    views.Add(new PlannedView(ad));
                }
            }
            foreach (var info in maintenance)
            {
                foreach (var ep in info.Episodes)
                {
                    if ((ep.Date == null || ep.Date >= date) && (info.PlannedDate != null && info.PlannedDate >= date))
                    {
                        views.Add(new PlannedView(ep));
                    }
                }
            }

            return views;
        }

        //public List<ControledParametrView> GetControledParametrViewsByInfos(List<ControledParametr> infos)
        //{
        //    List<ControledParametrView> views = new List<ControledParametrView>();
        //    foreach (var info in infos)
        //    {
        //        views.Add(new ControledParametrView(info));
        //    }
        //    return views;
        //}

        public TechPassport? GetPassportById(int id)
        {
            return Data.Instance.GetPassportById(id);
        }

        public List<Characteristic> GetCharacteristicsByPassportId(int id)
        {
            return Data.Instance.GetCharacteristics().Where(i => i.TechPassport != null && i.TechPassport.Id == id).ToList();
        }

            public void AddMaintananceEpisode(int maintenanceId, DateTime date, double hoursOnWork, List<int> workerIds)
        {
            List<Operator> operators = Data.Instance.GetOperators(workerIds);
            Data.Instance.AddMaintananceEpisode(maintenanceId, date, hoursOnWork, operators);
        }

        public void AddUndoneEpisode(int maintenanceId, DateTime date, List<int> workerIds, DateTime oldDate)
        {
            List<Operator> operators = Data.Instance.GetOperators(workerIds);
            Data.Instance.AddUndoneEpisode(maintenanceId, date, operators, oldDate);
        }

        public void SaveEmptyEpisodes(int maintenanceId, DateTime date)
        {
            var m = Data.Instance.GetMaintenance().FirstOrDefault(x => x.Id ==  maintenanceId);
            if (m != null)
            {
                var mView = new MaintenanceNewView(m);
                var dates = mView.GetPlannedDates(DateTime.Today, date);
                foreach (var d in dates)
                {
                    Data.Instance.SaveEmptyEpisode(maintenanceId, d);
                }
            }
        }

        public void MakeMaintananceEpisodeDone(int episodeId, DateTime date, double hoursOnWork, List<int> workerIds)
        {
            List<Operator> operators = Data.Instance.GetOperators(workerIds);
            Data.Instance.MakeMaintananceEpisodeDone(episodeId, date, hoursOnWork, operators);
        }

        public void ChangeEpisodeInfo(int episodeId, DateTime date, List<int> workerIds)
        {
            List<Operator> operators = Data.Instance.GetOperators(workerIds);
            Data.Instance.ChangeEpisodeInfo(episodeId, date, operators);
        }

        public void ErasePlannedDate(int id)
        {
            Data.Instance.ErasePlannedDate(id);
        }

        public void ChangePlannedDate(int id, DateTime date)
        {
            Data.Instance.ChangePlannedDate(id, date);
        }

        public void ChangeAdditionalInfo(int id, DateTime date, List<int> workerIds)
        {
            List<Operator> operators = Data.Instance.GetOperators(workerIds);
            Data.Instance.ChangeAdditionalInfo(id, date, operators);
        }

        public void ChangeFactDate(int id, DateTime date, double hoursOnWork, List<int> workerIds)
        {
            List<Operator> operators = Data.Instance.GetOperators(workerIds);
            Data.Instance.ChangeFactDate(id, date, hoursOnWork, operators);
        }

        public List<MaintenanceEpisodeView> GetEpisodeViewsByMaintenance(int maintenanceId)
        {
            List<MaintenanceEpisodeView> result = new List<MaintenanceEpisodeView>();
            var episodes = Data.Instance.GetEpisodesByInfoId(maintenanceId).ToList();
            foreach (var episode in episodes)
            {
                result.Add(new MaintenanceEpisodeView(episode));
            }
            return result;
        }

        public List<MaterialView> GetMaterialViewsByMaintenance(int maintenanceId, bool isAdditional)
        {
            List<Material> materials = new List<Material>();
            if (Data.Instance.GetMaterials() != null)
            {
                if (isAdditional)
                {
                    materials = Data.Instance.GetMaterials().Where(x => x.AdditionalWork != null && x.AdditionalWork.Id == maintenanceId).ToList();
                }
                else
                {
                    materials = Data.Instance.GetMaterials().Where(x => x.MaintenanceInfo != null && x.MaintenanceInfo.Id == maintenanceId).ToList();
                }
            }
            List<MaterialView> materialViews = new List<MaterialView>();
            foreach (Material material in materials)
            {
                materialViews.Add(new MaterialView(material));
            }

            return materialViews;
        }

        public MaterialView GetMaterialViewById(int id)
        {
            Material material = Data.Instance.GetMaterials().FirstOrDefault(x => x.Id == id);
            return new MaterialView(material);
        }

        public MaterialInfo GetMaterialInfoById(int id)
        {
            return Data.Instance.GetMaterialInfos().FirstOrDefault(x => x.Id == id);
        }

        public void EditInstructionPath(int id, string path)
        {
            Data.Instance.EditInstructionPath(id, path);
        }

        public void EditArtBySupplier(int id, int supId)
        {
            Data.Instance.EditArtBySupplier(id, supId);
        }

        public int AddArtInfo(string artName, int materialId, int supId)
        {
            return Data.Instance.AddArtInfo(materialId, artName, supId);
        }

        //public int AddMaintenance (MaintenanceNewView view)
        //{
        //    return Data.Instance.AddMaintanance(view.MachineId, view.MaintenanceName, view.Type, view.IsFixed);
        //}
    }
}