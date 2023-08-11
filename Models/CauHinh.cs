namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CauHinh")]
    public partial class CauHinh
    {
        [Key]
        public int id_cauhinh { get; set; }

        [StringLength(50)]
        public string ManHinh { get; set; }

        [StringLength(50)]
        public string DoPhanGiai { get; set; }

        [StringLength(50)]
        public string CongNgheManHinh { get; set; }

        [StringLength(50)]
        public string RAM { get; set; }

        [StringLength(50)]
        public string BoNho { get; set; }

        [StringLength(50)]
        public string ChatLieu { get; set; }

        [StringLength(50)]
        public string KichThuoc { get; set; }

        [StringLength(50)]
        public string TrongLuong { get; set; }

        [StringLength(200)]
        public string Camera { get; set; }

        [StringLength(200)]
        public string Pin { get; set; }

        [StringLength(200)]
        public string CPU { get; set; }

        [StringLength(20)]
        public string HinhAnh_CPU { get; set; }

        [StringLength(20)]
        public string HinhAnh_RAM { get; set; }

        [StringLength(20)]
        public string HinhAnh_Camera { get; set; }

        [StringLength(20)]
        public string HinhAnh_Pin { get; set; }

        [StringLength(1000)]
        public string NoiDung_CPU { get; set; }

        [StringLength(1000)]
        public string NoiDung_RAM { get; set; }

        [StringLength(1000)]
        public string NoiDung_Camera { get; set; }

        [StringLength(1000)]
        public string NoiDung_Pin { get; set; }

        [StringLength(50)]
        public string Chip { get; set; }

        public int id_sanpham { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
