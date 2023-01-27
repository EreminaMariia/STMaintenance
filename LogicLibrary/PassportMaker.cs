using Entities;

namespace LogicLibrary
{
    public class PassportMaker
    {
        DataService dataService;
        bool isPassportInfoChanged;
        public TechPassport TechPassport { get; }
        public List<CharacteristicView> Characteristics { get; set; }
        public List<MaintenanceNewView> Maintenances { get; set; }
        public List<AdditionalWorkView> Additionals { get; set; }
        public List<MaterialView> Materials { get; set; }
        public List<InstructionView> Instructions { get; set; }
        public List<InstrumentView> Instruments { get; set; }
        public List<HourView> WorkingHours { get; set; }
        public List<ErrorNewView> Errors { get; set; }
        public List<MaintenanceEpisodeView> Episodes { get; set; }
        public List<ControledParametrView> ControledParametrs { get; set; }
        public List<ControledParametrEpisodeView> ControledParametrEpisodes { get; set; }

        public int CharacteristicsId { get; set; }
        public int MaintenancesId { get; set; }
        public int AdditionalsId { get; set; }
        public int MaterialsId { get; set; }
        public int InstructionsId { get; set; }
        public int InstrumentsId { get; set; }
        public int WorkingHoursId { get; set; }
        public int ErrorsId { get; set; }
        public int EpisodesId { get; set; }
        public int ControledParametrsId { get; set; }
        public int ControledParametrEpisodesId { get; set; }
        public PassportMaker(DataService dataService)
        {
            this.dataService = dataService;
            TechPassport = new TechPassport();
            Characteristics = new List<CharacteristicView>();
            Maintenances = new List<MaintenanceNewView>();
            Materials = dataService.GetMaterialViews();
            Instructions = new List<InstructionView>();
            Instruments = new List<InstrumentView>();
            WorkingHours = new List<HourView>();
            Errors = new List<ErrorNewView>();
            Additionals = new List<AdditionalWorkView>();
            Episodes = new List<MaintenanceEpisodeView>();
            ControledParametrs = new List<ControledParametrView>();
            ControledParametrEpisodes = new List<ControledParametrEpisodeView>();
            isPassportInfoChanged = true;

            GetIds();
        }

        public PassportMaker(DataService dataService, int passportId)
        {
            GetIds();

            this.dataService = dataService;
            TechPassport = dataService.GetPassportById(passportId);
            
            Characteristics = new List<CharacteristicView>();
            var characteristics = TechPassport.Characteristics;
            if (characteristics != null)
            {
                Characteristics = dataService.GetCharacteristicViewsByInfos(characteristics);
            }
            var maintenances = TechPassport.MaintenanceInfos;
            Maintenances = new List<MaintenanceNewView>();
            Materials = new List<MaterialView>();
            Episodes = new List<MaintenanceEpisodeView>();
            if (maintenances != null)
            {
                var m = dataService.GetMaintenanceViewsByInfos(maintenances);
                Maintenances = m;
                foreach (var maintenance in maintenances)
                {
                    Materials.AddRange(dataService.GetMaterialViewsByMaintenance(maintenance.Id, false).ToList());
                    Episodes.AddRange(dataService.GetEpisodeViewsByMaintenance(maintenance.Id).ToList());
                }
            }
            Additionals = new List<AdditionalWorkView>();
            var additionals = TechPassport.AdditionalWorks;
            if (additionals != null)
            {
                Additionals = dataService.GetAdditionalWorkViewsByInfos(additionals);
                foreach (var additional in additionals)
                {
                    Materials.AddRange(dataService.GetMaterialViewsByMaintenance(additional.Id, true).ToList());
                }
            }

            Instructions = new List<InstructionView>();
            var instructions = TechPassport.Instructions;
            if (instructions != null)
            {
                Instructions = dataService.GetInstructionViewsByInfos(instructions);
            }

            Instruments = new List<InstrumentView>();
            var instruments = TechPassport.Instruments;
            if (instruments != null)
            {
                Instruments = dataService.GetInstrumentViewsByInfos(instruments);
            }

            WorkingHours = new List<HourView>();
            var hours = TechPassport.WorkingHours;
            if (hours != null)
            {
                WorkingHours = dataService.GetHourViewsByInfos(hours);
            }

            Errors = new List<ErrorNewView>();
            var errors = TechPassport.Errors;
            if (errors != null)
            {
                Errors = dataService.GetErrorViewsByInfos(errors);
            }

            ControledParametrs = new List<ControledParametrView>();
            var controledparams = TechPassport.ControledParametrs;
            if (controledparams != null)
            {
                ControledParametrs = dataService.GetControlViewsByInfos(controledparams);
            }

            ControledParametrEpisodes = new List<ControledParametrEpisodeView>();
            List<ControledParametrDateInfo> controledparamEpisodes = new List<ControledParametrDateInfo>();
            foreach (var controlP in ControledParametrs)
            {
                controledparamEpisodes.AddRange(Data.Instance.GetEpisodesByControlParametr(controlP.Id));
            }
            if (controledparamEpisodes != null)
            {
                ControledParametrEpisodes = dataService.GetControlEpisodeViewsByInfos(controledparamEpisodes);
            }

            isPassportInfoChanged = false;
        }

