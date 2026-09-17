using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MovieTicketBookingSystem.Models
{
    public class BookingDBHandle
    {
        private SqlConnection con;

        private void Connection()
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings[
                    "MovieTicketBookingSystem"
                ].ConnectionString;

            con = new SqlConnection(connectionString);
        }

        // Get selected movie
        public BookingModel GetMovieById(int id)
        {
            Connection();

            string query =
                "SELECT Movie_id, Movie_name, Category_id, Rate " +
                "FROM Tbl_Movie WHERE Movie_id = @Movie_id";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Movie_id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dt.Rows[0];

            BookingModel booking = new BookingModel();

            booking.Movie_id =
                Convert.ToInt32(row["Movie_id"]);

            booking.Movie_name =
                row["Movie_name"].ToString();

            booking.Category_id =
                Convert.ToInt32(row["Category_id"]);

            booking.Rate =
                Convert.ToDecimal(row["Rate"]);

            return booking;
        }

        // Add booking
        public bool AddBooking(BookingModel booking)
        {
            Connection();

            string query =
                "INSERT INTO tbl_Booking " +
                "(Booking_id, User_id, Category_id, Movie_id, No_Of_Tickets, Amount) " +
                "VALUES (@Booking_id, @User_id, @Category_id, @Movie_id, @No_Of_Tickets, @Amount)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@Booking_id",
                booking.Booking_id
            );

            cmd.Parameters.AddWithValue(
                "@User_id",
                booking.User_id
            );

            cmd.Parameters.AddWithValue(
                "@Category_id",
                booking.Category_id
            );

            cmd.Parameters.AddWithValue(
                "@Movie_id",
                booking.Movie_id
            );

            cmd.Parameters.AddWithValue(
                "@No_Of_Tickets",
                booking.No_Of_Tickets
            );

            cmd.Parameters.AddWithValue(
                "@Amount",
                booking.Amount
            );

            con.Open();

            int result = cmd.ExecuteNonQuery();

            con.Close();

            return result > 0;
        }

        // Generate Booking ID
        public int GetNewBookingId()
        {
            Connection();

            string query =
                "SELECT ISNULL(MAX(Booking_id), 0) + 1 FROM tbl_Booking";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            int newId = Convert.ToInt32(cmd.ExecuteScalar());

            con.Close();

            return newId;
        }
        public List<BookingModel> GetAllBookings(int userId)
        {
            Connection();

            List<BookingModel> bookings = new List<BookingModel>();

            string query =
                "SELECT b.Booking_id, b.User_id, b.Category_id, b.Movie_id, " +
                "b.No_Of_Tickets, b.Amount, m.Movie_name, m.Rate " +
                "FROM tbl_Booking b " +
                "INNER JOIN Tbl_Movie m ON b.Movie_id = m.Movie_id " +
                "WHERE b.User_id = @User_id";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@User_id", userId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                BookingModel booking = new BookingModel();

                booking.Booking_id =
                    Convert.ToInt32(row["Booking_id"]);

                booking.User_id =
                    Convert.ToInt32(row["User_id"]);

                booking.Category_id =
                    Convert.ToInt32(row["Category_id"]);

                booking.Movie_id =
                    Convert.ToInt32(row["Movie_id"]);

                booking.No_Of_Tickets =
                    Convert.ToInt32(row["No_Of_Tickets"]);

                booking.Amount =
                    Convert.ToInt32(row["Amount"]);

                booking.Movie_name =
                    row["Movie_name"].ToString();

                booking.Rate =
                    Convert.ToDecimal(row["Rate"]);

                bookings.Add(booking);
            }

            return bookings;
        }
    }
}