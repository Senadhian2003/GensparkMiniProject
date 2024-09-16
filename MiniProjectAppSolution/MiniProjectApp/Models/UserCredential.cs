using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MiniProjectApp.Models
{
    public class UserCredential
    {
       
        [Key]
        public int UserId { get; set; }

       
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }  

        public byte[] Password { get; set; }

        public byte[] HashKey { get; set; }

     
    }
}
