using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Bai01.Models
{
    public partial class BookStoreEF : DbContext
    {
        public BookStoreEF()
            : base("name=BookStoreEF")
        {
        }

        public virtual DbSet<TINTUC> TINTUC { get; set; }
        public virtual DbSet<THELOAITIN> THELOAITIN { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<THELOAITIN>()
                .HasMany(e => e.TINTUC)
                .WithRequired(e => e.THELOAITIN)
                .WillCascadeOnDelete(false);
        }
    }
}
