namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserPermission")]
    public partial class UserPermission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserPermission()
        {
            UserGrantPermissions = new HashSet<UserGrantPermission>();
        }

        [Key]
        public int PermissionId { get; set; }

        [Required]
        [StringLength(256)]
        public string PermissionName { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        [StringLength(64)]
        public string BusinessId { get; set; }

        public bool? Status { get; set; }

        public virtual UserBusiness UserBusiness { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserGrantPermission> UserGrantPermissions { get; set; }
    }
}
