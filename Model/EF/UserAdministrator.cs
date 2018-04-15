namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAdministrator")]
    public partial class UserAdministrator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserAdministrator()
        {
            UserCategories = new HashSet<UserCategory>();
            UserGrantPermissions = new HashSet<UserGrantPermission>();
            UserPosts = new HashSet<UserPost>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(64)]
        public string UserName { get; set; }

        [Required]
        [StringLength(256)]
        public string Password { get; set; }

        [Required]
        [StringLength(64)]
        public string FullName { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(256)]
        public string Avatar { get; set; }

        public byte? IsAdmin { get; set; }

        public bool Allowed { get; set; }

        public DateTime CreatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCategory> UserCategories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserGrantPermission> UserGrantPermissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}
