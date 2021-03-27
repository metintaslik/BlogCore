using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Blog.Models.DataModel
{
    public partial class Users
    {
        public Users()
        {
            Articles = new HashSet<Articles>();
            Categories = new HashSet<Categories>();
        }

        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public int RoleId { get; set; }
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }

        public virtual Roles Role { get; set; }
        [JsonIgnore]
        public virtual ICollection<Articles> Articles { get; set; }
        [JsonIgnore]
        public virtual ICollection<Categories> Categories { get; set; }
    }
}
