using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class MainContext: DbContext
    {
        public MainContext()
        {}

        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=MaintananceDB;Integrated Security=True;MultipleActiveResultSets=true");
            optionsBuilder.UseMySql("server=94.231.127.68;port=15100;database=pvtdata;uid=userpvtrans1;pwd=UserPV1", ServerVersion.Parse("5.5.23-mysql"));
            //optionsBuilder.UseMySql("server=192.168.1.252;port=3306;database=pvtdata;uid=userpvtrans1;pwd=UserPV1", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.5.23-mysql"));
        }
        public DbSet<EquipmentSupplier> EquipmentSuppliers { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<ErrorCode> ErrorCodes { get; set; }
        public DbSet<MaintenanceError> MaintenanceErrors { get; set; }
        public DbSet<MaintenanceInfo> MaintenanceInfos { get; set; }
        public DbSet<MaintenanceEpisode> MaintenanceEpisodes { get; set; }
        public DbSet<MaterialInfo> MaterialInfos { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Repairing> Repairings { get; set; }
        public DbSet<TechPassport> TechPassports { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<ArtInfo> ArtInfos { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<HoursInfo> WorkingHours { get; set; }
        public DbSet<ControledParametrDateInfo> ControledParametrDateInfos { get; set; }
        public DbSet<ControledParametr> ControledParametrs { get; set; }
        public DbSet<AdditionalWork> AdditionalWorks { get; set; }
        public DbSet<MaintenanceType> MaintenanceTypes { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ElectroPoint> Points { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Downtime> Downtimes { get; set; }
    }
}
