namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        [Key]
        public int id_tintuc { get; set; }

        [StringLength(500)]
        public string TieuDe { get; set; }

        [Column(TypeName = "text")]
        public string NoiDung { get; set; }

        [StringLength(20)]
        public string HinhAnh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        [StringLength(1000)]
        public string GioiThieu { get; set; }

        public int? TrangThai { get; set; }
    }
}
