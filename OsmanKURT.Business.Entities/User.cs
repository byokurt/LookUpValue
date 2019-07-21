using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsmanKURT.Business.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        [NotMapped]
        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
