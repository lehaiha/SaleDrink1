using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleDrink.Areas.Admin.Models.DataModel
{
    [Table("UserAdministrator")]
    public class UserAdministrator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Hãy nhập tên người dùng")]
        [StringLength(64, ErrorMessage = "Tên đăng nhập phải trong khoảng 3-64 ký tự", MinimumLength = 3)]
        [Column(TypeName = "varchar")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Hãy nhập mật khẩu")]
        [MaxLength(256)]
        [Column(TypeName = "varchar")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Hãy nhập họ và tên")]
        [Display(Name = "Họ và tên")]
        [MaxLength(64)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email nhập không đúng định dạng")]
        [Display(Name = "Email")]
        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Ảnh đại diện")]
        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        public string Avatar { get; set; }

        [Display(Name = "Là quản trị")]
        public byte? IsAdmin { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool Allowed { get; set; }
        public DateTime CreatedDate { get; set; }

        //Thuộc tính Navigation
        public ICollection<UserGrantPermission> UserGrantPermissions { get; set; }
    }
}