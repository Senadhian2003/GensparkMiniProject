namespace MiniProjectApp.Models
{
    public class Rent
    {
        public int RentId { get; set; }
        public int UserId { get; set; }

        public string CartType { get; set; }

        public DateTime DateOfRent { get; set; }

        public DateTime DueDate { get; set;}

        public string Progress { get; set; }

        public double Amount { get; set; }


    }
}
