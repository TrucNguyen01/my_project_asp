namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            CauHinhs = new HashSet<CauHinh>();
            Comments = new HashSet<Comment>();
            GioHangs = new HashSet<GioHang>();
        }

        [Key]
        public int id_sanpham { get; set; }

        public int LoaiSanPham { get; set; }

        [Required(ErrorMessage = "Nhập tên sản phẩm")]
        [StringLength(200)]
        public string TenSanPham { get; set; }

        [Required]
        [StringLength(20)]
        public string Avatar { get; set; }

        [Required(ErrorMessage = "Nhập mô tả sản phẩm")]
        [StringLength(2000)]
        public string MoTa { get; set; }
        [Required(ErrorMessage = "Nhập số lượng sản phẩm")]
        public int SoLuong { get; set; }
       
        public int SoLuongMua { get; set; }
        [Required(ErrorMessage = "Nhập giá sản phẩm")]
        public double Gia { get; set; }
        [Required(ErrorMessage = "Nhập giá bán sản phẩm")]
        public double GiaBan { get; set; }
        [Required(ErrorMessage = "Nhập giá giảm sản phẩm")]
        public double GiaGiam { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayTao { get; set; }

        public int NhaCungCap { get; set; }

        public int TrangThai { get; set; }
        [Required(ErrorMessage = "Nhập độ hot sản phẩm")]
        public int DoHot { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CauHinh> CauHinhs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }

        public virtual LoaiSP LoaiSP { get; set; }

        public virtual NhaCungCap NhaCungCap1 { get; set; }
    }
}
