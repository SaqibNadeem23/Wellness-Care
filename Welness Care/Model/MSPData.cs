using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Welness_Care.Model
{
    [Table("MSPData")]
    class MSPData
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string MSPId { get; set; }

        [MaxLength(50)]
        public string Gender { get; set; }

        [MaxLength(200)]
        public string ActiveStatus { get; set; }

        [MaxLength(50)]
        public string OrderUserId { get; set; }

        [MaxLength(200)]
        public string MSPLatitude { get; set; }

        [MaxLength(200)]
        public string MSPLongitude { get; set; }


        [MaxLength(200)]
        public string ServiceCharges { get; set; }

        [MaxLength(200)]
        public string Designation { get; set; }

        [MaxLength(200)]
        public string Experience { get; set; }


        [MaxLength(200)]
        public string BloodPressureService { get; set; }

        [MaxLength(200)]
        public string InjectionsService { get; set; }

        [MaxLength(200)]
        public string BandagesService { get; set; }

        [MaxLength(200)]
        public string InsulinService { get; set; }

        [MaxLength(200)]
        public string PainKillerService { get; set; }

        [MaxLength(200)]
        public string ChestPainService { get; set; }

        [MaxLength(200)]
        public string MinorInjuryService { get; set; }

        [MaxLength(200)]
        public string BreatingProblemService { get; set; }


    }
}
