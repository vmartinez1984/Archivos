using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Files.Models
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "{0} es requerido")]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [StringLength(100)]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [StringLength(50)]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [StringLength(50)]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required]
        //[NotMapped]
        //[StringLength(50)]
        //[Display(Name = "Contraseña")]
        //[DataType(DataType.Password)]
        //[Compare(nameof(Password))]
        //public string ConfirmPassword { get; set; }

        [Required]
        [ForeignKey(nameof(Role))]
        [Display(Name = "Rol")]
        public int RoleId { get; set; }

        [Display(Name = "Rol")]
        public virtual Role Role { get; set; }

        [NotMapped]
        [Display(Name = "Nombre")]
        public string FullName { get { return $"{Name} {LastName}"; } }
    }
}
