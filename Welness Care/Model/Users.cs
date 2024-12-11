using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Welness_Care.Model
{

    [Table("Users")]
    public class Users
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int UserId { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string FullName { get; set; }

        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Gender { get; set; }


        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(50)]
        public string UserType { get; set; }

        [MaxLength(50)]
        public string OrderMSPId { get; set; }
    }
}



