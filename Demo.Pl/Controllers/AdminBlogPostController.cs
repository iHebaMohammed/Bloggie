using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Entities;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.Pl.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IMapper _mapper;

        public AdminBlogPostController(
            ITagRepository tagRepository,
            IBlogPostRepository blogPostRepository,
            IMapper mapper
            )
        {
            _tagRepository=tagRepository;
            _blogPostRepository=blogPostRepository;
            _mapper=mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blogPosts = await _blogPostRepository.GetAll();
            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Get all tags from repository
            var tags = await _tagRepository.GetAll();

            var model = new AddBlogPostViewModel()
            {
                Tags = tags.Select(x => new SelectListItem 
                {
                    Text = x.DisplayName, 
                    Value = x.Id.ToString()
                }),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostViewModel addBlogPostViewModel)
        {
            if (addBlogPostViewModel != null)
            {
                var blogPost = new BlogPost
                {
                    Heading = addBlogPostViewModel.Heading,
                    PageTitle = addBlogPostViewModel.PageTitle,
                    Content = addBlogPostViewModel.Content,
                    ShortDescription = addBlogPostViewModel.ShortDescription,
                    FeaturedImageUrl = addBlogPostViewModel.FeaturedImageUrl,
                    UrlHandle = addBlogPostViewModel.UrlHandle,
                    PublishDate = addBlogPostViewModel.PublishDate,
                    Author = addBlogPostViewModel.Author,
                    IsVisble = addBlogPostViewModel.IsVisble,
                };
                // Map Tags from selected tags
                var selectedTags = new List<Tag>();
                foreach (var selectedTagId in addBlogPostViewModel.SelectedTags)
                {
                    var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                    var existingTag = await _tagRepository.GetById(selectedTagIdAsGuid);

                    if (existingTag != null)
                    {
                        selectedTags.Add(existingTag);
                    }
                }
                // Mapping tags back to domain model
                blogPost.Tags = selectedTags;
                await _blogPostRepository.Add(blogPost);
                return RedirectToAction(nameof(Index));
            }
            return View(addBlogPostViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var blogPost = await _blogPostRepository.GetById(id);
            if (blogPost != null) {
                var tagsDomainModel = await _tagRepository.GetAll();

                var model = new EditBlogPostViewModel()
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
                    UrlHandle = blogPost.UrlHandle,
                    Tags = tagsDomainModel.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray(),
                };
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // retrive the result from the repository
            var blogPost = await _blogPostRepository.GetById(id);

            if(blogPost != null)
            {
                // get all tags
                var tagsDomainModel = await _tagRepository.GetAll();

                var model = new EditBlogPostViewModel()
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
                    UrlHandle = blogPost.UrlHandle,
                    Tags = tagsDomainModel.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray(),
                };
                return View(model);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostViewModel editBlogPostVM)
        {
            if(editBlogPostVM != null)
            {
                var blogPostDomainModel = new BlogPost()
                {
                    Id = editBlogPostVM.Id,
                    Author = editBlogPostVM.Author,
                    Content = editBlogPostVM.Content,
                    FeaturedImageUrl= editBlogPostVM.FeaturedImageUrl,
                    Heading = editBlogPostVM.Heading,
                    IsVisble= editBlogPostVM.IsVisble,
                    PageTitle= editBlogPostVM.PageTitle,
                    PublishDate= editBlogPostVM.PublishDate,
                    ShortDescription= editBlogPostVM.ShortDescription,
                    UrlHandle= editBlogPostVM.UrlHandle,
                };

                var selectedTags = new List<Tag>();
                foreach (var selectedTag in editBlogPostVM.SelectedTags)
                {
                    if (Guid.TryParse(selectedTag, out var tag))
                    {
                        var foundTag = await _tagRepository.GetById(tag);
                        if (foundTag != null)
                        {
                            selectedTags.Add(foundTag);
                        }
                    }
                
                }
                blogPostDomainModel.Tags = selectedTags;
                var result = await _blogPostRepository.Update(blogPostDomainModel);
                return RedirectToAction(nameof(Index));
            }
            return View(editBlogPostVM);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var blogPost = await _blogPostRepository.GetById(id);
            if (blogPost != null) {
                var tagsDomainModel = await _tagRepository.GetAll();
                var model = new EditBlogPostViewModel()
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
                    UrlHandle = blogPost.UrlHandle,
                    Tags = tagsDomainModel.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray(),
                };
                return View(model);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostViewModel editBlogPostVM)
        {
            var blogPost = await _blogPostRepository.GetById(editBlogPostVM.Id);
            if (blogPost != null)
            {
                var result = _blogPostRepository.Delete(blogPost);
                return RedirectToAction(nameof(Index));
            }
            return View(editBlogPostVM);
        }
    }
}
