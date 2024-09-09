using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Demo.Pl.ViewModels
{
    public class EditTagViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
    }
}
