using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingSystem.Models
{
    public class BookingModel
    {
        public int Booking_id { get; set; }

        public int User_id { get; set; }

        public int Category_id { get; set; }

        public int Movie_id { get; set; }

        [Required(ErrorMessage = "Please enter number of tickets")]
        [Range(1, 10, ErrorMessage = "Tickets must be between 1 and 10")]
        public int No_Of_Tickets { get; set; }

        public int Amount { get; set; }

        public string Movie_name { get; set; }

        public decimal Rate { get; set; }
    }
}