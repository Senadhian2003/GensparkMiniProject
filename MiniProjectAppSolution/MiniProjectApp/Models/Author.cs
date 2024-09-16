using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models
{
    public class Author
    {

        [Key]
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }




    }
}
