using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class Data
    {
        private List<MaterialInfoFromOuterBase> materials;

        private static Data instance = new Data();
        public static Data Instance
        {
            get
            {
                if (instance == null)
                    instance = new Data();
                return instance;
            }

        }

        private Data()
        {
            var context = new MainContext();
            materials = new List<MaterialInfoFromOuterBase>();

            try
            {
                using (AdditionalContext db = new AdditionalContext())
                {
                    string monthPrefix = DateTime.Today.Month < 10 ? "0" : "";
                    string sqlParams = @"declare @DtBeg datetime='"
                        + DateTime.Today.Year.ToString() + monthPrefix + DateTime.Today.Month.ToString() +
                        DateTime.Today.Day.ToString() +
                        @"' declare @DtEnd datetime='" + DateTime.Today.Year.ToString() + monthPrefix + DateTime.Today.Month.ToString() +
                        DateTime.Today.Day.ToString() + @" 23:59:59' ";
                    materials = db.MaterialsInfoFromOuterBase.FromSqlRaw(sqlParams + sqlExpression).ToList();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<MaterialInfoFromOuterBase> MaterialsInfoFromOuterBase()
        {
            return materials;
        }

        public List<TechPassport> GetTechPassports()
        {
            using var context = new MainContext();
            return context.TechPassports
                .Include(d => d.Department)
                .ToList();
        }
        public int GetCharacteristicsId()
        {
            using var context = new MainContext();
            return context.Characteristics.Any() ? context.Characteristics.Max(x => x.Id) + 1 : 1;
        }
        public int GetMaintenancesId()
        {
            using var context = new MainContext();
            return context.MaintenanceInfos.Any() ? context.MaintenanceInfos.Max(x => x.Id) + 1 : 1;
        }
        public int GetAdditionalsId()
        {
            using var context = new MainContext();
            return context.AdditionalWorks.Any() ? context.AdditionalWorks.Max(x => x.Id) + 1 : 1;
        }
        public int GetMaterialsId()
        {
            using var context = new MainContext();
            return context.Materials.Any() ? context.Materials.Max(x => x.Id) + 1 : 1;
        }
        public int GetRepairingsId()
        {
            using var context = new MainContext();
            return context.Repairings.Any() ? context.Repairings.Max(x => x.Id) + 1 : 1;
        }
        public int GetInstructionsId()
        {
            using var context = new MainContext();
            return context.Instructions.Any() ? context.Instructions.Max(x => x.Id) + 1 : 1;
        }
        public int GetInstrumentsId()
        {
            using var context = new MainContext();
            return context.Instruments.Any() ? context.Instruments.Max(x => x.Id) + 1 : 1;
        }
        public int GetWorkingHoursId()
        {
            using var context = new MainContext();
            return context.WorkingHours.Any() ? context.WorkingHours.Max(x => x.Id) + 1 : 1;
        }
        public int GetErrorsId()
        {
            using var context = new MainContext();
            return context.MaintenanceErrors.Any() ? context.MaintenanceErrors.Max(x => x.Id) + 1 : 1;
        }
        public int GetEpisodesId()
        {
            using var context = new MainContext();
            return context.MaintenanceEpisodes.Any() ? context.MaintenanceEpisodes.Max(x => x.Id) + 1 : 1;
        }

        public int GetControledParametrsId()
        {
            using var context = new MainContext();
            return context.ControledParametrs.Any() ? context.ControledParametrs.Max(x => x.Id) + 1 : 1;
        }

        public int GetControledParametrEpisodesId()
        {
            using var context = new MainContext();
            return context.ControledParametrDateInfos.Any() ? context.ControledParametrDateInfos.Max(x => x.Id) + 1 : 1;
        }

        public List<MaintenanceError> GetErrors()
        {
            using var context = new MainContext();
            return context.MaintenanceErrors.Include(s => s.Repairings).Include(x => x.TechPassport).Where(x => x.IsActive == null || x.IsActive.Value).ToList();
        }

        public List<AdditionalWork> GetAdditionalWorks()
        {
            using var context = new MainContext();
            return context.AdditionalWorks
                .Include(a => a.Materials)
                .Include(m => m.Operators)
                .Include(x => x.TechPassport)
                .Include(e => e.MaintenanceType).ToList();
        }

        public List<ErrorCode> GetErrorCodes()
        {
            using var context = new MainContext();
            return context.ErrorCodes.ToList();
        }

        public MaintenanceType? GetMaintenanceTypeByMaintenanceId(int id)
        {
            using var context = new MainContext();
            var maintenanceInfo = context.MaintenanceInfos.Include(t => t.MaintenanceType).FirstOrDefault(i => i.Id == id);
            return maintenanceInfo?.MaintenanceType;
        }

        public List<ControledParametrDateInfo> GetEpisodesByControlParametr(int id)
        {
            using var context = new MainContext();
            List<ControledParametrDateInfo> result = new List<ControledParametrDateInfo>();
            var controlParam = context.ControledParametrs.Include(e => e.Episodes).FirstOrDefault(x => x.Id == id);
            if (controlParam != null && controlParam.Episodes != null)
            {
                result = controlParam.Episodes.ToList();
            }
            return result;
        }

        public TechPassport? GetPassportById(int id)
        {
            using var context = new MainContext();
            return context.TechPassports.
                Include(m => m.MaintenanceInfos).ThenInclude(me=> me.Episodes).
                Include(e => e.Errors).
                Include(w => w.Downtimes).
                Include(c => c.Characteristics).ThenInclude(u => u.Unit).
                Include(i => i.Instructions).
                Include(h => h.WorkingHours).
                Include(p => p.ControledParametrs).
                Include(a => a.AdditionalWorks).ThenInclude(o => o.Operators).
                Include(a => a.AdditionalWorks).ThenInclude(o => o.MaintenanceType).
                Include(s => s.Supplier).
                Include(d => d.Department).
                Include(t => t.Type).
                Include(s => s.Instruments).ThenInclude(u => u.Unit).
                FirstOrDefault(t => t.Id == id);
        }

        public List<TechPassport> GetPassportByIds(List<int> ids)
        {
            using var context = new MainContext();
            return context.TechPassports.
                Include(m => m.MaintenanceInfos).
                Include(e => e.Errors).
                Include(w => w.Downtimes).
                Include(c => c.Characteristics).ThenInclude(u => u.Unit).
                Include(i => i.Instructions).
                Include(h => h.WorkingHours).
                Include(p => p.ControledParametrs).
                Include(a => a.AdditionalWorks).ThenInclude(o => o.Operators).
                Include(s => s.Supplier).
                Include(d => d.Department).
                Include(t => t.Type).
                Include(s => s.Instruments).ThenInclude(u => u.Unit).
                Where(t => ids.Contains(t.Id))
                .OrderBy(d => d.Department.Number)
                .ToList();
        }


        public List<TechPassport> GetFullTechPassports()
        {
            using var context = new MainContext();
            return context.TechPassports.
                Include(m => m.MaintenanceInfos).
                Include(e => e.Errors).
                Include(c => c.Characteristics).
                Include(i => i.Instructions).
                Include(h => h.WorkingHours).
                Include(p => p.ControledParametrs).
                Include(a => a.AdditionalWorks).
                Include(s => s.Supplier).
                Include(d => d.Department).
                Include(t => t.Type).ToList();
        }
        public List<EquipmentType> GetTypes()
        {
            using var context = new MainContext();
            return context.EquipmentTypes.ToList();
        }

        public List<Unit> GetUnitTypes()
        {
            using var context = new MainContext();
            return context.Units.ToList();
        }

        public List<Department> GetDepartmentTypes()
        {
            using var context = new MainContext();
            return context.Departments.ToList();
        }

        public List<MaintenanceType> GetMaintenanceTypes()
        {
            using var context = new MainContext();
            return context.MaintenanceTypes.ToList();
        }

        public List<Operator> GetOperators()
        {
            using var context = new MainContext();
            return context.Operators.ToList();
        }

        public List<EquipmentSupplier> GetSuppliers()
        {
            using var context = new MainContext();
            return context.EquipmentSuppliers.ToList();
        }

        public List<MaintenanceInfo> GetMaintenance()
        {
            using var context = new MainContext();
            return context.MaintenanceInfos
                .Include(e => e.Episodes).ThenInclude(o => o.Operators)
                .Include(t => t.MaintenanceType)
                .Include(p => p.TechPassport).ThenInclude(h => h.WorkingHours).ToList();
        }

        public void EditCharacteristicsByUnit(int id, int unitId)
        {
            using var context = new MainContext();
            Characteristic? characteristic = context.Characteristics.FirstOrDefault(x => x.Id == id);
            Unit? unit = context?.Units?.FirstOrDefault(c => c.Id == unitId);
            if (unit != null && characteristic != null)
            {
                characteristic.Unit = unit;
                context.SaveChanges();
            }
        }

        public void EditMaterialInfoByUnit(int id, int unitId)
        {
            using var context = new MainContext();
            MaterialInfo? info = context.MaterialInfos.FirstOrDefault(x => x.Id == id);
            if (info != null)
            {
                Unit? unit = context.Units.FirstOrDefault(c => c.Id == unitId);
                if (unit != null)
                {
                    info.Unit = unit;
                    context.SaveChanges();
                }
            }
        }

        public void EditMaterialInfoBySupplier(int id, int supId)
        {
            using var context = new MainContext();
            MaterialInfo? info = context.MaterialInfos.FirstOrDefault(x => x.Id == id);
            if (info != null)
            {
                EquipmentSupplier? sup = context.EquipmentSuppliers.FirstOrDefault(c => c.Id == supId);
                if (sup != null)
                {
                    var art = info.ArtInfos.FirstOrDefault(x => x.IsOriginal == true);
                    if (art != null)
                    {
                        art.Supplier = sup;
                    }
                    else
                    {
                        info.ArtInfos.Add(new ArtInfo
                        {
                            IsOriginal = true,
                            Supplier = sup
                        });
                    }
                    context.SaveChanges();
                }
            }
        }

        public void EditDepartmentByOperator(int id, int unitId)
        {
            using var context = new MainContext();
            Department? dep = context.Departments.FirstOrDefault(x => x.Id == id);
            if (dep != null)
            {
                Operator? op = context.Operators.FirstOrDefault(c => c.Id == unitId);
                if (dep != null)
                {
                    dep.Operator = op;
                    context.SaveChanges();
                }
            }
        }

        public void EditErrorWorking(int id, bool isWorking)
        {
            using var context = new MainContext();
            MaintenanceError error = context.MaintenanceErrors.FirstOrDefault(x => x.Id == id);
            if (error != null)
            {
                error.IsWorking = isWorking;
                context.SaveChanges();
            }
        }

        public void EditInstructionPath(int id, string path)
        {
            using var context = new MainContext();
            Instruction ins = context.Instructions.FirstOrDefault(x => x.Id == id);
            if (ins != null)
            {
                ins.Path = path;
                context.SaveChanges();
            }
        }

        public void EditAdditionalByType(int id, int maintenanceTypeId)
        {
            using var context = new MainContext();
            AdditionalWork work = context.AdditionalWorks.FirstOrDefault(x => x.Id == id);
            MaintenanceType type = context.MaintenanceTypes.FirstOrDefault(c => c.Id == maintenanceTypeId);
            work.MaintenanceType = type;
            context.SaveChanges();
        }

        public void EditAdditionalByOperators(int id, List<int> operatorIds)
        {
            using var context = new MainContext();
            if (operatorIds != null)
            {
                AdditionalWork work = context.AdditionalWorks.FirstOrDefault(x => x.Id == id);
                var operators = context.Operators.Where(c => operatorIds.Contains(c.Id)).ToList();
                work.Operators = operators;
                context.SaveChanges();
            }
            //повторяющиеся ключи?
        }

        public void EditMaterialByInfo(int id, int infoId)
        {
            using var context = new MainContext();
            Material m = context.Materials.FirstOrDefault(x => x.Id == id);
            var info = context.MaterialInfos.FirstOrDefault(c => c.Id == infoId);
            m.MaterialInfo = info;
            context.SaveChanges();
        }

        public void EditMaterialByArts(int id, List<int> artIds)
        {
            using var context = new MainContext();
            var info = context.MaterialInfos.FirstOrDefault(c => c.Id == id);
            var originalArt = context.ArtInfos.FirstOrDefault(x => x.Material!.Id == id);
            if (info != null)
            {
                if (info.ArtInfos == null)
                {
                    info.ArtInfos = new List<ArtInfo>();
                }
                var arts = context.ArtInfos.Where(a => artIds.Contains(a.Id)).ToList();
                if (originalArt != null)
                {
                    arts.Add(originalArt);
                }

                if (arts != null && arts.Count > 0)
                {
                    info.ArtInfos = arts;
                }
                context.SaveChanges();
            }
        }

        public void EditAdditionalByMaterials(int id, List<int> materialsIds)
        {
            using var context = new MainContext();
            if (materialsIds != null)
            {
                AdditionalWork work = context.AdditionalWorks.FirstOrDefault(x => x.Id == id);
                var materials = context.Materials.Where(c => materialsIds.Contains(c.Id)).ToList();
                work.Materials = materials;
                context.SaveChanges();
            }
        }

        public void EditMaintenanceByMaterials(int id, List<int> materialsIds)
        {
            using var context = new MainContext();
            if (materialsIds != null)
            {
                MaintenanceInfo work = context.MaintenanceInfos.FirstOrDefault(x => x.Id == id);
                var materials = context.Materials.Where(c => materialsIds.Contains(c.Id)).ToList();
                work.Materials = materials;
                context.SaveChanges();
            }
        }

        public void EditErorByRepairings(int id, List<int> repairingIds)
        {
            using var context = new MainContext();
            if (repairingIds != null)
            {
                MaintenanceError error = context.MaintenanceErrors.FirstOrDefault(x => x.Id == id);
                var reps = context.Repairings.Where(c => repairingIds.Contains(c.Id)).ToList();
                error.Repairings = reps;
                context.SaveChanges();
            }
        }
        public Characteristic GetCharacteristic(int id)
        {
            using var context = new MainContext();
            return context.Characteristics
                .Include(e => e.TechPassport)
                .Include(u => u.Unit)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Characteristic> GetCharacteristics()
        {
            using var context = new MainContext();
            return context.Characteristics
                .Include(e => e.TechPassport)
                .Include(u => u.Unit)
                .ToList();
        }

        public int AddTechPassport(string name, string serial, string inventory, string departmentNumber)
        {
            using var context = new MainContext();
            TechPassport techPassport = new TechPassport();
            techPassport.Name = name;
            techPassport.SerialNumber = serial;
            techPassport.InventoryNumber = inventory;
            Department dep = context.Departments.FirstOrDefault(x => x.Number == departmentNumber);
            if (dep != null)
            {
                techPassport.Department = dep;
            }

            context.TechPassports.Add(techPassport);
            context.SaveChanges();
            return techPassport.Id;
        }

        public void EditTechPassport(int id, string name, string serial, string inventory, string departmentNumber)
        {
            using var context = new MainContext();
            TechPassport techPassport = context.TechPassports.FirstOrDefault(x => x.Id == id);
            if (techPassport != null)
            {
                techPassport.Name = name;
                techPassport.SerialNumber = serial;
                techPassport.InventoryNumber = inventory;
                if (context.Departments != null && context.Departments.Count() > 0)
                {
                    Department dep = context.Departments.First(x => x.Number == departmentNumber);
                    if (dep != null)
                    {
                        techPassport.Department = dep;
                    }
                }
                context.SaveChanges();
            }
        }

        public int AddTechPassport(TechPassport passport)
        {
            using var context = new MainContext();
            context.TechPassports.Add(passport);
            context.SaveChanges();
            return passport.Id;
        }

        public void EditTechPassport(TechPassport passport)
        {
            using var context = new MainContext();
            TechPassport techPassport = context.TechPassports.FirstOrDefault(x => x.Id == passport.Id);
            if (techPassport != null)
            {
                techPassport = passport;
                context.SaveChanges();
            }
        }

        public int AddTechPassportBaseInfo(TechPassport passport)
        {
            using var context = new MainContext();
            TechPassport techPassport = new TechPassport();
            techPassport.Name = passport.Name;
            techPassport.Version = passport.Version;
            techPassport.SerialNumber = passport.SerialNumber;
            techPassport.InventoryNumber = passport.InventoryNumber;
            techPassport.ReleaseYear = passport.ReleaseYear;
            techPassport.CommissioningDate = passport.CommissioningDate;
            techPassport.DecommissioningDate = passport.DecommissioningDate;
            techPassport.GuaranteeEndDate = passport.GuaranteeEndDate;
            techPassport.Power = passport.Power;
            if (passport.Supplier != null)
            {
                techPassport.Supplier = passport.Supplier;
            }
            if (passport.Type != null)
            {
                techPassport.Type = passport.Type;
            }
            if (passport.Department != null)
            {
                techPassport.Department = passport.Department;
            }
            if (passport.ElectroPoint != null)
            {
                techPassport.ElectroPoint = passport.ElectroPoint;
            }
            if (passport.Operator != null)
            {
                techPassport.Operator = passport.Operator;
            }
            context.TechPassports.Add(techPassport);
            context.SaveChanges();
            return techPassport.Id;
        }

        public void EditTechPassportBaseInfo(TechPassport passport)
        {
            using var context = new MainContext();
            TechPassport techPassport = context.TechPassports.FirstOrDefault(x => x.Id == passport.Id);
            if (techPassport != null)
            {
                techPassport.Name = passport.Name;
                techPassport.Version = passport.Version;
                techPassport.SerialNumber = passport.SerialNumber;
                techPassport.InventoryNumber = passport.InventoryNumber;
                techPassport.ReleaseYear = passport.ReleaseYear;
                techPassport.CommissioningDate = passport.CommissioningDate;
                techPassport.DecommissioningDate = passport.DecommissioningDate;
                techPassport.GuaranteeEndDate = passport.GuaranteeEndDate;
                techPassport.Power = passport.Power;
                if (passport.Supplier != null)
                {
                    techPassport.Supplier = passport.Supplier;
                }
                if (passport.Type != null)
                {
                    techPassport.Type = passport.Type;
                }
                if (passport.Department != null)
                {
                    techPassport.Department = passport.Department;
                }
                if (passport.ElectroPoint != null)
                {
                    techPassport.ElectroPoint = passport.ElectroPoint;
                }
                if (passport.Operator != null)
                {
                    techPassport.Operator = passport.Operator;
                }
                context.SaveChanges();
            }
        }

        public int AddOperator(string name, string position)
        {
            using var context = new MainContext();
            Operator oper = new Operator();
            oper.Name = name;
            oper.Position = position;
            context.Operators.Add(oper);
            context.SaveChanges();
            return oper.Id;
        }

        public void EditOperator(int id, string name, string position)
        {
            using var context = new MainContext();
            var oper = context.Operators.FirstOrDefault(o => o.Id == id);
            if (oper != null)
            {
                oper.Name = name;
                oper.Position = position;
                context.SaveChanges();
            }
        }

        public int AddPoint(string name, string description)
        {
            using var context = new MainContext();
            ElectroPoint p = new ElectroPoint();
            p.Name = name;
            p.Description = description;
            context.Points.Add(p);
            context.SaveChanges();
            return p.Id;
        }

        public void EditPoint(int id, string name, string description)
        {
            using var context = new MainContext();
            var p = context.Points.FirstOrDefault(o => o.Id == id);
            if (p != null)
            {
                p.Name = name;
                p.Description = description;
                context.SaveChanges();
            }
        }

        public int AddType(string name)
        {
            using var context = new MainContext();
            var type = new EquipmentType
            {
                Type = name
            };
            context.EquipmentTypes.Add(type);
            context.SaveChanges();

            return type.Id;
        }

        public void EditType(int id, string name)
        {
            using var context = new MainContext();
            var type = context.EquipmentTypes.FirstOrDefault(x => x.Id == id);
            if (type != null)
            {
                type.Type = name;
                context.SaveChanges();
            }
        }

        public int AddMaintenanceType(string name, string description)
        {
            using var context = new MainContext();
            var type = new MaintenanceType
            {
                Type = name,
                Description = description
            };
            context.MaintenanceTypes.Add(type);
            context.SaveChanges();

            return type.Id;
        }

        public void EditMaintenanceType(int id, string name, string description)
        {
            using var context = new MainContext();
            var type = context.MaintenanceTypes.FirstOrDefault(x => x.Id == id);
            if (type != null)
            {
                type.Type = name;
                type.Description = description;
                context.SaveChanges();
            }
        }

        public int AddUnit(string shortname, string fullname)
        {
            using var context = new MainContext();
            var unit = new Unit
            {
                Name = shortname,
                FullName = fullname
            };
            context.Units.Add(unit);
            context.SaveChanges();
            return unit.Id;
        }

        public void EditUnit(int id, string shortname, string fullname)
        {
            using var context = new MainContext();
            var unit = context.Units.FirstOrDefault(x => x.Id == id);
            if (unit != null)
            {
                unit.Name = shortname;
                unit.FullName = fullname;
                context.SaveChanges();
            }
        }

        public int AddDepartment(string number, string name)
        {
            using var context = new MainContext();
            var department = new Department
            {
                Name = name,
                Number = number
            };
            context.Departments.Add(department);
            context.SaveChanges();
            return department.Id;
        }

        public void EditDepartment(int id, string number, string name)
        {
            using var context = new MainContext();
            var department = context.Departments.FirstOrDefault(x => x.Id == id);
            if (department != null)
            {
                department.Name = name;
                department.Number = number;
                context.SaveChanges();
            }
        }
        public int AddSupplier(string name, string address, string phone, string addphone, string email, string person, string comment)
        {
            using var context = new MainContext();
            var supplier = new EquipmentSupplier
            {
                Name = name,
                Address = address,
                PhoneNumber = phone,
                AdditionalPhoneNumber = addphone,
                Email = email,
                Person = person,
                Commentary = comment
            };
            context.EquipmentSuppliers.Add(supplier);
            context.SaveChanges();

            return supplier.Id;
        }

        public void EditSupplier(int id, string name, string address, string phone, string addphone, string email, string person, string comment)
        {
            using var context = new MainContext();
            var supplier = context.EquipmentSuppliers.FirstOrDefault(x => x.Id == id);
            if (supplier != null)
            {
                supplier.Name = name;
                supplier.Address = address;
                supplier.PhoneNumber = phone;
                supplier.AdditionalPhoneNumber = addphone;
                supplier.Email = email;
                supplier.Person = person;
                supplier.Commentary = comment;
                context.SaveChanges();
            }
        }


        public int AddMaintanance(int passportId, string name, int type, bool isFixed, double interval, double hours, DateTime? date, bool isInWork)
        {
            using var context = new MainContext();
            var maintenance = new MaintenanceInfo();
            maintenance = MakeMaintanance(maintenance, name, type, isFixed, interval, hours, date, isInWork);
            maintenance.TechPassportId = passportId;
            context.MaintenanceInfos.Add(maintenance);
            context.SaveChanges();
            return maintenance.Id;
        }

        public void ChangeAdditionalInfo(int id, DateTime date, List<Operator> operators)
        {
            using var context = new MainContext();
            var add = context.AdditionalWorks.FirstOrDefault(x => x.Id == id);
            if (add != null)
            {
                add.PlannedDate = date;
                add.Operators = operators;
                context.SaveChanges();
            }
        }

        public void ChangeFactDate(int id, DateTime date, double hoursOnWork, List<Operator> workers)
        {
            using var context = new MainContext();
            var work = context.AdditionalWorks.FirstOrDefault(x => x.Id == id);
            if (work != null)
            {
                work.DateFact = date;
                work.HoursFact = hoursOnWork;
                work.Operators = workers;
                work.PlannedDate = null;
                context.SaveChanges();
            }
        }

        public void EditMaintanance(int passportId, int id, string name, int type, bool isFixed, double interval, double hours, DateTime? date, bool isInWork)
        {
            using var context = new MainContext();
            var maintenance = context.MaintenanceInfos.FirstOrDefault(x => x.Id == id);
            if (maintenance != null)
            {
                maintenance = MakeMaintanance(maintenance, name, type, isFixed, interval, hours, date, isInWork);
                maintenance.TechPassportId = passportId;
                context.SaveChanges();
            }
        }

        public MaintenanceEpisode? GetMaintananceEpisode(int id)
        {
            using var context = new MainContext();
            return context.MaintenanceEpisodes.Include(m => m.Operators).FirstOrDefault(x => x.Id == id);
        }

        public List<MaintenanceEpisode> GetMaintananceEpisodes()
        {
            using var context = new MainContext();
            return context.MaintenanceEpisodes
                .Include(m => m.Operators)
                .Include(e => e.Info).ThenInclude(i=>i.MaintenanceType)
                .Include(e => e.Info).ThenInclude(i => i.TechPassport).ToList();
        }

        public void AddMaintananceEpisode(int maintenanceId, DateTime Date, double HoursOnWork, List<Operator> Operators)
        {
            using var context = new MainContext();
            var maintananceEpisode = new MaintenanceEpisode();
            var maintenanceInfo = context.MaintenanceInfos.FirstOrDefault(x => x.Id == maintenanceId);
            if (maintenanceInfo != null)
            {
                maintananceEpisode.Info = maintenanceInfo;
                maintananceEpisode.Date = Date;
                maintananceEpisode.Hours = HoursOnWork;
                maintananceEpisode.Operators = Operators;
                maintananceEpisode.IsDone = true;
                context.MaintenanceEpisodes.Add(maintananceEpisode);

                if (maintenanceInfo.Episodes == null)
                {
                    maintenanceInfo.Episodes = new List<MaintenanceEpisode>();
                }
                maintenanceInfo.Episodes.Add(maintananceEpisode);
                context.SaveChanges();
            }
        }

        public MaintenanceEpisode AddUndoneEpisode(int maintenanceId, DateTime date, List<Operator> operators, DateTime oldDate)
        {
            using var context = new MainContext();
            var maintananceEpisode = new MaintenanceEpisode();
            var maintenanceInfo = context.MaintenanceInfos.FirstOrDefault(x => x.Id == maintenanceId);
            if (maintenanceInfo != null)
            {
                maintananceEpisode.Info = maintenanceInfo;
                maintananceEpisode.Date = date;
                maintananceEpisode.Operators = operators;
                maintananceEpisode.IsDone = false;
                context.MaintenanceEpisodes.Add(maintananceEpisode);

                if (maintenanceInfo.Episodes == null)
                {
                    maintenanceInfo.Episodes = new List<MaintenanceEpisode>();
                }
                TimeSpan delta = date - oldDate;
                if (delta != TimeSpan.Zero)
                {
                    var eps = context.MaintenanceEpisodes.Where(a => a.Info.Id == maintananceEpisode.Info.Id && a.Date.Date > oldDate);
                    foreach (var e in eps)
                    {
                        if (e.IsDone == null || !(bool)e.IsDone)
                        {
                            e.Date += delta;
                        }
                    }
                }
                maintenanceInfo.Episodes.Add(maintananceEpisode);
                context.SaveChanges();
            }
            return maintananceEpisode;
        }

        public List<MaintenanceEpisode> AddUndoneEpisodes(int[] maintenanceId, DateTime[] date, List<Operator> operators, DateTime[] oldDate)
        {
            using var context = new MainContext();
            var maintananceEpisodes = new List<MaintenanceEpisode>();
            for (int i = 0; i < maintenanceId.Length; i++)
            {
                var maintenanceInfo = context.MaintenanceInfos.FirstOrDefault(x => x.Id == maintenanceId[i]);
                if (maintenanceInfo != null)
                {
                    var maintananceEpisode = new MaintenanceEpisode
                    {
                        Info = maintenanceInfo,
                        Date = date[i],
                        Operators = operators,
                        IsDone = false
                    };
                    context.MaintenanceEpisodes.Add(maintananceEpisode);

                    maintenanceInfo.Episodes ??= new List<MaintenanceEpisode>();
                    TimeSpan delta = date[i] - oldDate[i];
                    if (delta != TimeSpan.Zero)
                    {
                        var eps = context.MaintenanceEpisodes.Where(a => a.Info.Id == maintananceEpisode.Info.Id && a.Date.Date > oldDate[i]);
                        foreach (var e in eps)
                        {
                            if (e.IsDone == null || !(bool)e.IsDone)
                            {
                                e.Date += delta;
                            }
                        }
                    }
                    maintenanceInfo.Episodes.Add(maintananceEpisode);
                    maintananceEpisodes.Add(maintananceEpisode);
                }
            }
            context.SaveChanges();
            return maintananceEpisodes;
        }

        public void SaveEmptyEpisode(int maintenanceId, DateTime date)
        {
            using var context = new MainContext();
            var maintananceEpisode = new MaintenanceEpisode();
            var maintenanceInfo = context.MaintenanceInfos.FirstOrDefault(x => x.Id == maintenanceId);
            if (maintenanceInfo != null)
            {
                maintananceEpisode.Info = maintenanceInfo;
                maintananceEpisode.Date = date;
                context.MaintenanceEpisodes.Add(maintananceEpisode);
                context.SaveChanges();
            }
        }

        public void MakeMaintananceEpisodeDone(int episodeId, DateTime date, double hoursOnWork, List<Operator> operators)
        {
            using var context = new MainContext();
            var maintenanceEpisode = context.MaintenanceEpisodes.FirstOrDefault(x => x.Id == episodeId);
            if (maintenanceEpisode != null)
            {
                maintenanceEpisode.Date = date;
                maintenanceEpisode.Hours = hoursOnWork;
                maintenanceEpisode.Operators = operators;
                maintenanceEpisode.IsDone = true;
                context.SaveChanges();
            }
        }

        public void ChangeEpisodeInfo(int episodeId, DateTime date, List<Operator> operators)
        {
            using var context = new MainContext();
            var maintenanceEpisode = context.MaintenanceEpisodes.FirstOrDefault(x => x.Id == episodeId);
            if (maintenanceEpisode != null)
            {
                var oldDate = maintenanceEpisode.Date;

                maintenanceEpisode.Date = date;
                maintenanceEpisode.Operators = operators;
                maintenanceEpisode.IsDone = false;

                TimeSpan delta = date - oldDate;
                if (delta != TimeSpan.Zero)
                {
                    var eps = context.MaintenanceEpisodes.Where(a => a.Info.Id == maintenanceEpisode.Info.Id && a.Date.Date > oldDate);
                    foreach (var e in eps)
                    {
                        if (e.IsDone == null || !(bool)e.IsDone)
                        {
                            e.Date += delta;
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        public void AddMaintananceEpisode(int maintenanceId, DateTime Date, double HoursOnWork, List<int> OperatorIds, bool IsDone)
        {
            using var context = new MainContext();
            var maintananceEpisode = new MaintenanceEpisode();
            var maintenanceInfo = context.MaintenanceInfos.FirstOrDefault(x => x.Id == maintenanceId);
            if (maintenanceInfo != null)
            {
                maintananceEpisode.Info = maintenanceInfo;
                maintananceEpisode.Date = Date;
                maintananceEpisode.Hours = HoursOnWork;
                maintananceEpisode.IsDone = IsDone;
                var operators = context.Operators.Where(x => OperatorIds.Contains(x.Id)).ToList();
                if (operators != null)
                {
                    maintananceEpisode.Operators = operators;
                }
                context.MaintenanceEpisodes.Add(maintananceEpisode);

                if (maintenanceInfo.Episodes == null)
                {
                    maintenanceInfo.Episodes = new List<MaintenanceEpisode>();
                }
                maintenanceInfo.Episodes.Add(maintananceEpisode);
                context.SaveChanges();
            }
        }

        public void EditMaintananceEpisode(int id, DateTime Date, double HoursOnWork, List<int> OperatorIds, bool IsDone)
        {
            using var context = new MainContext();
            var maintananceEpisode = context.MaintenanceEpisodes.FirstOrDefault(x => x.Id == id);
            if (maintananceEpisode != null)
            {
                maintananceEpisode.Date = Date;
                maintananceEpisode.Hours = HoursOnWork;
                maintananceEpisode.IsDone = IsDone;
                var operators = context.Operators.Where(x => OperatorIds.Contains(x.Id)).ToList();
                if (operators != null)
                {
                    maintananceEpisode.Operators = operators;
                }
                context.SaveChanges();
            }
        }

        public List<MaintenanceEpisode> GetEpisodesByInfoId(int id)
        {
            using var context = new MainContext();
            return context.MaintenanceEpisodes.Include(s => s.Operators).Where(x => x.Info.Id == id).ToList();
        }

        private MaintenanceInfo MakeMaintanance(MaintenanceInfo maintenance, string name, int typeId, bool isFixed, double interval, double hours, DateTime? date, bool isInWork)
        {
            using var context = new MainContext();
            maintenance.MaintenanceName = name;
            maintenance.MaintenanceType = context.MaintenanceTypes.FirstOrDefault(x => x.Id == typeId);
            maintenance.IsIntervalFixed = isFixed;
            maintenance.IntervalTime = interval;
            maintenance.Hours = hours;
            maintenance.IsInWork = isInWork;

            return maintenance;
        }

        public List<Operator> GetOperators(List<int> ids)
        {
            using var context = new MainContext();
            return context.Operators.Include(o => o.MaintananceEpisodes).Include(s => s.Repairings).Where(x => ids.Contains(x.Id)).ToList();
        }

        public List<Material> GetMaterials()
        {
            using var context = new MainContext();
            return context.Materials.Include(s => s.MaterialInfo).ToList();
        }

        public List<Repairing> GetRepairings()
        {
            using var context = new MainContext();
            return context.Repairings.Include(s => s.Error).ToList();
        }

        public int AddMaterialInfo(string name, string inner, string original, List<int> arts, string comment, int supId, int? unitId)
        {
            using var context = new MainContext();
            var materialInfo = new MaterialInfo
            {
                Name = name ?? "",
                InnerArt = inner ?? "",
                Commentary = comment ?? "",
                ArtInfos = context.ArtInfos.Where(x => arts.Contains(x.Id)).ToList(),
                Unit = context.Units.FirstOrDefault(u => u.Id == unitId)
            };
            context.MaterialInfos.Add(materialInfo);

            var artInfo = new ArtInfo
            {
                Art = original != null ? original : ""
            };
            var supplier = context.EquipmentSuppliers.FirstOrDefault(x => x.Id == supId);
            if (supplier != null)
            {
                artInfo.Supplier = supplier;
            }
            artInfo.Material = materialInfo;
            artInfo.IsOriginal = true;
            context.ArtInfos.Add(artInfo);

            context.SaveChanges();

            return materialInfo.Id;
        }

        public bool DeletePassport(int id)
        {
            try
            {
                using var context = new MainContext();
                var passport = GetPassportById(id);
                if (passport != null)
                {
                    if ((passport.MaintenanceInfos == null || passport.MaintenanceInfos.Count == 0) &&
                        (passport.Errors == null || passport.Errors.Count == 0) &&
                        (passport.Characteristics == null || passport.Characteristics.Count == 0) &&
                        (passport.Instructions == null || passport.Instructions.Count == 0) &&
                        (passport.WorkingHours == null || passport.WorkingHours.Count == 0) &&
                        (passport.ControledParametrs == null || passport.ControledParametrs.Count == 0) &&
                        (passport.AdditionalWorks == null || passport.AdditionalWorks.Count == 0) &&
                        passport.Supplier == null &&
                        passport.Type == null)
                    {
                        context.TechPassports.Remove(passport);
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteMaterialInfo(int id)
        {
            try
            {
                using var context = new MainContext();
                var materialInfo = context.MaterialInfos.Include(y => y.MaterialInUse).FirstOrDefault(x => x.Id == id);
                if (materialInfo != null)
                {
                    if (materialInfo.MaterialInUse == null || materialInfo.MaterialInUse.Count == 0)
                    {
                        context.MaterialInfos.Remove(materialInfo);
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePoints(int id)
        {
            try
            {
                using var context = new MainContext();
                var point = context.Points.Include(y => y.TechPassports).FirstOrDefault(x => x.Id == id);
                if (point != null)
                {
                    if (point.TechPassports == null || point.TechPassports.Count == 0)
                    {
                        context.Points.Remove(point);
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUnit(int id)
        {
            try
            {
                using var context = new MainContext();
                var unit = context.Units.FirstOrDefault(x => x.Id == id);
                if (unit != null)
                {
                    var pars = context.ControledParametrs.Where(t => t.Unit != null && t.Unit.Id == id);
                    var materials = context.MaterialInfos.Where(t => t.Unit != null && t.Unit.Id == id);
                    if ((pars == null || pars.ToList().Count == 0) &&
                        (materials == null || materials.ToList().Count == 0))
                    {
                        context.Units.Remove(unit);
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteDepartment(int id)
        {
            try
            {
                using var context = new MainContext();
                var dep = context.Departments.FirstOrDefault(x => x.Id == id);
                if (dep != null)
                {
                    var passports = context.TechPassports.Where(t => t.Department != null && t.Department.Id == dep.Id);
                    if (passports == null || passports.ToList().Count == 0)
                    {
                        context.Departments.Remove(dep);
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSupplier(int id)
        {
            try
            {
                using var context = new MainContext();
                var sup = context.EquipmentSuppliers.Include(x => x.TechPassports).FirstOrDefault(x => x.Id == id);
                if (sup != null)
                {
                    var materials = context.MaterialInfos.Where(t => t.ArtInfos != null && t.ArtInfos.Any(x => x.Supplier != null && x.Supplier.Id == id));
                    if ((materials == null || materials.ToList().Count == 0) &&
                        (sup.TechPassports == null || sup.TechPassports.ToList().Count == 0))
                    {
                        context.EquipmentSuppliers.Remove(sup);
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteEType(int id)
        {
            try
            {
                using var context = new MainContext();
                var type = context.EquipmentTypes.Include(y => y.TechPassports).FirstOrDefault(x => x.Id == id);
                if (type != null)
                {
                    if (type.TechPassports == null || type.TechPassports.Count == 0)
                    {
                        context.EquipmentTypes.Remove(type);
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteMType(int id)
        {
            try
            {
                using var context = new MainContext();
                var type = context.MaintenanceTypes.Include(y => y.MaintenanceInfos)
                    .Include(x => x.AdditionalWorks)
                    .FirstOrDefault(x => x.Id == id);
                if (type != null)
                {
                    if ((type.MaintenanceInfos == null || type.MaintenanceInfos.Count == 0) &&
                        (type.AdditionalWorks == null || type.AdditionalWorks.Count == 0))
                    {
                        context.MaintenanceTypes.Remove(type);
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOperator(int id)
        {
            try
            {
                using var context = new MainContext();
                var op = context.Operators.Include(y => y.MaintananceEpisodes)
                    .Include(z => z.AdditionalWorks)
                    .Include(z => z.Repairings)
                    .FirstOrDefault(x => x.Id == id);
                if (op != null)
                {
                    if ((op.MaintananceEpisodes == null || op.MaintananceEpisodes.Count == 0) &&
                        (op.AdditionalWorks == null || op.AdditionalWorks.Count == 0) &&
                        (op.Repairings == null || op.Repairings.Count == 0))
                    {
                        var passports = context.TechPassports.Where(t => t.Operator != null && t.Operator.Id == op.Id);
                        if (passports == null || passports.ToList().Count == 0)
                        {
                            context.Operators.Remove(op);
                            context.SaveChanges();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public void EditMaterialInfo(int id, string name, string inner, string original, string comment)
        {
            using var context = new MainContext();
            var materialInfo = context.MaterialInfos.FirstOrDefault(x => x.Id == id);
            if (materialInfo != null)
            {
                materialInfo.Name = name;
                materialInfo.InnerArt = inner;
                materialInfo.Commentary = comment;

                if (materialInfo.ArtInfos != null)
                {
                    var art = materialInfo.ArtInfos.FirstOrDefault(x => x.IsOriginal);
                    if (art != null)
                    {
                        art.Art = original;
                    }
                    else
                    {
                        art = new ArtInfo
                        {
                            Art = original,
                            IsOriginal = true
                        };
                        materialInfo.ArtInfos.Add(art);
                        context.ArtInfos.Add(art);
                    }
                }
                else
                {
                    materialInfo.ArtInfos = new List<ArtInfo>();
                    var art = new ArtInfo
                    {
                        Art = original,
                        IsOriginal = true
                    };
                    materialInfo.ArtInfos.Add(art);
                    context.ArtInfos.Add(art);
                }
                context.SaveChanges();
            }
        }

        public List<ArtInfo> GetArtInfos()
        {
            using var context = new MainContext();
            return context.ArtInfos.Include(x => x.Supplier).ToList();
        }

        public int AddArtInfo(int materialId, string art, int supId)
        {
            using var context = new MainContext();
            var materialInfo = context.MaterialInfos.FirstOrDefault(x => x.Id == materialId);
            var artInfo = new ArtInfo
            {
                Art = art
            };
            var supplier = context.EquipmentSuppliers.FirstOrDefault(x => x.Id == supId);
            artInfo.Supplier = supplier;
            artInfo.Material = materialInfo;
            artInfo.IsOriginal = false;
            context.ArtInfos.Add(artInfo);
            context.SaveChanges();

            return artInfo.Id;
        }

        public void EditArtInfo(int id, string art, int supId)
        {
            using var context = new MainContext();
            var info = context.ArtInfos.FirstOrDefault(x => x.Id == id);
            if (info != null)
            {
                info.Art = art;
                var sup = context.EquipmentSuppliers.FirstOrDefault(x => x.Id == supId);
                if (sup != null)
                {
                    info.Supplier = sup;
                }
                context.SaveChanges();
            }
        }

        public int AddRepairing(int errorId, DateTime date, string comment)
        {
            using var context = new MainContext();
            var error = context.MaintenanceErrors.FirstOrDefault(x => x.Id == errorId);
            var repairing = new Repairing
            {
                Date = date,
                Comment = comment
            };
            context.Repairings.Add(repairing);
            context.SaveChanges();

            return repairing.Id;
        }

        public void EditRepairing(int id, DateTime date, string comment)
        {
            using var context = new MainContext();
            var repairing = context.Repairings.FirstOrDefault(x => x.Id == id);
            if (repairing != null)
            {
                repairing.Date = date;
                repairing.Comment = comment;
                context.SaveChanges();
            }
        }

        public void EditArtBySupplier(int id, int supId)
        {
            using var context = new MainContext();
            var info = context.ArtInfos.FirstOrDefault(x => x.Id == id);
            if (info != null)
            {
                var sup = context.EquipmentSuppliers.FirstOrDefault(x => x.Id == supId);
                if (sup != null)
                {
                    info.Supplier = sup;
                }
                context.SaveChanges();
            }
        }

        public int AddMaterial(int infoId, double count, int maintenanceId, bool isAdditional)
        {
            using var context = new MainContext();
            if (infoId == 0)
            {
                return 0;
            }
            var materialInfo = context.MaterialInfos.FirstOrDefault(x => x.Id == infoId);
            var material = new Material();
            if (materialInfo != null)
            {
                material.MaterialInfo = materialInfo;
                if (isAdditional)
                {
                    var additionalWork = context.AdditionalWorks.FirstOrDefault(m => m.Id == maintenanceId);
                    if (additionalWork != null)
                    {
                        material.AdditionalWork = additionalWork;
                    }
                }
                else
                {
                    var maintenanceInfo = context.MaintenanceInfos.FirstOrDefault(m => m.Id == maintenanceId);
                    if (maintenanceInfo != null)
                    {
                        material.MaintenanceInfo = maintenanceInfo;
                    }
                }
                material.Count = count;
                context.Materials.Add(material);
                context.SaveChanges();
                return material.Id;
            }
            else
            {
                return 0;
            }
        }

        public void EditMaterial(int id, int infoId, double count, int maintenanceId)
        {
            using var context = new MainContext();
            var materialInfo = context.MaterialInfos.FirstOrDefault(x => x.Id == infoId);
            var maintenanceInfo = context.MaintenanceInfos.FirstOrDefault(m => m.Id == maintenanceId);
            var material = context.Materials.FirstOrDefault(x => x.Id == id);
            if (material != null)
            {
                if (materialInfo != null)
                {
                    material.MaterialInfo = materialInfo;
                }
                if (maintenanceInfo != null)
                {
                    material.MaintenanceInfo = maintenanceInfo;
                }
                material.Count = count;
                context.SaveChanges();
            }
        }

        public void EditMaterialForAdditional(int id, int infoId, double count, int additionalId)
        {
            using var context = new MainContext();
            var material = context.Materials.FirstOrDefault(x => x.Id == id);
            if (material != null)
            {
                var materialInfo = context.MaterialInfos.FirstOrDefault(x => x.Id == infoId);
                var work = context.AdditionalWorks.FirstOrDefault(m => m.Id == additionalId);

                if (materialInfo != null)
                {
                    material.MaterialInfo = materialInfo;
                }
                if (work != null)
                {
                    material.AdditionalWork = work;
                }
                material.Count = count;
                context.SaveChanges();
            }
        }

        public int AddControledParam(int passportId, string name, double nominal, int unitId)
        {
            using var context = new MainContext();
            var controledParametr = new ControledParametr
            {
                Name = name,
                Nominal = nominal,
                TechPassportId = passportId
            };
            var unit = context.Units.FirstOrDefault(x => x.Id == unitId);
            if (unit != null)
            {
                controledParametr.Unit = unit;
            }
            context.ControledParametrs.Add(controledParametr);
            context.SaveChanges();
            return controledParametr.Id;
        }

        public void EditControledParam(int passportId, int id, string name, double nominal, int unitId)
        {
            using var context = new MainContext();
            var controledParametr = context.ControledParametrs.FirstOrDefault(x => x.Id == id);
            if (controledParametr != null)
            {
                controledParametr.Name = name;
                controledParametr.Nominal = nominal;
                controledParametr.TechPassportId = passportId;

                var unit = context.Units.FirstOrDefault(x => x.Id == unitId);
                if (unit != null)
                {
                    controledParametr.Unit = unit;
                }
                context.SaveChanges();
            }
        }

        public int EditControledParamEpisode(int id, DateTime date, double count, int paramId)
        {
            using var context = new MainContext();
            var controledParametr = context.ControledParametrs.FirstOrDefault(x => x.Id == paramId);
            if (controledParametr != null)
            {
                var controledParametrDI = context.ControledParametrDateInfos.FirstOrDefault(x => x.Id == id);

                if (controledParametrDI != null)
                {
                    controledParametrDI.Date = date;
                    controledParametrDI.Count = count;
                    controledParametrDI.ControledParametr = controledParametr;
                }
                else
                {
                    controledParametrDI = new ControledParametrDateInfo();
                    controledParametrDI.Date = date;
                    controledParametrDI.Count = count;
                    controledParametrDI.ControledParametr = controledParametr;

                    context.ControledParametrDateInfos.Add(controledParametrDI);
                }
                context.SaveChanges();

                return controledParametrDI.Id;
            }
            return 0;
        }

        public int AddHours(int passportId, int hours, DateTime date)
        {
            using var context = new MainContext();
            var hoursInfo = new HoursInfo
            {
                Hours = hours,
                Date = date,
                TechPassportId = passportId
            };
            context.Add(hoursInfo);
            context.SaveChanges();
            return hoursInfo.Id;
        }

        public void EditHours(int passportId, int id, int hours, DateTime date)
        {
            using var context = new MainContext();
            var hoursInfo = context.WorkingHours.FirstOrDefault(x => x.Id == id);
            if (hoursInfo != null)
            {
                hoursInfo.Hours = hours;
                hoursInfo.Date = date;
                hoursInfo.TechPassportId = passportId;
                context.SaveChanges();
            }
        }

        public int AddAdditionalWork(int passportId, string name, DateTime planedDate, DateTime? dateFact, string comment, double hours, double hoursFact)
        {
            using var context = new MainContext();
            var work = new AdditionalWork
            {
                Name = name,
                DateFact = dateFact,
                PlannedDate = planedDate,
                Commentary = comment,
                Hours = hours,
                HoursFact = hoursFact,
                TechPassportId = passportId
            };

            context.Add(work);
            context.SaveChanges();
            return work.Id;
        }

        public void EditAdditionalWork(int passportId, int id, string name, DateTime planedDate, DateTime? dateFact, string comment, double hours, double hoursFact)
        {
            using var context = new MainContext();
            var work = context.AdditionalWorks.FirstOrDefault(x => x.Id == id);
            if (work != null)
            {
                work.Name = name;
                work.DateFact = dateFact;
                work.PlannedDate = planedDate;
                work.Commentary = comment;
                work.Hours = hours;
                work.HoursFact = hoursFact;
                work.TechPassportId = passportId;
                context.SaveChanges();
            }
        }

        public int AddErrorNew(int passportId, DateTime date, string code, string name, bool isWorking, string description, string comment, DateTime? dateOfSolving, bool? isActive)
        {
            using var context = new MainContext();
            var error = new MaintenanceError
            {
                Date = date,
                Name = name,
                IsWorking = isWorking,
                Comment = comment,
                Description = description,
                DateOfSolving = dateOfSolving,
                Code = code,
                IsActive = isActive,
                TechPassportId = passportId
            };

            if (dateOfSolving.HasValue && dateOfSolving.Value != DateTime.MinValue)
                error.IsWorking = true;

            if (!error.IsWorking.Value && (!isActive.HasValue || isActive.Value))
            {
                var downtime = context.Downtimes.Where(x => x.TechPassport.Id == passportId && x.End == null).FirstOrDefault();
                if (downtime != null)
                {
                    downtime.Start = date;
                }
                else
                {
                    Downtime d = new Downtime() { Start = date, TechPassportId = passportId };
                    context.Add(d);
                }
            }

            context.Add(error);
            context.SaveChanges();

            return error.Id;
        }

        public void EditErrorNew(int passportId, int id, DateTime date, string code, string name, bool isWorking, string description, string comment, DateTime? dateOfSolving, bool? isActive)
        {
            using var context = new MainContext();
            var error = context.MaintenanceErrors.FirstOrDefault(x => x.Id == id);
            if (error != null)
            {
                if ((!isActive.HasValue || isActive.Value) && error.IsWorking != isWorking)
                {
                    if (!error.IsWorking.HasValue || error.IsWorking.HasValue && error.IsWorking.Value)
                    {
                        var downtime = context.Downtimes.Where(x => x.TechPassport.Id == passportId && x.End == null).FirstOrDefault();
                        if (downtime != null)
                        {
                            downtime.Start = date;
                        }
                        else
                        {
                            Downtime d = new Downtime() { Start = date, TechPassportId = passportId };
                            context.Add(d);
                        }
                    }
                    else
                    {
                        error.IsWorking = isWorking;
                        var downtime = context.Downtimes.Where(x => x.TechPassport.Id == passportId && x.End == null).FirstOrDefault();
                        if (downtime != null)
                        {
                            var allWorking = !context.MaintenanceErrors.Any(x => x.TechPassport.Id == passportId && (!x.IsActive.HasValue || x.IsActive.Value) && x.IsWorking.HasValue && !x.IsWorking.Value);
                            if (allWorking)
                            {
                                downtime.End = dateOfSolving != null && dateOfSolving.Value != DateTime.MinValue ?
                                    dateOfSolving : DateTime.Now;
                            }
                        }
                    }
                }

                error.Date = date;
                error.Name = name;
                error.IsWorking = isWorking;
                error.Comment = comment;
                error.Description = description;
                error.DateOfSolving = dateOfSolving;
                error.Code = code;
                error.IsActive = isActive;
                error.TechPassportId = passportId;
            }
            context.SaveChanges();
        }

        public int AddInstrument(int passportId, string art, string name, double? count, int unitId,
            DateTime? createDate, DateTime? removeDate, string removeReason, string commentary)
        {
            using var context = new MainContext();
            var instrument = new Instrument
            {
                Name = name,
                Art = art,
                CreateDate = createDate,
                Count = count,
                Commentary = commentary,
                RemoveDate = removeDate,
                RemoveReason = removeReason,
                TechPassportId = passportId
            };

            var unit = context.Units.FirstOrDefault(x => x.Id == unitId);
            if (unit != null)
            {
                instrument.Unit = unit;
            }
            context.Add(instrument);
            context.SaveChanges();
            return instrument.Id;
        }

        public void EditInstrument(int passportId, int id, string art, string name, double? count, int unitId,
            DateTime? createDate, DateTime? removeDate, string removeReason, string commentary)
        {
            using var context = new MainContext();
            var instrument = context.Instruments.FirstOrDefault(x => x.Id == id);
            if (instrument != null)
            {

                instrument.Name = name;
                instrument.Art = art;
                instrument.CreateDate = createDate;
                instrument.Count = count;
                instrument.Commentary = commentary;
                instrument.RemoveDate = removeDate;
                instrument.RemoveReason = removeReason;
                instrument.TechPassportId = passportId;

                var unit = context.Units.FirstOrDefault(x => x.Id == unitId);
                if (unit != null)
                {
                    instrument.Unit = unit;
                }

                context.SaveChanges();
            }
        }

        public int AddCharacteristic(int passportId, string name, double? count, string commentary)
        {
            using var context = new MainContext();
            var characteristic = new Characteristic
            {
                Name = name,
                Count = count,
                Commentary = commentary,
                TechPassportId = passportId
            };

            context.Add(characteristic);
            context.SaveChanges();
            return characteristic.Id;
        }

        public void EditCharacteristic(int passportId, int id, string name, double? count, string commentary)
        {
            using var context = new MainContext();
            var characteristic = context.Characteristics.FirstOrDefault(x => x.Id == id);
            if (characteristic != null)
            {
                characteristic.Name = name;
                characteristic.Count = count;
                characteristic.Commentary = commentary;
                characteristic.TechPassportId = passportId;
                context.SaveChanges();
            }
        }

        public int AddInstruction(int passportId, string name, string path)
        {
            using var context = new MainContext();
            var instruction = new Instruction
            {
                Name = name,
                Path = path,
                TechPassportId = passportId
            };

            context.Add(instruction);
            context.SaveChanges();
            return instruction.Id;
        }

        public void EditInstruction(int passportId, int id, string name, string path)
        {
            using var context = new MainContext();
            var instruction = context.Instructions.FirstOrDefault(x => x.Id == id);
            if (instruction != null)
            {
                instruction.Name = name;
                instruction.Path = path;
                instruction.TechPassportId = passportId;
                context.SaveChanges();
            }
        }

        public List<MaterialInfo> GetMaterialInfos()
        {
            using var context = new MainContext();
            return context.MaterialInfos.Include(c => c.Unit).Include(a => a.ArtInfos).ToList();
        }

        public List<ElectroPoint> GetPoints()
        {
            using var context = new MainContext();
            return context.Points.ToList();
        }
        string sqlExpression = @"Select SKLN_Cd, max(SklN_Nm) SklN_Nm, sum(EndQt) EndQt, sum(QtRes) QtRes

From(

  Select  SklN_Cd, SklN_Nm, SklGr_Nm, SklStor_Sh StorSh, SklStor_Nm StorNm, BegQt, EndQt, SklKrt_QtRes QtRes

  From

     (

      Select SklKrt_Rcd as CardID, SklOu_Rcd as SklOu,

        Max(isnull(OstBeg.SklOst_Qt, 0)) + Sum(isnull(case when SklDcs_Dat < @DtBeg then CASE WHEN SklDcs_Mov = 0 then - SklDcs_QtOsn else +SklDcs_QtOsn end end, 0)) BegQt,

        Max(isnull(OstEnd.SklOst_Qt, 0)) + Sum(isnull(case when OstEnd.SklOst_Qt is null OR SklDcs_Dat >= OstEnd.SklOst_Dt + 1 then CASE WHEN SklDcs_Mov = 0 then - SklDcs_QtOsn else +SklDcs_QtOsn end end, 0)) EndQt,

        Sum(case when SklDcs_Dat >= @DtBeg AND SklDcs_Dat < @DtEnd AND SklDcs_Mov = 0 then SklDcs_QtOsn else 0 end) ExpenseQt,

        Sum(case when SklDcs_Dat >= @DtBeg AND SklDcs_Dat < @DtEnd AND SklDcs_Mov = 1 then SklDcs_QtOsn else 0 end) ReceiptQt,

        Max(isnull(SklCnBeg.SklCn_From, 0)) as BegCnFrom,

        Max(isnull(SklCnEnd.SklCn_From, 0)) as EndCnFrom,

        Max(isnull(SklCnBeg.SklCn_Dt, 0)) as BegCnDt,

        Max(isnull(SklCnEnd.SklCn_Dt, 0)) as EndCnDt,

        Max(SklCnBeg.SklCn_Cn) as BegCn,

        Max(SklCnEnd.SklCn_Cn) as EndCn

      From

         (

          Select SklKrt_Rcd, SklOu_Rcd

          From SklKrt

            INNER JOIN SklOpa ON SklOpa_Rcd = SklKrt_RcdOpa

            INNER Join SklStor ON SklStor.SklStor_Rcd = SklKrt_Stor and SklStor_Sh in ('041', '0442') /*список складов*/

            INNER JOIN SklN ON SklN_Rcd = SklOpa_RcdNom

            inner join SklGr on SklGr_Rcd = SklN_RcdGrp

            INNER JOIN SklOu ON SklOu_Rcd = SklKrt_RcdSOu

            INNER JOIN SOu ON Ou_CdBp = 3/*hSkl*/ AND Ou_Rcd = SklOu_OuCd

      ) ListCard

         Left join SklOst OstBeg ON OstBeg.SklOst_Krt = SklKrt_Rcd

          AND OstBeg.SklOst_Dt =

          (Select Top 1 SklOst_Dt

             from SklOst

             where SklOst_Krt = OstBeg.SklOst_Krt

                AND SklOst_Dt<@DtBeg

                    Order By SklOst_Dt Desc)

         Left join SklOst OstEnd ON OstEnd.SklOst_Krt = SklKrt_Rcd

          AND OstEnd.SklOst_Dt =

          (Select Top 1 SklOst_Dt

              from SklOst

              where SklOst_Krt = OstEnd.SklOst_Krt

                AND SklOst_Dt<@DtEnd

                Order By SklOst_Dt Desc)

         left Join SklDcs ON SklDcs_Krt = ListCard.SklKrt_Rcd AND SklDcs_Dat<@DtEnd

         AND (OstBeg.SklOst_Dt is NULL OR (not OstBeg.SklOst_Dt is NULL) AND SklDcs_Dat >= OstBeg.SklOst_Dt + 1)

             /*переоценка – если будет притормаживать, то можно этот блок убрать, переоценка бывает редко*/

         Left Join SklCn SklCnBeg on SklCnBeg.SklCn_Tp = 0 and SklCnBeg.SklCn_SOu = ListCard.SklOu_Rcd and SklCnBeg.SklCn_From <> 1 and SklCnBeg.bookmark =

               (Select top 1 isnull(SklCn.bookmark, 0) bMrk

                 from SklCn

                 Where SklCn_SOu = SklCnBeg.SklCn_SOu and SklCn_Tp = 0 and SklCn_From<> 1 and SklCn_Dt<CONVERT(varchar, @DtBeg, 112)

                  Order By SklCn.SklCn_Dt Desc  )

            Left Join SklCn SklCnEnd  on SklCnEnd.SklCn_Tp = 0 and SklCnEnd.SklCn_SOu = SklOu_Rcd and SklCnEnd.bookmark =

               (Select top 1 isnull(SklCn.bookmark, 0) bMrk

                 from SklCn

                 Where SklCn_SOu = SklCnEnd.SklCn_SOu and SklCn_Tp = 0

                    and((SklCn_From <> 1 and  /*CONVERT(varchar,*/ SklCn_Dt/*,    100)*/ < CONVERT(varchar, @DtEnd, 112))

                    or(SklCn_From <> 2 and CONVERT(varchar, SklCn_Dt, 112) = CONVERT(varchar, @DtEnd, 112))) 

                  Order By SklCn.SklCn_Dt Desc  )   

         Group By SklKrt_Rcd, SklOu_Rcd

  ) Oborotka

       Inner Join SklKrt ON SklKrt_Rcd = Oborotka.CardId

       inner join SKlOpa on SklKrt_RcdOpa = SklOpa_Rcd

       inner join SklN on SklN_Rcd = SklOpa_RcdNom

       inner join SklOu on SklOu_Rcd = SklKrt_RcdSou

       inner join SOU on Ou_Rcd = SklOu_OuCd and Ou_CdBp = 3

       inner join SklGr on SklGr_Rcd = SklN_RcdGrp

       left join SklStor on SklStor_Rcd = SklKrt_Stor

       )QV

group by SKLN_Cd, StorSh

having(sum(EndQt) + sum(QtRes)) > 0

order by SklN_nm";
    }
}
