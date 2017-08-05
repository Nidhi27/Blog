using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blogging.Models;

namespace Blogging.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();


        public IEnumerable<Category> GetAll()
        {
            // TO DO : Code to get the list of all the records in database

            return db.Categories;
        }

        public Category Get(int id)
        {
            // TO DO : Code to find a record in database
            return db.Categories.Find(id);
        }

        //public Employee Add(Employee item)
        //{
        //    if (item == null)
        //    {
        //        throw new ArgumentNullException("item");
        //    }

        //    // TO DO : Code to save record into database
        //    db.Employees.Add(item);
        //    db.SaveChanges();
        //    return item;
        //}

        public Category Add(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            // TO DO : Code to save record into database
            db.Categories.Add(category);
            db.SaveChanges();
            return category;
        }
        public bool Update(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            // TO DO : Code to update record into database
            var categories = db.Categories.Single(a => a.Id == category.Id);
            categories.Name = category.Name;
           

            db.SaveChanges();

            return true;
        }


        public bool Delete(int id)
        {
            // TO DO : Code to remove the records from database
            Category categories = db.Categories.Find(id);
            db.Categories.Remove(categories);
            db.SaveChanges();
            return true;
        }
    }
}