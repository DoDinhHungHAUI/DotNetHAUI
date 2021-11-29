namespace ThucPhamSach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Vui nhập mã sản phẩm")]
        [DisplayName("Mã sản phẩm")]
        public int MaSP { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã sản phẩm")]
        [DisplayName("Tên sản phẩm")]
        [StringLength(100)]
        public string TenSanPham { get; set; }

        [DisplayName("Mô tả")]
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Vui lòng mô tả")]
        public string MoTa { get; set; }

        [DisplayName("Nhà cung cấp")]
        [Required(ErrorMessage = "Vui lòng nhập nhà cung cấp")]
        [StringLength(100)]
        public string NhaCungCap { get; set; }

        [DisplayName("Giá bán")]
        [Required(ErrorMessage = "Vui lòng nhập giá bán")]
        public decimal GiaBan { get; set; }

        [DisplayName("Hình ảnh")]
        [StringLength(100)]
        public string Anh { get; set; }

        
        public int MaDanhMuc { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [DisplayName("Số lượng")]
        public int? SoLuong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual DanhMucSanPham DanhMucSanPham { get; set; }
    }
}
