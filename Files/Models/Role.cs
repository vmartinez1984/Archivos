using System.ComponentModel.DataAnnotations;

namespace Files.Models
{
    public class Role: BaseEntity
    {
        [Required(ErrorMessage = "Es obligatorio el nombre")]
        [StringLength(20)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

    }
}
