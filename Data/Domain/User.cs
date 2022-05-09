using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Base;

namespace Data.Domain
{
    public class User : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Status { get; set; }
        public virtual List<Post> Post { get; set; }
        [NotMapped]
        public string FullName{get{return this.Name+" "+this.LastName;} private set{}}
    }
}