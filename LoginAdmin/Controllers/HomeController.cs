using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LoginAdmin.Data;

namespace LoginAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new User().GetUsers());
        }

        public ActionResult About()
        {
            return View();

        }

        public ActionResult Contact()
        {

            return View();
        }
        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                throw new Exception("User id is not valid");
            }
            User user = new User(id);

            return View(user);
        }

        [HttpPost]
        public ActionResult LogIn(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Username and password cannot be null or empty");
            }

            try
            {
                User user = new User(email);

                if (user.Password == password)
                {
                    Session["userID"] = user.Id;
                    Session["userName"] = user.Name;
                    ViewBag.Success = "Login Succesfully";
                    return View(user);
                }
                else
                {
                    throw new Exception("Invalid Login");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string ComputeSha256Hash(object password)
        {
            throw new NotImplementedException();
        }


        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                throw new Exception("User id is not valid");
            }
            User user = new User(id);

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {
            if (ModelState.IsValid)
            {
                return View();
            }

            try
            {
                //get that user from db
                User user = new User();

                // update based on the info provided
                user.UpdateUser(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ViewBag.success = "User updated succesfully";
            return RedirectToAction("Index");
        }

        /*public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                throw new Exception("User id is not valid");
            }
            User user = new User(id);

            return View(user);
        }*/

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User model)
        {
            User user = new User();
            user.AddUser(model);
            return RedirectToAction("Index");
        }
       [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;
            if (id <= 0)
            {
                throw new Exception("User id is not valid");
            }

            try
            {
                User user = new User(id);

                result =  user.DeleteUser(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ViewBag.success = "User Deleted succesfully";
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}