using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using paisa2u.common.Models;
using System.Net.Http;
using System.Text;

namespace paisa2u.UI.Controllers
{
    public class UsersController : Controller
    {
        // GET: UsersController
        public ActionResult Login(int id)
        {
            return View("Login");
        }
        [HttpPost]
        public async Task<ActionResult<Users>> CheckLogin(IFormCollection collection)
        {
            //Code to check valid user
            //On valid user it will go to home screen
            Users user = new Users();
            user.Email = collection["gr_login_email"];
            user.Pwd = collection["gr_login_password"];
            

            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            using (var response = await client.PostAsync("https://localhost:7172/api/Users/Login", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                
                var a = JsonConvert.DeserializeObject<Users>(apiResponse);
                if (Request.Cookies["RegId"] == null)
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(50);
                    option.IsEssential = true;
                    Response.Cookies.Append("userid", a.RegId.ToString(), option);
                    
                }
                //changed username to Email by Shazia Jul 31, 2023 and added TempData
                if(a.Email == null)
                {
                    ViewBag.message = "Invalid Email or Password";
                }
                else
                {
                    ViewBag.message = "Welcome " + a.Username;
                    TempData["RegId"] = a.RegId;
                    TempData["Username"] = a.Username;
                 

                }
                
                return View("../Home/Index");
               /* return View("/Home/Index", ViewBag);*/
            }
            //Store in cookies
            
           /* return View("Index");*/
           
        }

        public ActionResult Register(int id)
        {
            return View("Register");
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
