namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductsShip")]
    public partial class ProductsShip
    {
        public int id { get; set; }

        public double? ShipRate { get; set; }

        public double? FromDes { get; set; }

        public double? ToDes { get; set; }
    }
}
