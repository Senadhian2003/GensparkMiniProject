using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models.DTO
{
    public class AddNewPublisherDTO
    {

        [Required(ErrorMessage = "Name is required.")]
        public String PublisherName { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }




    }
}
