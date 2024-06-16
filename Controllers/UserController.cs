using Microsoft.AspNetCore.Mvc;
using PagelightPrime_BunyanSamuel_WebApp.Contracts;
using PagelightPrime_BunyanSamuel_WebApp.Models;

namespace PagelightPrime_BunyanSamuel_WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserContract _userContract;

        public UserController(IUserContract userContract) {
        
            _userContract = userContract;        
        }

        public IActionResult Index()
        {
            return View();
        }
       
        public JsonResult ReadUsers()
        {
            var users = _userContract.ReadAllUser();

            return Json(users);
        }

        [HttpPost]
        public JsonResult CreateUser(User model)
        {
            if (ModelState.IsValid)
            {
                _userContract.CreateUser(model);
                return Json("User details created!");
            }
            else
            {
                return Json("Fill the required field!");
            }
        }

        [HttpGet]
        public JsonResult EditUser(int Id)
        {
            var user = _userContract.FindUserById(Id);

            if (user==null)
            {
                return Json(null);
            }
            return Json(user);
        }

        [HttpPost]
        public JsonResult UpdateUser(User user)
        {
            if (ModelState.IsValid)
            {
                _userContract.UpdateUser(user);
                

                return Json("User Details Updated.");
            }
         
            
            return Json("User Validation Failed.");
        }

        [HttpPost]
        public JsonResult DeleteUser(int Id)
        {
            var user = _userContract.FindUserById(Id);

            if (user != null)
            {
                _userContract.DeleteUser(Id);
                return Json("User Detail Deleted");
            }
            return Json("User Id Not Found");

        }
    }
}
