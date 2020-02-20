using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gap.NetFest.DataAccess.DTO
{
    [Table("Customers")]
    public class Customers
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("first_name")]
        public string Name { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        public ICollection<Invoices> Invoices { get; set; }
    }
}
