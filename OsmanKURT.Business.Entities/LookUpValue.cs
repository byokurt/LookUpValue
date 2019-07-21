using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsmanKURT.Business.Entities
{
    [Table("LookUpValue")]
    public class LookUpValue
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public string ApplicationName { get; set; }

        [NotMapped]
        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
