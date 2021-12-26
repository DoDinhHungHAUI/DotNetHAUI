namespace ThucPhamSach.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? MaTK { get; set; }


        [StringLength(100)]
        [Required(ErrorMessage = "Vui nh?p h? và tên")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui nh?p tên")]
        [StringLength(50)]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Vui lòng nh?p email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Vui lòng nh?p email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui nh?p ?i?n tho?i")]
        [RegularExpression(@"^(\d{10,11})$", ErrorMessage = "Vui lòng nh?p s? ?i?n tho?i")]
        [StringLength(50)]
        public string DienThoai { get; set; }

        [Required(ErrorMessage = "Vui nh?p m?t kh?u")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "M?t kh?u ph?i t? 5 ??n 20 ký t? ", MinimumLength = 5)]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Vui nh?p ??a ch?")]
        [StringLength(100)]
        public string DiaChi { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaQuyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }



        public virtual PhanQuyen PhanQuyen { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Vui nh?p m?t kh?u")]
        [Compare("MatKhau", ErrorMessage = "M?t kh?u không trùng nhau")]
        public string ConfirmPassword { get; set; }
    }
}
