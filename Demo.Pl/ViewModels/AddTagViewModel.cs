using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.ViewModels
{
    public class AddTagViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
    }
}
