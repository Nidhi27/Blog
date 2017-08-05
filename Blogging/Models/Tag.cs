using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blogging.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public IList<int> PostIds { get; set; }

       
        public virtual IList<Post> Posts { get; set; }
    }
}