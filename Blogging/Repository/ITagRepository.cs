using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogging.Models;

namespace Blogging.Repository
{
    interface ITagRepository
    {
        IQueryable GetAll();
        Tag Get(int id);
        Tag Add(Tag tag);
        bool Update(Tag tag);
        bool Delete(int id);
    }
}
