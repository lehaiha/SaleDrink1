using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleDrink.Areas.Admin.Models.DataModel
{
    [Table("UserGrantPermission")]
    public class UserGrantPermission
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("UserPermission")]
        [Display(Name = "Mã quyền hạn")]
        [Required]
        public int PermissionId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("UserAdministrator")]
        [Display(Name = "Mã người dùng")]
        [Required]
        public int UserId { get; set; }

        [Display(Name = "Mô tả")]
        [MaxLength(256)]
        public string Description { get; set; }

        //Thuộc tính Navigation
        public virtual UserPermission UserPermission { get; set; }
        public virtual UserAdministrator UserAdministrator { get; set; }
    }
}