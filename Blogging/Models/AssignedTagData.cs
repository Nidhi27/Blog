using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogging.Models
{
    public class AssignedTagData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Assigned { get; set; }
    }
}