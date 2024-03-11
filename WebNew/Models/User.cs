
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebNew.Models
{
    [Table("User")]
    public class User
    {
        [Required]

        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public int Age { get; set; }
        [Required]

        public string Mail { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]

        public Rolles Rolles { get; set; }

        public User(int id, string name, int age, string mail, string password, Rolles rolles)
        {
            Id = id;
            Name = name;
            Age = age;
            Mail = mail;
            Password = password;
            Rolles = rolles;
        }
        public User() { }

    }
}
