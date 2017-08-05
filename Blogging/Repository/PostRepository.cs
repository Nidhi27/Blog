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
            
            // TO DO : Code to save record into database
            Post p = new Post();
           
            p.Title = post.Title;
            p.Content = post.Content;
            p.PostedOn = post.PostedOn;
            p.UserId = post.UserId;
            p.CategoryId = post.CategoryId;
            //p.Tags = post.Tags;
            //p.TagIds = post.TagIds;

            foreach (var assignedtag in post.Tags)
            {
                db = new ApplicationDbContext();
                db.Entry(assignedtag).State = EntityState.Unchanged;
                db.SaveChanges();
            }

          
            p.Tags = post.Tags;
            db.Posts.Add(p);
           
            db.SaveChanges();
            //db.Posts.Add(p);
            //db.SaveChanges();

          
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

           
            Post posts = db.Posts.SingleOrDefault(i => i.Id == post.Id);
            posts.Title = post.Title;
            posts.Content = post.Content;
            posts.PostedOn = post.PostedOn;
            posts.CategoryId = post.CategoryId;
           
            post.Tags = new List<Tag>();
            //ApplicationDbContext _db = new ApplicationDbContext();
            //Post posted = db.Posts.SingleOrDefault(i => i.Id == Id);
            //_db.Entry(posted).Collection(t => t.Tags).Load();
            var currentTags = post.Tags.ToList();

            posts.Tags.Clear();

            foreach (var tag in postToUpdate)
            {
                var currentTag = currentTags.SingleOrDefault(t => t.Id == tag.Id);
                if (tag != null)
                { 
               
                    posts.Tags.Add(tag);
                    db.Entry(tag).State = EntityState.Unchanged;

                }

                else
                {
                    //db.Tags.Attach(tag);
                    posts.Tags.Add(currentTag);
                }

               

            }
            //db.Entry()
            //  postToUpdate.Tags = post.Tags;
            //  var selectedTagsHS = new HashSet<string>(Tags);
            //var postTags = new HashSet<int>(postToUpdate.Tags.Select(t => t.Id));
            //foreach (var tag in db.Tags)
            //{
            //    postToUpdate.Tags = post.Tags;
            //    if (postToUpdate.Tags.Contains(tag))
            //    {
            //        if (!postToUpdate.Tags.Contains(tag))
            //        {
            //            postToUpdate.Tags.Add(tag);
            //        }
            //    }

            //    else
            //    {
            //        if (postToUpdate.Tags.Contains(tag))
            //        {
            //            postToUpdate.Tags.Remove(tag);
            //        }
            //    }


            //}
            db.Entry(posts).State = EntityState.Modified;
            db.SaveChanges();

            return true;

        }

        public void PopulateAssignedPostData(Post post)
        {
            var allTag = db.Tags;
            var postTags = new HashSet<int>(post.Tags.Select(t => t.Id));
            var viewModel = new List<AssignedTagData>();
            foreach (var tag in allTag)
            {
                viewModel.Add(new AssignedTagData
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Assigned = postTags.Contains(tag.Id)
                });
            }


        }

        //public void UpdatePostTags(Post postToUpdate)
        //{
        //    if (postToUpdate.Tags == null)
        //    {
        //        postToUpdate.Tags = new List<Tag>();
        //        return;
        //    }

        //    var selectedTagsHS = new HashSet<IList>(postToUpdate.Tags);
        //    var postTags = new HashSet<int>(postToUpdate.Tags.Select(t => t.Id));
        //    foreach (var tag in db.Tags)
        //    {
        //        if (selectedTagsHS.Contains(tag.Id.ToString()))
        //        {
        //            if (!postTags.Contains(tag.Id))
        //            {
        //                postToUpdate.Tags.Add(tag);
        //            }
        //        }

        //        else
        //        {
        //            if (postTags.Contains(tag.Id))
        //            {
        //                postToUpdate.Tags.Remove(tag);
        //            }
        //        }
        //    }
        //}

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