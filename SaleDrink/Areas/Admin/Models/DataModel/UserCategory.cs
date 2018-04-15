using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaleDrink.Areas.Admin.Models.DataModel
{
    [Table("UserCategory")]
    public class UserCategory
    {
        [Key]
        [Display(Name = "Mã chủ đề")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int CategoryId { get; set; }

        [Display(Name = "Tên chủ đề")]
        [Required(ErrorMessage = "Hãy nhập tên chủ đề")]
        [StringLength(256)]
        public string CategoryName { get; set; }

        [Display(Name = "Thứ tự xuất hiện")]
        [Required(ErrorMessage = "Hãy nhập thứ tự")]
        public int OrderNo { get; set; }

        [Display(Name = "Trạng thái")]
        [StringLength(32)]
        public string Status { get; set; }

        [Display(Name = "Mã người dùng")]
        public int? UserId { get; set; }

        public virtual UserAdministrator UserAdministrator { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}