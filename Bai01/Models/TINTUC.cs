namespace Bai01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TINTUC")]
    public partial class TINTUC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDTIN { get; set; }

        public int IDLOAI { get; set; }

        [Required]
        [StringLength(100)]
        public string TIEUDETIN { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string NOIDUNGTIN { get; set; }

        public virtual THELOAITIN THELOAITIN { get; set; }
    }
}
