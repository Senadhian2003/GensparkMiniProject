using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        public string PublisherName { get; set; }

        public string City { get; set;}

        public string State { get; set;}
        public string Country { get; set;}



    }
}
