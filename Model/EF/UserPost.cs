namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserPost")]
    public partial class UserPost
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [StringLength(512)]
        public string Title { get; set; }

        [Required]
        [StringLength(1024)]
        public string Brief { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }

        [StringLength(256)]
        public string Picture { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(128)]
        public string Tags { get; set; }

        public int CategoryId { get; set; }

        public int? ViewNo { get; set; }

        [StringLength(32)]
        public string Status { get; set; }

        public int? UserId { get; set; }

        public virtual UserAdministrator UserAdministrator { get; set; }

        public virtual UserCategory UserCategory { get; set; }
    }
}
