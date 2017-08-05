using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Blogging.Repository;
using Blogging.Models;
using System.Collections;

namespace Blogging.Controllers
{
    [Authorize]
    public class TagController : ApiController
    {
        private static ITagRepository repository = new TagRepository();

        ApplicationDbContext db = new ApplicationDbContext();
        // api/Get
        [AllowAnonymous]
        [Route("api/tag/")]
        public IQueryable GetAllTag()
        {

            return repository.GetAll();
        }
        [Authorize(Roles = "Admin")]
        public Tag PostTags(Tag tag)
        {

            return repository.Add(tag);
        }
        [Authorize(Roles = "Admin")]
        public IEnumerable PutTag(int id, Tag tag)
        {
            tag.Id = id;
            if (repository.Update(tag))
            {
                return repository.GetAll();
            }
            else
            {
                return null;
            }
        }
        [Authorize(Roles = "Admin")]
        public bool DeleteTag(int id)
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
