using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Blogging.Models;
using Blogging.Repository;
using System.Collections;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Blogging.Controllers
{
    [Authorize]
    public class PostController : ApiController
    {
        private static IPostRepository repository = new PostRepository();

        ApplicationDbContext db = new ApplicationDbContext();


        // api/Get
      
     [AllowAnonymous]
        public IQueryable GetAllPost(int categoryId=0, int tagId = 0, int Id = 0)
        {
            if (categoryId > 0)
            {
                return repository.GetAllByCategoryId(categoryId);
            }
            else if(tagId>0)
            {
                return repository.GetAllByTagId(tagId);
            }

            else if (Id > 0)
            {
                return repository.Get(Id);
            }

            else
            {
                return repository.GetAll();
            }
        }

        // api/Get        
        [Route("user")]
        public IEnumerable GetAllPostByUserId()
        {
            var userId = User.Identity.GetUserId();
            return repository.GetAll(userId).ToList();
        }

        //[Route("GetPostCategory")]
        //public IEnumerable GetPostCategory(int id)
        //{
        //    return repository.GetPostCategory(id).ToList();
        //}
      
        public Post PostPosts(Post post)
        {
            //  var sdsd=HttpContext.Current.User.Identity.Name;

            //var bghg = db.Users.FirstOrDefault(x => x.UserName == sdsd);
            //  post.UserId = bghg.Id;

         
            post.UserId = User.Identity.GetUserId();
           // db.SaveChanges();
            return repository.Add(post);

           
        }


        [Authorize(Roles = "Admin")]
        public IEnumerable PutPost(int id, Post post)
        {
            post.Id = id;

            if (repository.Update(id, post))
            {
                return repository.GetAll();
            }
            else
            {
                return null;
            }
        }
        
       [Authorize(Roles ="Admin")]
        public bool DeletePost(int id)
        {
            if (repository.Delete(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
