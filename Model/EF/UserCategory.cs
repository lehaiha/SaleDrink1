namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserCategory")]
    public partial class UserCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserCategory()
        {
            UserPosts = new HashSet<UserPost>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(256)]
        public string CategoryName { get; set; }

        public int OrderNo { get; set; }

        [StringLength(32)]
        public string Status { get; set; }

        public int? UserId { get; set; }

        public virtual UserAdministrator UserAdministrator { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}
