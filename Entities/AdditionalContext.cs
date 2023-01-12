using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AdditionalContext : DbContext
    {
        public AdditionalContext()
        {
            //Database.EnsureCreated();
        }

        public AdditionalContext(DbContextOptions<AdditionalContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=MaterialsDB;Integrated Security=True;MultipleActiveResultSets=true");
            optionsBuilder.UseSqlServer("Data Source=192.168.168.100;Initial Catalog=vkt007;User id=pvt_fa;Password=Fed4evg62+=;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaterialInfoFromOuterBase>().HasNoKey();
        }

        public DbSet<MaterialInfoFromOuterBase> MaterialsInfoFromOuterBase { get; set; }
    }
}
