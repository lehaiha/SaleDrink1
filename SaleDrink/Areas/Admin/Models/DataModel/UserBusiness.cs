using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleDrink.Areas.Admin.Models.DataModel
{
    [Table("UserBusiness")]
    public class UserBusiness
    {
        [Key]
        [MaxLength(64)]
        [Display(Name = "Mã nghiệp vụ")]
        [Column(TypeName = "varchar")]
        public string BusinessId { get; set; }

        [Required(ErrorMessage = "Hãy nhập tên nghiệp vụ")]
        [Display(Name = "Tên nghiệp vụ")]
        [MaxLength(256)]
        public string BusinessName { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool? Status { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}