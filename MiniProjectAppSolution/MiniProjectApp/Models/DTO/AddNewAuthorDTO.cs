using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models.DTO
{
    public class AddNewAuthorDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public String AuthorName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }


    }
}
