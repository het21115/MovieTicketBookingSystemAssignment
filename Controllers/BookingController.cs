using MovieTicketBookingSystem.Models;
using System;
using System.Web.Mvc;

namespace MovieTicketBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        BookingDBHandle bookingDB = new BookingDBHandle();

        // GET: Booking/BookTicket/5
        // GET: Booking/BookTicket/5
        [HttpGet]
        public ActionResult BookTicket(int id)
        {
            BookingModel booking = bookingDB.GetMovieById(id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking);
        } // POST: Booking/BookTicket
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookTicket(BookingModel booking)
        {
            if (Session["User_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (ModelState.IsValid)
            {
                booking.User_id =
                    Convert.ToInt32(Session["User_id"]);

                booking.Booking_id =
                    bookingDB.GetNewBookingId();

                booking.Amount =
                    (int)(booking.Rate * booking.No_Of_Tickets);

                bool result =
                    bookingDB.AddBooking(booking);

                if (result)
                {
                    return RedirectToAction("AllBookings");
                }
            }

            return View(booking);
        }
        public ActionResult AllBookings()
        {
            if (Session["User_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            int userId =
                Convert.ToInt32(Session["User_id"]);

            var bookings =
                bookingDB.GetAllBookings(userId);

            return View(bookings);
        }

        // Booking successful
        public ActionResult BookingSuccess(int id)
        {
            ViewBag.BookingId = id;

            return View();
        }
    }
}