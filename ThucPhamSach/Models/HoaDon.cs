namespace ThucPhamSach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        [Required]
        [StringLength(100)]
        public string DiaChi { get; set; }

        public DateTime NgayDat { get; set; }

        public int MaTK { get; set; }

        public decimal TongTien { get; set; }

        public int MaQuyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
