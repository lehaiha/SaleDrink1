namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Permission
    {
        public int ID { get; set; }

        [StringLength(128)]
        public string RoleId { get; set; }

        [StringLength(50)]
        public string FunctionId { get; set; }

        public bool CanCreate { get; set; }

        public bool CanRead { get; set; }

        public bool CanUpdate { get; set; }

        public bool CanDelete { get; set; }

        public virtual AppRole AppRole { get; set; }

        public virtual Function Function { get; set; }
    }
}
