using E_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Controllers
{
    public class LoginsController : Controller
    {
        private readonly IAccount _account;

        public LoginsController(IAccount account)
        {
            _account=account;


        }

        public IActionResult Index(string username, string password)
        {
         

            if (username != null || password != null)
            {
                var user = _account.getuserByname(username);
                if (user == null)
                {
                    ViewBag.message = "invalid credentials,try again";

                }
                if (username.Equals("admin") && password.Equals("admin"))
                {
                    HttpContext.Session.SetString("UserName", username);
                    ViewBag.message = "login successfull";
                    return RedirectToAction("AdminDash", "LendRequests");

                }
                else if (username.Equals(user.UserName) && password.Equals(user.Password))

                {
                    HttpContext.Session.SetString("UserName", username);
                    return RedirectToAction("UserDash", "Books");


                }
                else
                {
                    ViewBag.message = "invalid credentials,try again";

                }
            }

            return View();
        }
    }
}
