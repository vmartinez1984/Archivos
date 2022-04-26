using System.ComponentModel.DataAnnotations;

namespace Files.Dtos.Inputs
{
    public class UserLogin
    {
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(50)]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(50)]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
