using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Entities;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Pl.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBlogPostCommentRepository _blogPostCommentRepository;

        public BlogsController(
            IBlogPostRepository blogPostRepository ,
            IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IBlogPostCommentRepository blogPostCommentRepository
            ) 
        {
            _blogPostRepository = blogPostRepository;
            _blogPostLikeRepository = blogPostLikeRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _blogPostCommentRepository = blogPostCommentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var blogPost = await _blogPostRepository.GetByUrlHandle(urlHandle);
            var model = new BlogDetailsViewModel();

            if (blogPost != null) 
            { 
                var totalLikes = await _blogPostLikeRepository.GetTotalLikes(blogPost.Id);

                if (_signInManager.IsSignedIn(User))
                {
                    // Get like for this blog and this user
                    var likesForBlog = await _blogPostLikeRepository.GetLikesForBlog(blogPost.Id);

                    var userId = _userManager.GetUserId(User);

                    if (userId != null)
                    {
                        var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }
                }

                // Get comment for blog post
                var blogPostComments = await _blogPostCommentRepository.GetAllCommnetByBlogPostId(blogPost.Id);
                var blogCommentForView = new List<BlogCommentViewModel>();

                foreach (var blogPostComment in blogPostComments)
                {
                    blogCommentForView.Add(new BlogCommentViewModel
                    { 
                        Description = blogPostComment.Description,
                        DateAdded = blogPostComment.DateAdded,
                        UserName = (await _userManager.FindByIdAsync(blogPostComment.UserId.ToString())).UserName
                    });
                }

                model = new BlogDetailsViewModel()
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Heading = blogPost.Heading,
                    IsVisble = blogPost.IsVisble,
                    PageTitle = blogPost.PageTitle,
                    PublishDate = blogPost.PublishDate,
                    ShortDescription = blogPost.ShortDescription,
                    Tags = blogPost.Tags,
                    UrlHandle = blogPost.UrlHandle,
                    Totallikes = totalLikes,
                    Liked = liked,
                    Commnets = blogCommentForView
                };
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
        {
            if (_signInManager.IsSignedIn(User))
            {
                if(blogDetailsViewModel.CommentDescription != null)
                {
                    var blogPostComment = new BlogPostComment()
                    {
                        BlogPostId = blogDetailsViewModel.Id,
                        UserId = Guid.Parse(_userManager.GetUserId(User)),
                        DateAdded = DateTime.Now,
                        Description = blogDetailsViewModel.CommentDescription
                    };
                    await _blogPostCommentRepository.Add(blogPostComment);
                    return RedirectToAction(nameof(Index) ,new { urlHandle = blogDetailsViewModel.UrlHandle });
                }
            }
            return View(blogDetailsViewModel);
        }
    }
}
