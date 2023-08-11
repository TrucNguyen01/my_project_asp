namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhanHoi")]
    public partial class PhanHoi
    {
        [Key]
        public int id_phanhoi { get; set; }

        [StringLength(200)]
        public string NoiDung { get; set; }

        public int id_comment { get; set; }

        public int id_account { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayPhanHoi { get; set; }

        public virtual Account Account { get; set; }

        public virtual Comment Comment { get; set; }
    }
}
