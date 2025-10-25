using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Bai3_LTWeb.Models
{
    public partial class QL_NhanSuN : DbContext
    {
        public QL_NhanSuN()
            : base("name=QL_NhanSuN")
        {
        }

        public virtual DbSet<tbl_Department> tbl_Department { get; set; }
        public virtual DbSet<tbl_Employee> tbl_Employee { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_Department>()
                .HasMany(e => e.tbl_Employee)
                .WithRequired(e => e.tbl_Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Employee>()
                .Property(e => e.Photo)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<Bai3_LTWeb.Models.NV_PBViewModel> NV_PBViewModel { get; set; }
    }
}
