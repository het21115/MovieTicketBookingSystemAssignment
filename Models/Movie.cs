using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieTicketBookingSystem.Models
{
    public class Movie
    {
        public int Movie_id { get; set; }

        public string Movie_name { get; set; }

        public DateTime Release_date { get; set; }

        public int Category_id { get; set; }

        public decimal Rate { get; set; }
    }
}