        private void GetIds()
        {
            CharacteristicsId = Data.Instance.GetCharacteristicsId();
            MaintenancesId = Data.Instance.GetMaintenancesId();
            AdditionalsId = Data.Instance.GetAdditionalsId();
            MaterialsId = Data.Instance.GetMaterialsId();
            InstructionsId = Data.Instance.GetInstructionsId();
            InstrumentsId = Data.Instance.GetInstrumentsId();
            WorkingHoursId = Data.Instance.GetWorkingHoursId();
            ErrorsId = Data.Instance.GetErrorsId();
            EpisodesId = Data.Instance.GetEpisodesId();
            ControledParametrsId = Data.Instance.GetControledParametrsId();
            ControledParametrEpisodesId = Data.Instance.GetControledParametrEpisodesId();
        }

        public TechPassport GetPassport()
        {
            return TechPassport;
        }
        
        public List<InnerArchiveView> GetArchiveView()
        {
            var additional = Additionals != null ? Additionals : new List<AdditionalWorkView>();
            var errors = Errors != null ? Errors: new List<ErrorNewView>();
            var maintenance = Maintenances != null? Maintenances: new List<MaintenanceNewView>();

            return GetArchiveViewsByInfos(additional, errors, maintenance);
        }

        public List<InnerArchiveView> GetArchiveViewsByInfos(ICollection<AdditionalWorkView> additional,
            ICollection<ErrorNewView> errors, ICollection<MaintenanceNewView> maintenance)
        {
            DateTime date = DateTime.Now;
            List<InnerArchiveView> views = new List<InnerArchiveView>();
            foreach (var ad in additional)
            {
                if (ad.DateFact != null && ad.DateFact <= date && ad.DateFact > DateTime.MinValue)
                {
                    views.Add(new InnerArchiveView(ad));
                }
            }
            //пока не ясно как сделать
            foreach (var error in errors)
            {
                if (error.DateOfSolving != null && error.DateOfSolving <= date && error.DateOfSolving > DateTime.MinValue)
                {
                    views.Add(new InnerArchiveView(error));
                    //if (error.Repairings != null)
                    //{
                    //    foreach (var rep in error.Repairings)
                    //    {
                    //        views.Add(new InnerArchiveView(rep));
                    //    }
                    //}
                }
            }
            foreach (var info in maintenance)
            {
                var eps = Episodes.Where(x => x.MaintenanceId == info.Id && x.IsDone);
                if (eps != null)
                {
                    foreach (var ep in eps)
                    {
                        if (ep.FutureDate <= date && ep.FutureDate > DateTime.MinValue)
                        {
                            views.Add(new InnerArchiveView(ep, info.Name, info.GetWorkingHours()));
                        }
                    }
                }
            }

            return views;
        }
        public List<MaterialView> GetMaterialViewsByMaintenance(int maintenanceId, bool isAdditional)
        {
            List<MaterialView> materials = new List<MaterialView>();
            if (Materials != null)
            {
                if (isAdditional)
                {
                    if (Additionals != null)
                    {
                        var additional = Additionals.FirstOrDefault(x => x.Id == maintenanceId);
                        if (additional != null && additional.materialIds != null)
                        {
                            materials = Materials.Where(x => additional.materialIds.Contains(x.Id)).ToList();
                        }
                    }
                }
                else
                {
                    if (Maintenances != null)
                    {
                        var maintenance = Maintenances.FirstOrDefault(x => x.Id == maintenanceId);
                        if (maintenance != null && maintenance.materialIds != null)
                        {
                            materials = Materials.Where(x => maintenance.materialIds.Contains(x.Id)).ToList();
                        }
                    }
                }
            }
            return materials;
        }

