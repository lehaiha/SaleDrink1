using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleDrink.Areas.Admin.Models.DataModel
{
    [Table("UserPermission")]
    public class UserPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionId { get; set; }

        [Required(ErrorMessage = "Hãy nhập tên quyền hạn")]
        [Display(Name = "Tên quyền")]
        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        public string PermissionName { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Hãy nhập mô tả quyền hạn")]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required()]
        [MaxLength(64)]
        [Display(Name = "Mã nghiệp vụ")]
        [ForeignKey("UserBusinesses")]
        [Column(TypeName = "varchar")]
        public string BusinessId { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool? Status { get; set; }

        //Thuộc tinh Navigation
        public virtual UserBusiness UserBusinesses { get; set; }

        public virtual ICollection<UserGrantPermission> UserGrantPermissions { get; set; }
    }
}