using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Files.Models
{
    public class Archive : BaseEntity
    {        
        [StringLength(150)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Archivo")]
        public string Base64 { get; set; }

        [StringLength(10)]
        public string Extension { get; set; }

        [ForeignKey(nameof(Folder))]
        [Display(Name = "Carpeta")]
        [Required(ErrorMessage = "Seleccione una carpeta")]
        public int FolderId { get; set; }
        public Folder Folder { get; set; }

        [Required]
        [Display(Name = "Solo lectura")]
        public bool OnlyRead { get; set; }

        [Required(ErrorMessage = "Agregue un archivo")]
        [NotMapped]
        [Display(Name = "Archivo")]
        public IFormFile FormFile { get; set; }
     
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
