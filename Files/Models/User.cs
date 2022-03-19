using System.ComponentModel.DataAnnotations;

namespace Files.Models
{
    public class User : BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }
    }
}
