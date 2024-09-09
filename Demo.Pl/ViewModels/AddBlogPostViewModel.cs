using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.ViewModels
{
    public class AddBlogPostViewModel
    {
        [Required(ErrorMessage ="Required")]
        public string Heading { get; set; }

        
        [DisplayName("Page Title")]
        [Required(ErrorMessage = "Required")]
        public string PageTitle { get; set; }
        
        
        [Required(ErrorMessage = "Required")]
        //[MinLength(50 , ErrorMessage ="Should be at lest 50 characters")]
        public string Content { get; set; }
        
        [Required(ErrorMessage = "Required")]
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Featured Image Url")]
        public string FeaturedImageUrl { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Url Handle")]
        public string UrlHandle { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Publish Date")]
        public DateTime PublishDate { get; set; }


        [Required(ErrorMessage = "Required")]
        public string Author { get; set; }

        [DisplayName("Is Visible ?")]
        public bool IsVisble { get; set; }


        // Display the tags:
        public IEnumerable<SelectListItem> Tags { get; set; }

        // collect tags
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
