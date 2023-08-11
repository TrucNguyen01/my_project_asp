using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication4.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model119")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<CauHinh> CauHinhs { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<GiamGia> GiamGias { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<LoaiSP> LoaiSPs { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<PhanHoi> PhanHois { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TinTuc> TinTucs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.TaiKhoan)
                .IsFixedLength();

            modelBuilder.Entity<Account>()
                .Property(e => e.MatKhau)
                .IsFixedLength();

            modelBuilder.Entity<Account>()
                .Property(e => e.SoDienThoai)
                .IsFixedLength();

            modelBuilder.Entity<Account>()
                .Property(e => e.Email)
                .IsFixedLength();

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.DonHangs)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.PhanHois)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CauHinh>()
                .Property(e => e.HinhAnh_CPU)
                .IsFixedLength();

            modelBuilder.Entity<CauHinh>()
                .Property(e => e.HinhAnh_RAM)
                .IsFixedLength();

            modelBuilder.Entity<CauHinh>()
                .Property(e => e.HinhAnh_Camera)
                .IsFixedLength();

            modelBuilder.Entity<CauHinh>()
                .Property(e => e.HinhAnh_Pin)
                .IsFixedLength();

            modelBuilder.Entity<Comment>()
                .HasMany(e => e.PhanHois)
                .WithRequired(e => e.Comment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.Email)
                .IsFixedLength();

            modelBuilder.Entity<DonHang>()
                .Property(e => e.SoDienThoai)
                .IsFixedLength();

            modelBuilder.Entity<GiamGia>()
                .Property(e => e.code)
                .IsFixedLength();

            modelBuilder.Entity<GioHang>()
                .Property(e => e.DonGiaBan)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoaiSP>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.LoaiSP)
                .HasForeignKey(e => e.LoaiSanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.SoDienThoai)
                .IsFixedLength();

            modelBuilder.Entity<NhaCungCap>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.NhaCungCap1)
                .HasForeignKey(e => e.NhaCungCap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.Avatar)
                .IsFixedLength();

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.CauHinhs)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TinTuc>()
                .Property(e => e.NoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<TinTuc>()
                .Property(e => e.HinhAnh)
                .IsFixedLength();
        }
    }
}
