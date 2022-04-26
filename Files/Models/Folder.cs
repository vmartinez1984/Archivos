using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
