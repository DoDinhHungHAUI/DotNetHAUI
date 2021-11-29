namespace ThucPhamSach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMucSanPham")]
    public partial class DanhMucSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhMucSanPham()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Vui lòng nhập mã danh mục")]
        [DisplayName("Mã danh mục")]
        public int MaDanhMuc { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [StringLength(100)]
        [DisplayName("Tên danh mục")]
        public string TenDanhMuc { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
