using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjectApp.Models.DTO
{
    public class AddNewBookDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        //public string Author { get; set; }
        [Required(ErrorMessage = "Author id is required.")]
        public int AuthorId { get; set; }


        [Required(ErrorMessage = "Publisher id is required.")]
        public int PublisherId { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

        public IFormFile BookImage { get; set; }




    }
}
