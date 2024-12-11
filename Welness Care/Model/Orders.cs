using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Welness_Care.Model
{
    [Table("Orders")]
    class Orders
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string CustomerId { get; set; }

        [MaxLength(50)]
        public string MSPId { get; set; }

        [MaxLength(50)]
        public string ServiceName { get; set; }

        [MaxLength(50)]
        public string ServiceCharges { get; set; }

        [MaxLength(50)]
        public string DistanceCharges { get; set; }

        [MaxLength(50)]
        public string TotalCharges { get; set; }

        [MaxLength(50)]
        public string Date { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        [MaxLength(200)]
        public string UserLatitude { get; set; }

        [MaxLength(200)]
        public string UserLongitude { get; set; }

        [MaxLength(200)]
        public string MSPLatitude { get; set; }

        [MaxLength(200)]
        public string MSPLongitude { get; set; }
    }
}
