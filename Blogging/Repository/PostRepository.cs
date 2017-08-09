using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blogging.Models;
using System.Data.Entity;
using System.Web.Http.ModelBinding;

namespace Blogging.Repository
{
    public class PostRepository : IPostRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();


        public IEnumerable<Post> GetAll(string userId)
        {
            // TO DO : Code to get the list of all the records in database

            return db.Posts.Where(p=> p.UserId == userId);
        }

        public IQueryable GetAllByCategoryId(int categoryId)
        {
            var data = db.Posts.Where(p => p.CategoryId == categoryId)
                .Select( x => new
                {
                    Id = x.Id,
                    Title = x.Title,
                    CategoryId = x.CategoryId,

                    UserName = db.Users.Where(y => y.Id == x.UserId)
                    .Select(y => y.UserName)
                    .FirstOrDefault(),
                    CategoryName = db.Categories.Where(c => c.Id == x.CategoryId)
                                    .Select(y => y.Name)
                                    .FirstOrDefault(),
                    TagName = x.Tags.Select(m => new { Id = m.Id, Name = m.Name }).ToList(),
                    Tags = x.Tags.Select(m => new { Id = m.Id, Name = m.Name }).ToList(),
                    PostedOn = x.PostedOn,
                    Content = x.Content,
                });

            return data;
        }

        public IQueryable GetAllByTagId(int tagId)
        {
            var data = db.Posts.Where(x => x.Tags.Select(m => m.Id).Contains(tagId))
                .Select(x => new
                {
                    Id = x.Id,
                    Title = x.Title,
                    CategoryId = x.CategoryId,

                    UserName = db.Users.Where(y => y.Id == x.UserId)
                   .Select(y => y.UserName)
                   .FirstOrDefault(),
                    CategoryName = db.Categories.Where(c => c.Id == x.CategoryId)
                                   .Select(y => y.Name)
                                   .FirstOrDefault(),
                    TagName = x.Tags.Select(m => new { Id = m.Id, Name = m.Name }).ToList(),
                    Tags = x.Tags.Select(m => new { Id = m.Id, Name = m.Name }).ToList(),
                    PostedOn = x.PostedOn,
                    Content = x.Content,
                });

            return data;

        }

        //public IEnumerable<Post> GetAllByTagIds(int tagId)
        //{
        //    return db.Posts.Where(p => p.TagIds == tagId);
        //}

        public IQueryable GetAll()
        {
            // TO DO : Code to get the list of all the records in database
            var data = db.Posts
                .Select(x => new
                {
                    Id = x.Id,
                    Title = x.Title,
                    CategoryId = x.CategoryId,
                    UserName = db.Users.Where(y => y.Id == x.UserId)
                    .Select(y => y.UserName)
                    .FirstOrDefault(),
                    CategoryName = db.Categories.Where(c => c.Id == x.CategoryId)
                                    .Select(y => y.Name)
                                    .FirstOrDefault(),
                    TagName = x.Tags.Select(m => new { Id = m.Id, Name = m.Name }).ToList(),
                     Tags = x.Tags.Select(m => new { Id = m.Id, Name = m.Name }).ToList(),
                     PostedOn = x.PostedOn,
                     Content = x.Content,
                 });
            return data;
        }

        public IQueryable Get(int Id)
        {
            var data = db.Posts.Where(x => x.Id==Id)
                .Select(x => new
                {
                    Id = x.Id,
                    Title = x.Title,
                    CategoryId = x.CategoryId,
                    UserName = db.Users.Where(y => y.Id == x.UserId)
                   .Select(y => y.UserName)
                   .FirstOrDefault(),
                    CategoryName = db.Categories.Where(c => c.Id == x.CategoryId)
                                   .Select(y => y.Name)
                                   .FirstOrDefault(),
                    TagName = x.Tags.Select(m => new { Id = m.Id, Name = m.Name }).ToList(),
                    Tags = x.Tags.Select(m => new { Id = m.Id, Name = m.Name }).ToList(),
                    PostedOn = x.PostedOn,
                    Content = x.Content,
                });

            return data;

        }

        //public IQueryable GetPostCategory(int id)
        //{
        //    var posts = db.Posts.Where(p => p.CategoryId == id);

        //    return posts;
        //}

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




        public Post Add(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException("post");
            }
            db = new ApplicationDbContext();
            // TO DO : Code to save record into database
            Post p = new Post();
           
            p.Title = post.Title;
            p.Content = post.Content;
            p.PostedOn = post.PostedOn;
            p.UserId = post.UserId;
            p.CategoryId = post.CategoryId;
            
            foreach (var assignedtag in post.Tags)
            {
                db.Entry(assignedtag).State = EntityState.Unchanged;
            }

          
            p.Tags = post.Tags;
            db.Posts.Add(p);
            db.SaveChanges();
            

          
            //for (int i = 0; i < post.TagIds.Count; i++)
            //{

            //    postTagMapping.PostId = post.Id;
            //    postTagMapping.TagId = post.TagIds[i];
            //    db.PostTagMappings.Add(postTagMapping);
            // }


            return post;

        }
       

        public bool Update(int Id, Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException("post");
            }

            var postToUpdate = post.Tags;
            db = new ApplicationDbContext();
            Post posts = db.Posts.SingleOrDefault(i => i.Id == post.Id);
            posts.Title = post.Title;
            posts.Content = post.Content;
            posts.PostedOn = post.PostedOn;
            posts.CategoryId = post.CategoryId;
            foreach (var assignedtag in post.Tags)
            {
              db.Entry(assignedtag).State = EntityState.Unchanged;
            }
            post.Tags = new List<Tag>();
            var currentTags = post.Tags.ToList();
            posts.Tags.Clear();
            foreach (var tag in postToUpdate)
            {
                var currentTag = currentTags.SingleOrDefault(t => t.Id == tag.Id);
                if (tag != null)
                {
                    posts.Tags.Add(tag);
                }
                //else
                //{
                //    posts.Tags.Add(tag);
                //}
            }
            db.SaveChanges();
            return true;

        }


        public bool Delete(int id)
        {
            // TO DO : Code to remove the records from database
            Post posts = db.Posts.Find(id);
            db.Posts.Remove(posts);
            db.SaveChanges();
            return true;
        }
    }
}