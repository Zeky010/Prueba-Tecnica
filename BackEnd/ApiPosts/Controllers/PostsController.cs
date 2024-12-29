using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ApiPosts.Models;

namespace ApiPosts.Controllers
{
    public class PostsController : ApiController
    {
        private DataAccessLayer dataAccess;
        
        [HttpGet]
        //GET /api/posts
        public IHttpActionResult Index()
        {
            string[] endPoints = { "[GET] - ListPosts", "[POST] - CreatePost", "[GET] - DeletePost(id: numeric)" };

            return Ok(endPoints);
        }
        //[Route("listposts")]
        [HttpGet]
        //GET /api/posts/listposts
        public async Task<IHttpActionResult> ListPosts()
        {
            try
            {
                dataAccess = new DataAccessLayer();

                List<Post> posts = await dataAccess.GetPosts();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[Route("createpost")]
        [HttpPost]
        [ActionName("createpost")]
        //POST /api/posts/createpost
        public async Task<IHttpActionResult> CreatePost(Post post)
        {
            dataAccess = new DataAccessLayer();
            Post postInsertado = await dataAccess.InsertPost(post);

            if (postInsertado != null)  return Ok(postInsertado);
            else                        return BadRequest();




        }

        //[Route("deletepost")]
        [HttpGet]
        //GET /api/posts/deletepost/5
        public async Task<IHttpActionResult> DeletePost(int id)
        {
            dataAccess = new DataAccessLayer();
            Post postInsertado = await dataAccess.DeletePost(id);

            if (postInsertado != null) return Ok(postInsertado);
            else return InternalServerError();

        }

    }


}
