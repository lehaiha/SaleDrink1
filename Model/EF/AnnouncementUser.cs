namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AnnouncementUser
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnnouncementId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public bool HasRead { get; set; }

        public virtual Announcement Announcement { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
