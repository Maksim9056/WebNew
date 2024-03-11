using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebNew.Models
{
    [Table("Rolles")]
    public class Rolles
    {
        [Required]

        public int Id { get; set; }
        [Required]

        public string NameRolles { get; set; }
        public Rolles(int id, string nameRolles)
        {
            Id = id;
            NameRolles = nameRolles;
        }
        public Rolles()
        {

        }
    }
}