        public int SavePassport(string name, string serialNumber, string inventoryNumber,
            DateTime releaseYear, DateTime commissioningDate, DateTime decommissioningDate, DateTime guaranteeEndDate, double power,
            int supplierId, int typeId, int departmentId, int pointId, int operatorId)
        {

            if (TechPassport.Name != name)
            {
                TechPassport.Name = name;
                isPassportInfoChanged = true;
            }
            if (TechPassport.SerialNumber != serialNumber)
            {
                TechPassport.SerialNumber = serialNumber;
                isPassportInfoChanged = true;
            }
            if (TechPassport.InventoryNumber != inventoryNumber)
            {
                TechPassport.InventoryNumber = inventoryNumber;
                isPassportInfoChanged = true;
            }
            if (TechPassport.ReleaseYear != releaseYear)
            {
                TechPassport.ReleaseYear = releaseYear;
                isPassportInfoChanged = true;
            }
            if (TechPassport.CommissioningDate != commissioningDate)
            {
                TechPassport.CommissioningDate = commissioningDate;
                isPassportInfoChanged = true;
            }
            if (TechPassport.DecommissioningDate != decommissioningDate)
            {
                TechPassport.DecommissioningDate = decommissioningDate;
                isPassportInfoChanged = true;
            }
            if (TechPassport.GuaranteeEndDate != guaranteeEndDate)
            {
                TechPassport.GuaranteeEndDate = guaranteeEndDate;
                isPassportInfoChanged = true;
            }
            if (TechPassport.Power != power)
            {
                TechPassport.Power = power;
                isPassportInfoChanged = true;
            }

                if (supplierId > 0)
            {
                var sup = dataService.GetSupplierById(supplierId);
                if (sup != null && TechPassport.Supplier != sup)
                {
                    TechPassport.Supplier = sup;
                    isPassportInfoChanged = true;
                }
            }

            if (typeId > 0)
            {
                var type = dataService.GetTypeById(typeId);
                if (type != null && TechPassport.Type != type)
                {
                    TechPassport.Type = type;
                    isPassportInfoChanged = true;
                }
            }

            if (departmentId > 0)
            {
                var department = dataService.GetDepartmentById(departmentId);
                if (department != null && TechPassport.Department != department)
                {
                    TechPassport.Department = department;
                    isPassportInfoChanged = true;
                }
            }

            if (pointId > 0)
            {
                var point = dataService.GetPointById(pointId);
                if (point != null && TechPassport.ElectroPoint != point)
                {
                    TechPassport.ElectroPoint = point;
                    isPassportInfoChanged = true;
                }
            }

            if (operatorId > 0)
            {
                var op = dataService.GetOperatorById(operatorId);
                if (op != null && TechPassport.Operator != op)
                {
                    TechPassport.Operator = op;
                    isPassportInfoChanged = true;
                }
            }

            if (TechPassport.Id > 0)
            {
                if (isPassportInfoChanged)
                {
                    dataService.EditTechPassportBaseInfo(TechPassport);
                    isPassportInfoChanged = false;
                }
            }
            else
            {
                TechPassport.Id = dataService.AddTechPassportBaseInfo(TechPassport);
            }

            SaveCharacteristics();

            SaveMaintenances();

            SaveAdditionals();

            SaveInstructions();

            SaveInstruments();

            SaveHours();

            SaveErrors();

            SaveControledParams();

            SaveControledParamEpisodes();

            return TechPassport.Id;
        }

        private void RefreshParamIds(int oldId, int id)
        {
            var paramEpisodes = ControledParametrEpisodes.Where(ep => ep.GetParamId() == oldId).ToList();
            foreach (var paramEpisode in paramEpisodes)
            {
                paramEpisode.ChangeParamId(id);
            }
        }

        private void SaveControledParamEpisodes()
        {
            if (TechPassport != null)
            {
                var changedParamEpisodes = ControledParametrEpisodes.Where(c => c.IsChanged()).ToList();
                if (changedParamEpisodes != null)
                {
                    foreach (ControledParametrEpisodeView cPE in changedParamEpisodes)
                    {
                        if (cPE != null)
                        {
                            //int id = cPE.Id;

                            int id = Data.Instance.EditControledParamEpisode( cPE.Id, cPE.Date, cPE.Count, cPE.GetParamId());
                            if (id != 0)
                            {
                                cPE.Id = id;
                            }
                            cPE.MarkUnChanged();
                        }
                    }
                }
            }
        }

        private void SaveControledParams()
        {
            if (TechPassport != null)
            {
                var changedParams = ControledParametrs.Where(c => c.IsChanged()).ToList();
                if (changedParams != null)
                {
                    foreach (ControledParametrView cP in changedParams)
                    {
                        if (cP != null)
                        {
                            if (TechPassport.ControledParametrs != null)
                            {
                                var ch = TechPassport.ControledParametrs.FirstOrDefault(x => x.Id == cP.Id);
                                if (ch != null)
                                {
                                    Data.Instance.EditControledParam(this.TechPassport.Id, cP.Id, cP.Name, cP.Nominal, cP.GetUnitId());
                                }
                                else
                                {
                                    int oldId = cP.Id;
                                    int id = Data.Instance.AddControledParam(this.TechPassport.Id, cP.Name, cP.Nominal, cP.GetUnitId());
                                    RefreshParamIds(oldId, id);
                                    cP.Id = id;
                                }
                            }
                            else
                            {
                                int oldId = cP.Id;
                                int id = Data.Instance.AddControledParam(this.TechPassport.Id, cP.Name, cP.Nominal, cP.GetUnitId());
                                RefreshParamIds(oldId, id);
                                cP.Id = id;
                            }
                            cP.MarkUnChanged();
                        }
                    }
                }
            }
        }

