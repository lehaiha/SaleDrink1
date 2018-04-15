using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleDrink.Areas.Admin.Models.DataModel
{
    [Table("UserPost")]
    public class UserPost
    {
        [Key]
        [Display(Name = "Mã số")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        [Display(Name = "Tiêu đề")]
        [StringLength(512)]
        [Required(ErrorMessage = "Hãy nhập tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Mô tả ngắn ngọn")]
        [StringLength(1024)]
        [Required(ErrorMessage = "Hãy nhập mô tả ngắn gọn")]
        public string Brief { get; set; }

        [Display(Name = "Nội dung bài viết")]
        [Required(ErrorMessage = "Hãy nhập nội dung")]
        [DataType(DataType.MultilineText)]
        [Column(TypeName = "ntext")]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name = "Ảnh bài viết")]
        [StringLength(256)]
        public string Picture { get; set; }

        [Display(Name = "Ngày tạo")]
        [DataType(DataType.DateTime)]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Thẻ tìm kiếm")]
        [StringLength(128)]
        public string Tags { get; set; }

        [Display(Name = "Mã chủ đề")]
        [ForeignKey("UserCatrgory")]
        public int CategoryId { get; set; }

        [Display(Name = "Số lần xem")]
        public int? ViewNo { get; set; }

        [Display(Name = "Trạng thái")]
        [StringLength(32)]
        public string Status { get; set; }

        [Display(Name = "Mã người dùng")]
        public int? UserId { get; set; }

        public virtual UserCategory UserCatrgory { get; set; }

        public virtual UserAdministrator UserAdministrator { get; set; }
    }
}