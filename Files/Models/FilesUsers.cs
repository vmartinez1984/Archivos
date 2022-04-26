using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Files.Models
{
    public class FilesUsers: BaseEntity
    {
        [Required]
        [ForeignKey(nameof(Files.Models.Archive))]
        public int ArchiveId { get; set; }

        public virtual Archive Archive { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual List<User> ListUsers { get; set; }
    }
}
