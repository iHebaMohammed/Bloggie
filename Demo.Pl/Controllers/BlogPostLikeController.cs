using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Entities;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Pl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            _blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeViewModel addLikeViewModel)
        {
            var model = new BlogPostLike
            {
                BlogPostId = addLikeViewModel.BlogPostId,
                UserId = addLikeViewModel.UserId
            };

            await _blogPostLikeRepository.AddLikeForBlogPost(model);

            return Ok();
        }


        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {
            var totalLikes = await _blogPostLikeRepository.GetTotalLikes(blogPostId);

            return Ok(totalLikes);
        }
    }
}
