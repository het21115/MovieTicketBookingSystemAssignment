using MovieTicketBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace MovieTicketBookingSystem.Controllers
{
    public class UserController : Controller
    {
        MovieCategoryDBHandle categoryDB = new MovieCategoryDBHandle();
        UserDBHandle userDB = new UserDBHandle();

        MovieDBHandle movieDB = new MovieDBHandle();

        // GET: MovieCategory
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                UserModel user = userDB.LoginUser(login);

                if (user != null)
                {
                    Session["User_id"] = user.User_id;
                    Session["User_name"] = user.User_name;
                    Session["Email_id"] = user.Email_id;

                    return RedirectToAction("DisplayMovies", "Movie");
                }
                ViewData["Message"] = "Invalid Email ID or Password";
            }

            return View(login);
        }
        [HttpGet]
        public ActionResult UserProfile()
        {
            if (Session["User_id"] == null)
            {
                return RedirectToAction("Login");
            }

            int userId = Convert.ToInt32(Session["User_id"]);

            UserModel user = userDB.GetUserById(userId);

            return View(user);
        }
        [HttpGet]
        public ActionResult DeleteMovie(int id)
        {
            movieDB.DeleteMovie(id);

            return RedirectToAction("DisplayMovies");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfile(UserModel user)
        {
            if (Session["User_id"] == null)
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                userDB.UpdateUser(user);

                Session["User_name"] = user.User_name;
                Session["Email_id"] = user.Email_id;

                ViewBag.Message = "Profile updated successfully.";
            }

            return View(user);
        }
        // GET: MovieCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieCategory category)
        {
            if (ModelState.IsValid)
            {
                if (categoryDB.AddCategory(category))
                {
                    return RedirectToAction("Display");
                }
            }

            return View(category);
        }

        // GET: MovieCategory/Edit/5
        public ActionResult Edit(int id)
        {
            MovieCategory category = categoryDB.GetCategoryById(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: MovieCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieCategory category)
        {
            if (ModelState.IsValid)
            {
                if (categoryDB.UpdateCategory(category))
                {
                    return RedirectToAction("Display");
                }
            }

            return View(category);
        }

        // GET: MovieCategory/Delete/5
        public ActionResult Delete(int id)
        {
            MovieCategory category = categoryDB.GetCategoryById(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: MovieCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (categoryDB.DeleteCategory(id))
            {
                return RedirectToAction("Display");
            }

            return View();
        }
    }
}