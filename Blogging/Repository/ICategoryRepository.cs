using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogging.Models;

namespace Blogging.Repository
{
    interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category Get(int id);
        Category Add(Category category);
        bool Update(Category category);
        bool Delete(int id);
    }
}
