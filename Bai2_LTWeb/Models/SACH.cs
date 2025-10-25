namespace Bai2_LTWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SACH")]
    public partial class SACH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SACH()
        {
            CHITIETDONHANGs = new HashSet<CHITIETDONHANG>();
            VIETSACHes = new HashSet<VIETSACH>();
        }

        [Key]
        public int MASACH { get; set; }

        [Required]
        [StringLength(50)]
        public string TENSACH { get; set; }

        public decimal GIABAN { get; set; }

        public string MOTA { get; set; }

        [StringLength(100)]
        public string ANHBIA { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYCAPNHAT { get; set; }

        public int? SOLUONGTON { get; set; }

        public int? MACD { get; set; }

        public int? MANXB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONHANG> CHITIETDONHANGs { get; set; }

        public virtual CHUDE CHUDE { get; set; }

        public virtual NHAXUATBAN NHAXUATBAN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VIETSACH> VIETSACHes { get; set; }
    }
}
