using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Files.Models
{
    public class Folder : BaseEntity
    {
        [Required(ErrorMessage = "Agregue un nombre")]
        [Display(Name = "Nombre")]
        [StringLength(255)]
        public string Name { get; set; }

        public int? FolderId { get; set; }

        public List<Archive> ListArchives { get; set; }
    }
}
