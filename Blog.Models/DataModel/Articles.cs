using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Blog.Models.DataModel
{
    public partial class Articles
    {
        public int Id { get; set; }
        public int CreativeId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string Theme { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Users Creative { get; set; }
    }
}
