﻿using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Status { get; set; }

        public ICollection<SuperCart>? SuperCartItems { get; set; }



    }
}
