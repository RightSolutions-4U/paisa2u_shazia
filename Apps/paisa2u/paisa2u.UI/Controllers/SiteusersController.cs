using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using paisa2u.common.Models;
using paisa2u.common.Resources;
using System.Text;

namespace paisa2u.UI.Controllers
{
    public class SiteusersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<UserResource>> CheckLogin(IFormCollection collection)
        {
            //Code to check valid user
            //On valid user it will go to home screen
            Siteuser siteuser = new Siteuser();
            siteuser.Email = collection["email"];
            siteuser.Pwd = collection["pwd"];


            StringContent content = new StringContent(JsonConvert.SerializeObject(siteuser), Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            using (var response = await client.PostAsync("https://localhost:7172/api/Siteusers/Login", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                var a = JsonConvert.DeserializeObject<Siteuser>(apiResponse);
                if (Request.Cookies["Siteuserid"] == null)
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(50);
                    option.IsEssential = true;
                    Response.Cookies.Append("Siteuserid", a.Userid.ToString(), option);

                }
                //changed username to Email by Shazia Jul 31, 2023 and added TempData
                if (a.Email == null)
                {
                    ViewBag.welcome = "Invalid Email or Password";
                }
                else
                {
                    ViewBag.welcome = "Welcome " + a.Username;
                    
                    TempData["Username"] = a.Username;
                }

                return View("AdminLogin");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Users>> RegisterUser(IFormCollection collection/*, string uri*/)
        {
            //create / register
            Users user = new Users();
            user.Email = collection["gr_register_Email"];
            user.Username = collection["gr_register_Username"];
            user.Pwd = collection["gr_register_Password"];
            user.Firstname = collection["gr_register_Firstname"];
            user.Middlename = collection["gr_register_Middlename"];
            user.Lastname = collection["gr_register_Lastname"];
            user.Phonenumber = collection["gr_register_phonenumber"];
            user.Substype = collection["Substype"];
            user.Regtype = collection["Regtype"];
            user.Referredby = collection["gr_register_Referredby"];
            user.Vendortype = collection["Vendortype"];
            var Autorenewal = collection["Autorenewal"];
            var Regstatus = collection["Regstatus"];
            user.Endate = DateTime.Now;
            user.Enuser = collection["gr_register_Firstname"];
            user.PasswordHash = "234";
            user.PasswordSalt = "234";
            user.Qrpicture = "123";

            if (Autorenewal == "N")
            { user.Autorenewal = false; }
            else
            { user.Autorenewal = true; }
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

    }
}
