using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models.DTO
{
    public class PayFineForOneBookDTO
    {

        [Required(ErrorMessage = "Fine id is required.")]
        public int FineId { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public int BookId { get; set; }
        

    }
}
