namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GioHang")]
    public partial class GioHang
    {
        [Key]
        public int id_giohang { get; set; }

        public int? id_sanpham { get; set; }

        public int? id_account { get; set; }

        public int? id_donhang { get; set; }

        [Column(TypeName = "money")]
        public decimal? DonGiaBan { get; set; }

        public int? SoLuongBan { get; set; }

        public int? TrangThai { get; set; }

        public virtual Account Account { get; set; }

        public virtual DonHang DonHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
