﻿using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models
{
    public class Book
    {

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }



    }
}
