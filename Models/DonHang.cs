namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            GioHangs = new HashSet<GioHang>();
        }

        [Key]
        public int id_DonHang { get; set; }

        [Required]
        [StringLength(50)]
        public string TenKhachHang { get; set; }

        [Required]
        [StringLength(50)]
        public string DiaChi { get; set; }

        [StringLength(20)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string SoDienThoai { get; set; }

        public int? id_magiamgia { get; set; }

        public int id_account { get; set; }

        public double? TongTien { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        public int? TrangThai { get; set; }

        public virtual Account Account { get; set; }

        public virtual GiamGia GiamGia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }
    }
}