        private void SaveInstruments()
        {
            if (TechPassport != null)
            {
                var changedInstruments = Instruments.Where(c => c.IsChanged()).ToList();
                if (changedInstruments != null)
                {
                    foreach (InstrumentView ins in changedInstruments)
                    {
                        if (ins != null)
                        {
                            if (TechPassport.Instruments != null)
                            {
                                var ch = TechPassport.Instruments.FirstOrDefault(x => x.Id == ins.Id);
                                if (ch != null)
                                {
                                    Data.Instance.EditInstrument(TechPassport.Id, ins.Id, ins.Art, ins.Name, ins.GetCount(), ins.GetUnitId(),
                                    ins.CreateDate, ins.RemoveDate, ins.RemoveReason, ins.Commentary);
                                }
                                else
                                {
                                    ins.Id = Data.Instance.AddInstrument(TechPassport.Id, ins.Art, ins.Name, ins.GetCount(), ins.GetUnitId(),
                                    ins.CreateDate, ins.RemoveDate, ins.RemoveReason, ins.Commentary);
                                }
                            }
                            else
                            {
                                ins.Id = Data.Instance.AddInstrument(TechPassport.Id, ins.Art, ins.Name, ins.GetCount(), ins.GetUnitId(),
                                    ins.CreateDate, ins.RemoveDate, ins.RemoveReason, ins.Commentary);
                            }
                            ins.MarkUnChanged();
                        }
                    }
                }
            }
        }

