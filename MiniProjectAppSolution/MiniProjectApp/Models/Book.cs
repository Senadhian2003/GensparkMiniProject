using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjectApp.Models
{
    public class Book
    {

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        //public string Author { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]

        public Author Author { get; set; }

        public int PublisherId {  get; set; }

        [ForeignKey(nameof(PublisherId))]

        public Publisher Publisher { get; set; }

        public string Category { get; set; }

        public double AvgRating { get; set; }

        public int RatingCount { get; set; }

        public byte[]? Image { get; set; }



    }
}
