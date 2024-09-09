using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Entities;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Demo.Pl.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public AdminTagsController(ITagRepository tagRepository , IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            string? searchQuery ,
            string? sortBy ,
            string ?sortDirection, 
            int pageSize = 3,
            int pageNumber = 1
            )
        {
            var tags = Enumerable.Empty<Tag>();
            var totalRecords = await _tagRepository.CountAsync();
            var totalPages = Math.Ceiling((decimal)totalRecords/pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.SortBy = sortBy;
            ViewBag.SortDirection = sortDirection;

            if (string.IsNullOrEmpty(searchQuery) == false)
            {
                tags = await _tagRepository.SearchByName(searchQuery);
            }

            if (string.IsNullOrEmpty(sortBy) == false)
            {
                if(sortDirection == "Asc")
                    tags = await _tagRepository.SortAsc(sortBy);
                else
                    tags = await _tagRepository.SortDesc(sortBy);
            }
            
            if(string.IsNullOrEmpty(searchQuery) && string.IsNullOrEmpty(sortBy))
                tags = await _tagRepository.GetAll();
            
            // Pagenation
            //var skipResults = (pageNumber - 1) * pageSize;
            //tags = tags.Skip(skipResults).Take(pageSize);

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tag = await _tagRepository.GetById(id);
            return View(tag);
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagViewModel addTagViewModel)
        {
            if (ModelState.IsValid)
            {
                // Mapping AddTagViewModel to Tag entity model
                //var tag = new Tag()
                //{
                //    Name = addTagViewModel.Name,
                //    DisplayName = addTagViewModel.DisplayName,
                //};
                var mappedTag = _mapper.Map<AddTagViewModel, Tag>(addTagViewModel);
                await _tagRepository.Add(mappedTag);
                return RedirectToAction(nameof(Index));
            }
            return View(addTagViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) 
        {
            var tag = await _tagRepository.GetById(id);
            if (tag != null)
            {
                //var editTagViewModel = new EditTagViewModel()
                //{
                //    Name = tag.Name,
                //    DisplayName = tag.DisplayName,
                //    Id = tag.Id
                //};
                var mappedTag = _mapper.Map<Tag, EditTagViewModel>(tag);
                return View(mappedTag);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm]Guid id , EditTagViewModel editTagViewModel)
        {
            if (id != editTagViewModel.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var mappedTag = _mapper.Map<EditTagViewModel, Tag>(editTagViewModel);
                    await _tagRepository.Update(mappedTag);
                    return RedirectToAction(nameof(Index));

                }catch(Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            return View(editTagViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tag = await _tagRepository.GetById(id);
            return View(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Tag tag)
        {
            if (tag != null)
            {
                await _tagRepository.Delete(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }
    }
}