        private void SaveErrors()
        {
            //var TechPassport = Data.Instance.GetPassportById(this.TechPassport.Id);
            if (TechPassport != null)
            {
                var changedErrors = Errors.Where(c => c.IsChanged()).ToList();
                if (changedErrors != null)
                {
                    foreach (ErrorNewView er in changedErrors)
                    {
                        if (er != null)
                        {
                            if (TechPassport.Errors != null)
                            {
                                var ch = TechPassport.Errors.FirstOrDefault(x => x.Id == er.Id);
                                if (ch != null)
                                {
                                    Data.Instance.EditErrorNew(this.TechPassport.Id, er.Id, er.Date, er.Code, er.Name, er.GetWorking(), er.Description, er.Comment, er.DateOfSolving);
                                }
                                else
                                {
                                    er.Id = Data.Instance.AddErrorNew(this.TechPassport.Id, er.Date, er.Code, er.Name, er.GetWorking(), er.Description, er.Comment, er.DateOfSolving);
                                }
                            }
                            else
                            {
                                er.Id = Data.Instance.AddErrorNew(this.TechPassport.Id, er.Date, er.Code, er.Name, er.GetWorking(), er.Description, er.Comment, er.DateOfSolving);
                            }
                            er.MarkUnChanged();
                        }
                    }
                }
            }
        }
        private void SaveAdditionals()
        {
            //var TechPassport = Data.Instance.GetPassportById(this.TechPassport.Id);
            if (TechPassport != null)
            {
                var changedAdditionals = Additionals.Where(c => c.IsChanged()).ToList();
                if (changedAdditionals != null)
                {
                    foreach (AdditionalWorkView a in changedAdditionals)
                    {
                        if (a != null)
                        {
                            int id = a.Id;
                            if (TechPassport.AdditionalWorks != null)
                            {
                                var ch = TechPassport.AdditionalWorks.FirstOrDefault(x => x.Id == a.Id);
                                if (ch != null)
                                {
                                    Data.Instance.EditAdditionalWork(this.TechPassport.Id, a.Id, a.Name, a.FutureDate, a.DateFact, a.Comment, a.GetWorkingHours(), a.GetWorkingHoursFact());
                                }
                                else
                                {
                                    id = Data.Instance.AddAdditionalWork(this.TechPassport.Id, a.Name, a.FutureDate, a.DateFact, a.Comment, a.GetWorkingHours(), a.GetWorkingHoursFact());

                                }
                            }
                            else
                            {
                                id = Data.Instance.AddAdditionalWork(this.TechPassport.Id, a.Name, a.FutureDate, a.DateFact, a.Comment, a.GetWorkingHours(), a.GetWorkingHoursFact());
                            }
                            Data.Instance.EditAdditionalByType(id, a.TypeId);
                            Data.Instance.EditAdditionalByOperators(id, a.operatorIds);

                            List<int> materialIds = SaveMaterials(a.materialIds, id, true);
                            Data.Instance.EditAdditionalByMaterials(id, materialIds);

                            a.Id = id;
                            a.MarkUnChanged();
                        }
                    }
                }
            }
        }
        private void SaveMaintenances()
        {
            //var TechPassport = Data.Instance.GetPassportById(this.TechPassport.Id);
            if (TechPassport != null)
            {
                var changedMaintenances = Maintenances.Where(c => c.IsChanged()).ToList();
                if (changedMaintenances != null)
                {
                    foreach (MaintenanceNewView m in changedMaintenances)
                    {
                        if (m != null)
                        {
                            int id = m.Id;
                            var mEpisodes = Episodes.Where(x => x.MaintenanceId == m.Id).ToList();
                            double interval = m.IsFixed ? m.GetDaysIntervalTime() : m.GetHoursIntervalTime();
                            DateTime? futureDate = m.IsDateChanged() ? m.FutureDate : null;
                            if (TechPassport.MaintenanceInfos != null)
                            {
                                var ch = TechPassport.MaintenanceInfos.FirstOrDefault(x => x.Id == m.Id);
                                if (ch != null)
                                {

                                    Data.Instance.EditMaintanance(this.TechPassport.Id, m.Id, m.Name, m.TypeId, m.IsFixed, interval, m.GetWorkingHours(), futureDate, m.IsInWork());
                                    if (mEpisodes != null && mEpisodes.Count > 0)
                                    {
                                        DateTime oldEpisodeDate = mEpisodes.Where(x=> !x.IsDone).Min(x => x.FutureDate);
                                        TimeSpan delta = m.FutureDate - oldEpisodeDate;
                                        foreach (var ep in mEpisodes)
                                        {
                                            DateTime dateForChange = ep.FutureDate;
                                            if (m.IsDateChanged())
                                            {
                                                if (ep.FutureDate == oldEpisodeDate)
                                                {
                                                    dateForChange = m.FutureDate;
                                                }
                                                else if (ep.FutureDate > oldEpisodeDate)
                                                {
                                                    dateForChange += delta;
                                                }
                                            }
                                                
                                            var oldEp = Data.Instance.GetMaintananceEpisode(ep.Id);
                                            if (oldEp == null)
                                            {
                                                Data.Instance.AddMaintananceEpisode(id, dateForChange, ep.WorkingHours, ep.OperatorIds, ep.IsDone);
                                            }
                                            else
                                            {
                                                Data.Instance.EditMaintananceEpisode(ep.Id, dateForChange, ep.WorkingHours, ep.OperatorIds, ep.IsDone);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    id = Data.Instance.AddMaintanance(this.TechPassport.Id, m.Name, m.TypeId, m.IsFixed, interval, m.GetWorkingHours(), futureDate, m.IsInWork());
                                    foreach (var ep in mEpisodes)
                                    {
                                        Data.Instance.AddMaintananceEpisode(id, ep.FutureDate, ep.WorkingHours, ep.OperatorIds, ep.IsDone);
                                    }
                                }
                            }
                            else
                            {
                                id = Data.Instance.AddMaintanance(this.TechPassport.Id, m.Name, m.TypeId, m.IsFixed, interval, m.GetWorkingHours(), futureDate, m.IsInWork());
                                foreach (var ep in mEpisodes)
                                {
                                    Data.Instance.AddMaintananceEpisode(id, ep.FutureDate, ep.WorkingHours, ep.OperatorIds, ep.IsDone);
                                }
                            }
                            //Data.Instance.EditMaintenanceByType(id, m.TypeId);

                            List<int> materialIds = SaveMaterials(m.materialIds, id, false);
                            Data.Instance.EditMaintenanceByMaterials(id, materialIds);

                            m.Id = id;
                            m.MarkUnChanged();
                        }
                    }
                }
            }
        }
        private List<int> SaveMaterials(List<int> ids, int workId, bool isAdditional)
        {
            List<int> result = new List<int>();
            foreach (int mId in ids)
            {
                var m = Materials.FirstOrDefault(x => x.Id == mId);
                if (m != null)
                {
                    var oldM = Data.Instance.GetMaterials().FirstOrDefault(i => i.Id == mId);
                    double.TryParse(m.Count, out double count);
                    if (oldM != null)
                    {
                        if (isAdditional)
                        {
                            Data.Instance.EditMaterial(m.Id, m.InfoId, count, workId);
                        }
                        else
                        {
                            Data.Instance.EditMaterialForAdditional(m.Id, m.InfoId, count, workId);
                        }
                        result.Add(m.Id);
                    }
                    else
                    {
                        int id = Data.Instance.AddMaterial(m.InfoId, count, workId, isAdditional);
                        if (id > 0)
                        {
                            result.Add(id);
                        }
                    }
                }
            }
            return result;
        }
        private void SaveCharacteristics()
        {
            //var TechPassport = Data.Instance.GetPassportById(this.TechPassport.Id);
            if (TechPassport != null)
            {
                var changedCharacteristics = Characteristics.Where(c => c.IsChanged()).ToList();
                if (changedCharacteristics != null)
                {
                    foreach (CharacteristicView cV in changedCharacteristics)
                    {
                        if (cV != null)
                        {
                            if (TechPassport.Characteristics != null)
                            {
                                var ch = TechPassport.Characteristics.FirstOrDefault(x => x.Id == cV.Id);
                                if (ch != null)
                                {
                                    Data.Instance.EditCharacteristic(this.TechPassport.Id, cV.Id, cV.Name, cV.GetCount(), cV.Commentary);
                                    Data.Instance.EditCharacteristicsByUnit(cV.Id, cV.GetUnitId());
                                }
                                else
                                {
                                    cV.Id = Data.Instance.AddCharacteristic(this.TechPassport.Id, cV.Name, cV.GetCount(), cV.Commentary);
                                    Data.Instance.EditCharacteristicsByUnit(cV.Id, cV.GetUnitId());
                                }
                            }
                            else
                            {
                                cV.Id = Data.Instance.AddCharacteristic(this.TechPassport.Id, cV.Name, cV.GetCount(), cV.Commentary);
                                Data.Instance.EditCharacteristicsByUnit(cV.Id, cV.GetUnitId());
                            }
                            cV.MarkUnChanged();
                        }
                    }
                }
            }
        }
        private void SaveInstructions()
        {
            //var TechPassport = Data.Instance.GetPassportById(this.TechPassport.Id);
            if (TechPassport != null)
            {
                var changedInstructions = Instructions.Where(c => c.IsChanged()).ToList();
                if (changedInstructions != null)
                {
                    foreach (InstructionView i in changedInstructions)
                    {
                        if (i != null)
                        {
                            if (TechPassport.Instructions != null)
                            {
                                var ch = TechPassport.Instructions.FirstOrDefault(x => x.Id == i.Id);
                                if (ch != null)
                                {
                                    Data.Instance.EditInstruction(this.TechPassport.Id, i.Id, i.Name, i.Path);
                                }
                                else
                                {
                                    i.Id = Data.Instance.AddInstruction(this.TechPassport.Id, i.Name, i.Path);
                                }
                            }
                            else
                            {
                                i.Id = Data.Instance.AddInstruction(this.TechPassport.Id, i.Name, i.Path);
                            }
                            i.MarkUnChanged();
                        }                    
                    }
                }
            }
        }
        private void SaveHours()
        {
            //var TechPassport = Data.Instance.GetPassportById(this.TechPassport.Id);
            if (TechPassport != null)
            {
                var changedWorkingHours = WorkingHours.Where(c => c.IsChanged()).ToList();
                if (changedWorkingHours != null)
                {
                    foreach (HourView h in changedWorkingHours)
                    {
                        if (h != null)
                        {
                            if (TechPassport.WorkingHours != null)
                            {
                                var ch = TechPassport.WorkingHours.FirstOrDefault(x => x.Id == h.Id);
                                if (ch != null)
                                {
                                    Data.Instance.EditHours(this.TechPassport.Id, h.Id, h.Hours, h.Date);
                                }
                                else
                                {
                                    h.Id = Data.Instance.AddHours(this.TechPassport.Id, h.Hours, h.Date);
                                }
                            }
                            else
                            {
                                h.Id = Data.Instance.AddHours(this.TechPassport.Id, h.Hours, h.Date);
                            }

                            h.MarkUnChanged();
                        }
                    }
                }
            }
        }
        public void EditAdditionalByType(int id, int maintenanceTypeId)
        {
            var work = Additionals.FirstOrDefault(x => x.Id == id);
            var type = dataService.GetMaintenanceTypeViews().FirstOrDefault(x => x.Id == maintenanceTypeId);
            work.TypeId = maintenanceTypeId;
            work.EditType(type);
        }
        public void EditMaintenanceByType(int id, int maintenanceTypeId)
        {
            var work = Maintenances.FirstOrDefault(x => x.Id == id);
            var type = dataService.GetMaintenanceTypeViews().FirstOrDefault(x => x.Id == maintenanceTypeId);
            work.TypeId = maintenanceTypeId;
            work.EditType(type);
        }

        public void EditAdditionalByOperators(int id, List<int> operatorIds)
        {
            var work = Additionals.FirstOrDefault(x => x.Id == id);
            var ops = dataService.GetOperatorViews().Where(n => operatorIds.Contains(n.Id));
            foreach (var op in ops)
            {
                work.operatorIds.Add(op.Id);
            }
            work.EditOperators(ops);
        }
        public void EditAdditionalByMaterials(int id, List<int> materialsIds)
        {
            var work = Additionals.FirstOrDefault(x => x.Id == id);
            var ops = Materials.Where(n => materialsIds.Contains(n.Id));
            if (work != null)
            {
                foreach (var op in ops)
                {
                    work.materialIds.Add(op.Id);
                }
                work.EditMaterials(ops);
            }
        }
        public void EditMaintenanceByMaterials(int id, List<int> materialsIds)
        {
            var work = Maintenances.FirstOrDefault(x => x.Id == id);
            var ops = Materials.Where(n => materialsIds.Contains(n.Id));
            if (work != null)
            {
                foreach (var op in ops)
                {
                    work.materialIds.Add(op.Id);
                }
                work.EditMaterials(ops);
            }
        }
        public void EditErrorWorking(int id, bool isWorking)
        {
            var error = Errors.FirstOrDefault(x => x.Id == id);
            if (error != null)
            {
                error.EditWorking(isWorking);
            }
        }
        public void EditInstructionPath(int id, string path)
        {
            var ins = Instructions.FirstOrDefault(x => x.Id == id);
            if (ins != null)
            {
                ins.EditPath(path);
            }
        }
        public void EditControlParamEpisode(int id, int paramId)
        {
            var cPE = ControledParametrEpisodes.FirstOrDefault(x => x.Id == id);
            if (cPE != null)
            {
                var cP = ControledParametrs.FirstOrDefault(x => x.Id == paramId);
                if (cP != null)
                {
                    cPE.EditParam(cP);
                    cPE.MarkChanged();
                }
            }
        }

        public int AddControlParamEpisode(int infoId, double count, DateTime date)
        {
            var cPE = new ControledParametrEpisodeView();
            int id = ControledParametrEpisodesId++;
            var paramInfo = ControledParametrs.FirstOrDefault(c => c.Id == infoId);
            if (paramInfo != null)
            {
                cPE.EditParam(paramInfo);
            }
            cPE.Count = count;
            cPE.Date = date;
            cPE.Id = id;
            cPE.MarkChanged();
            
            ControledParametrEpisodes.Add(cPE);
            return id;
        }

        public void EditInstrument(int id, int unitId)
        {
            var ins = Instruments.FirstOrDefault(x => x.Id == id);
            if (ins != null)
            {
                var u = dataService.GetUnitViews().FirstOrDefault(x => x.Id == unitId);
                if (u != null)
                {
                    ins.EditUnit(u);
                    ins.MarkChanged();
                }
            }
        }

        public int AddInstrument(int unitId, string name, string nominal)
        {
            var cP = new InstrumentView();
            int id = InstrumentsId++;
            var u = dataService.GetUnitViews().FirstOrDefault(c => c.Id == unitId);
            if (u != null)
            {
                cP.EditUnit(u);
            }
            cP.Name = name;
            cP.Count = nominal;
            cP.Id = id;

            Instruments.Add(cP);
            cP.MarkChanged();
            return id;
        }

        public void EditControlParam(int id, int unitId)
        {
            var cPE = ControledParametrs.FirstOrDefault(x => x.Id == id);
            if (cPE != null)
            {
                var u = dataService.GetUnitViews().FirstOrDefault(x => x.Id == unitId);
                if (u != null)
                {
                    cPE.EditUnit(u);
                    cPE.MarkChanged();
                }
            }
        }

        public int AddControlParam(int unitId, string name, double nominal)
        {
            var cP = new ControledParametrView();
            int id = ControledParametrsId++;
            var u = dataService.GetUnitViews().FirstOrDefault(c => c.Id == unitId);
            if (u != null)
            {
                cP.EditUnit(u);
            }
            cP.Name = name;
            cP.Nominal = nominal;
            cP.Id = id;

            ControledParametrs.Add(cP);
            cP.MarkChanged();
            return id;
        }
        public void EditMaterialByInfo(int id, int infoId)
        {
            MaterialView m = Materials.FirstOrDefault(x => x.Id == id);
            var info = dataService.GetMaterialInfoViews().FirstOrDefault(c => c.Id == infoId);
            m.EditInfo(info);
        }
        public int AddMaterial(int infoId, string count, int maintenanceId, bool isAdditional)
        {
            var material = new MaterialView();
            //int id = Materials.Any() ? Materials.Max(x => x.Id) + 1 : 1;
            int id = MaterialsId++;
            var materialInfo = dataService.GetMaterialInfoViews().FirstOrDefault(c => c.Id == infoId);
            if (materialInfo != null)
            {
                material.EditInfo(materialInfo);
            }
            material.Count = count;
            material.Id = id;
            if (isAdditional)
            {
                var additionalWork = Additionals.FirstOrDefault(m => m.Id == maintenanceId);
                if (additionalWork != null)
                {
                    additionalWork.AddMaterial(material);
                }
            }
            else
            {
                var maintenanceInfo = Maintenances.FirstOrDefault(m => m.Id == maintenanceId);
                if (maintenanceInfo != null)
                {
                    maintenanceInfo.AddMaterial(material);
                }
            }
            Materials.Add(material);
            return id;
        }
        public void ErasePlannedDate(int id)
        {
            var maintenance = Maintenances.FirstOrDefault(x => x.Id == id);
            if (maintenance != null)
            {
                maintenance.FutureDate = DateTime.MinValue;
                RecountDate(maintenance);
            }
        }
        public void RecountDateWithEpisodes(MaintenanceNewView maintenance)
        {
            RecountDate(maintenance);
            var eps = Episodes.Where(a => a.MaintenanceId == maintenance.Id && a.FutureDate.Date >= DateTime.Today).OrderBy(a => a.FutureDate).ToArray();
            var dates = maintenance.GetPlannedDates(maintenance.FutureDate, eps.Length);
            for (int i = 0; i<eps.Length; i++)
            {
                if (!eps[i].IsDone)
                {
                    DateTime oldDate = eps[i].FutureDate;
                    eps[i].FutureDate = dates[i];
                    if (oldDate != eps[i].FutureDate)
                    {
                        maintenance.ChangeEpisodeDates(oldDate, eps[i].FutureDate);
                    }
                }
            }
        }
        public void RecountDate(MaintenanceNewView maintenance)
        {
            maintenance.RecountDate();
            maintenance.MarkChanged();
        }
        public void ChangePlannedDate(int id, DateTime date)
        {
            var maintenance = Maintenances.FirstOrDefault(x => x.Id == id);
            if (maintenance != null)
            {
                maintenance.FutureDate = date;
                maintenance.MarkChanged();
            }
        }

        public void ChangeAdditionalInfo(int id, DateTime date, List<int> operatorIds)
        {
            var add = Additionals.FirstOrDefault(x => x.Id == id);
            if (add != null)
            {
                add.FutureDate = date;
                add.operatorIds = operatorIds;
                add.MarkChanged();
            }
        }
        public void ChangeAdditionalFutureDate(int id, DateTime date, double hours, List<int> operatorIds)
        {
            var add = Additionals.FirstOrDefault(x => x.Id == id);
            if (add != null)
            {
                add.DateFact = date;
                add.SetWorkingHoursFact(hours);
                add.operatorIds = operatorIds;
                add.FutureDate = DateTime.MinValue;
                add.MarkChanged();
            }
        }
        public void AddMaintananceEpisode(int maintenanceId, DateTime date, double hoursOnWork, List<int> workerIds)
        {
            var maintenance = Maintenances.FirstOrDefault(x => x.Id == maintenanceId);
            if (maintenance != null)
            {
                int id = EpisodesId++;
                Episodes.Add(new MaintenanceEpisodeView
                {
                    Id = id,
                    FutureDate = date,
                    WorkingHours = hoursOnWork,
                    MaintenanceId = maintenanceId,
                    OperatorIds = workerIds,
                    IsDone = true
                });
                maintenance.AddEpisodeDate(date);
                RecountDate(maintenance);
            }
        }
        public int AddUndoneEpisode(int maintenanceId, DateTime date, List<int> workerIds, DateTime oldDate)
        {
            var maintenance = Maintenances.FirstOrDefault(x => x.Id == maintenanceId);
            if (maintenance != null)
            {
                int id = EpisodesId++;
                Episodes.Add(new MaintenanceEpisodeView
                {
                    Id = id,
                    FutureDate = date,
                    MaintenanceId = maintenanceId,
                    OperatorIds = workerIds,
                    IsDone = false,   
                    Name = maintenance.Name,
                    Type = maintenance.Type
                });
                maintenance.AddEpisodeDate(date);
                TimeSpan delta = oldDate - date;
                if (delta != TimeSpan.Zero)
                {
                    var eps = Episodes.Where(a => a.MaintenanceId == maintenance.Id && a.FutureDate.Date > oldDate);
                    foreach (var e in eps)
                    {
                        DateTime old = e.FutureDate;
                        e.FutureDate += delta;
                        maintenance.ChangeEpisodeDates(old, e.FutureDate);
                    }
                }
                RecountDate(maintenance);
                return id;
            }
            return 0;
        }
        public void MakeMaintananceEpisodeDone(int episodeId, DateTime date, double hoursOnWork, List<int> workerIds)
        {
            var episode = Episodes.FirstOrDefault(a => a.Id == episodeId);
            if (episode != null)
            {
                episode.FutureDate = date;
                episode.WorkingHours = hoursOnWork;
                episode.OperatorIds = workerIds;
                episode.IsDone = true;

                var maintenance = Maintenances.FirstOrDefault(a => a.Id == episode.Id);
                if (maintenance != null)
                {
                    maintenance.MarkChanged();
                }
            }            
        }
        public void ChangeEpisodeInfo(int episodeId, DateTime date, List<int> workerIds)
        {
            var episode = Episodes.FirstOrDefault(a => a.Id == episodeId);           
            if (episode != null)
            {
                DateTime oldDate = episode.FutureDate;
                episode.FutureDate = date;
                episode.OperatorIds = workerIds;
                episode.IsDone = false;

                var maintenance = Maintenances.FirstOrDefault(a => a.Id == episode.MaintenanceId);
                if (maintenance != null)
                {
                    maintenance.MarkChanged();
                    if (oldDate != date)
                    {
                        maintenance.ChangeEpisodeDates(oldDate, date);
                    }

                    TimeSpan delta = oldDate - date;
                    if (delta != TimeSpan.Zero)
                    {
                        var eps = Episodes.Where(a => a.MaintenanceId == maintenance.Id && a.FutureDate.Date > date);
                        foreach (var e in eps)
                        {
                            DateTime old = e.FutureDate;
                            e.FutureDate += delta;
                            maintenance.ChangeEpisodeDates(old, e.FutureDate);
                        }
                    }
                    RecountDate(maintenance);
                }
            }
            
        }

        public void RecountMaintenances()
        {
            foreach (var m in Maintenances)
            {
                if (!m.IsDateChanged())
                {
                    RecountDateWithEpisodes(m);
                }
            }
        }


    }
}
