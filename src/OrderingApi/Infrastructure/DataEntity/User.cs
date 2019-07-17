using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.DataEntity
{
    public class User : IDataEntity
    {
        [Key]
        [Column("id", TypeName = "uuid")]
        public string Id { get; set; }

        [Required]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        public string LastName { get; set; }

        [Required]
        [Column("address")]
        public Address Address { get; set; }

        [Required]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        public Cart Cart { get; set; }
        public IList<Order> Orders { get; set; }

    }
}
