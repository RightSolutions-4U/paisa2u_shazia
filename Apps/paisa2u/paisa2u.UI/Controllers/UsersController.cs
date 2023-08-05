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
                    ViewBag.welcome = "Invalid Email or Password";
                }
                else
                {
                    ViewBag.welcome = "Welcome " + a.Username;
                    TempData["RegId"] = a.RegId;
                    TempData["Username"] = a.Username;
                }
                
                return View("../Home/Index");
            }
        }
        //Added by Shazia Aug 4, 2023 for registration
        public ActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        public async Task<ActionResult<Users>> RegisterUser(IFormCollection collection/*, string uri*/)
        {
            string uri = "http://example.com/file?a=3&b=2&c=string%20param";
            string[] parts = uri.Split(new char[] { '?', '&' });
            ///parts[0] now contains http://example.com/file
            ///parts[1] = "a=1"
            ///parts[2] = "b=2"
            ///parts[3] = "c=string%20param"
            int part1 = parts[1].IndexOf("=");
            string referredby = parts[1].Substring(part1+1);
            //creeate / register
            Users user = new Users();
            user.Email = collection["gr_register_Email"];
            user.Username = collection["gr_register_Username"];
            user.Pwd = collection["gr_register_Password"];
            user.Firstname = collection["gr_register_Firstname"];
            user.Middlename =  collection["gr_register_Middlename"];
            user.Lastname = collection["gr_register_Lastname"];
            user.Phonenumber = collection["gr_register_phonenumber"];
            user.Substype = collection["Substype"];
            user.Regtype = collection["Regtype"];
            user.Referredby = referredby;
            user.Vendortype = collection["Vendortype"];
            var Autorenewal = collection["Autorenewal"];
            var Regstatus = collection["Regstatus"];
            user.Endate = DateTime.Now;
            user.Enuser = collection["gr_register_Firstname"];
            user.PasswordHash = "234";
            user.PasswordSalt = "234";
            user.Qrpicture = "123";

            if (Autorenewal == "N")
            {   user.Autorenewal = false; }
            else
            {   user.Autorenewal = true; }
            if (Regstatus == "N")
            { user.Regstatus = false; }
            else
            { user.Regstatus = true; }
            
            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            using (var response = await client.PostAsync("https://localhost:7172/api/Users/Register", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                var a = JsonConvert.DeserializeObject<Users>(apiResponse);
                if (a.Email == null)
                {
                    ViewBag.message = "Invalid Email or Password";
                }
                else
                {
                    ViewBag.message = "Registered Successfully " + a.Username;
                    TempData["RegId"] = a.RegId;
                    TempData["Username"] = a.Username;
                }
            }
            
            return View("../Home/Index");
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
