using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using paisa2u.common.Models;
using paisa2u.common.Resources;
using paisa2u.common.Services;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;

namespace paisa2u.UI.Controllers
{
    public class UsersController : Controller
    {
        //private readonly UserManager<Users> _userManager;
        //private readonly SignInManager<Users> _signInManager;
        //private readonly IEmailSender _emailSender;
        //public UsersController(UserManager<Users> userManager, SignInManager<Users> signInManager, IEmailSender emailSender)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _emailSender = emailSender;
        //}
        // GET: UsersController
        public ActionResult Login(int id)
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<ActionResult<Users>> ForgotPassword(IFormCollection collection)
        {
            Users user = new Users();
            var email = collection["gr_register_Email"];
            user.Email = email;
            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            using (var response = await client.PostAsync("https://localhost:7172/api/Users/Login_check_email", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                var a = JsonConvert.DeserializeObject<Users>(apiResponse);
                return RedirectToAction(nameof(ForgotPasswordSendEmail));

            }
            
        }
        [HttpGet]
        public IActionResult ResestPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                return View();
            }
            return View();
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
                if (a.Email == null)
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
        [HttpPost]
        public async Task<IActionResult> ResetThePassword(IFormCollection collection)
        {
            //Code to check valid user
            //On valid user it will go to home screen
            var Password = collection["Password"];
            var confirm_Password = collection["Confirmpassword"];
            if (Password == confirm_Password)
            {
                Users user = new Users();
                user.Email = collection["email"];
                user.Pwd = "abc";//required by login resource but not used
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                using (var response = await client.PostAsync("https://localhost:7172/api/Users/Login_check_email", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var salt = PasswordHasher.GenerateSalt();
                    var pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
                    var iteration = 3;
                    var PasswordHash = PasswordHasher.ComputeHash(Password, salt, pepper, iteration);
                    var a = JsonConvert.DeserializeObject<Users>(apiResponse);
                    var RegUser = new RegUserResource(
                    a.RegId,
                    a.Firstname,
                    a.Middlename,
                    a.Lastname,
                    a.Email,
                    a.Username,
                    "",
                    a.Referredby,
                    a.Regtype,
                    a.Vendortype,
                    a.Phonenumber,
                    a.Endate,
                    a.Enuser,
                    a.Substype,
                    a.Regstatus,
                    a.Autorenewal,
                    a.Qrpicture,
                    salt,
                    PasswordHash
                    );
                    var client2 = new HttpClient();
                    client2.DefaultRequestHeaders.Clear();
                    StringContent content2 = new StringContent(JsonConvert.SerializeObject(RegUser), Encoding.UTF8, "application/json");
                    using (var response_update = await client2.PostAsync("https://localhost:7172/api/Users/UpdateRegUser", content2))
                    {
                        string apiResponse_update = await response_update.Content.ReadAsStringAsync();
                        var a_update = JsonConvert.DeserializeObject<RegUserResource>(apiResponse);
                    }

                }
                return View("ResetPasswordConfirmation");
                

            }
            return View("ForgotPassword"); //for checking
        }
        //Added by Shazia Aug 14, 2023 for registration
        public ActionResult Register()
        {
            return View("Register");
        }
     
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }
       
        //public ActionResult Register(string email)
        //{
        //     return View("Register");
        //}

        [HttpGet]
        public ActionResult ResetPassword(string email)
        {
            ViewBag.email = email;
            return View("ResetPassword");
        }

        //created by Shazia on Aug 14, 2023 for resetting and forget password
        [HttpPost]
        public ActionResult SendtoAfriend(IFormCollection collection)
        {
            var sendto = collection["email"];
            MailMessage message = new MailMessage();

            message.From = new MailAddress("smalikbudhwani@gmail.com");
            message.To.Add(sendto);
            message.Bcc.Add("mohtashim1974@outlook.com");
            message.Subject = "Register";
            message.Body = "https://localhost:7257/Users/Register?RegId=" + TempData["RegId"];

            var client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587 /*465*/,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(message.From.ToString(), "yvfqhwixtvavufzj")
            };
            client.Send(message);
            return View("InviteConfirmation");
        }

       
       //created by Shazia on Aug 6, 2023 for resetting and forget password
        [HttpPost]
        public ActionResult ForgotPasswordSendEmail(IFormCollection collection)
        {
            var forgotemail = collection["gr_register_Email"];
            MailMessage message = new MailMessage();

            message.From = new MailAddress("smalikbudhwani@gmail.com");
            message.To.Add(forgotemail);
            message.Bcc.Add("mohtashim1974@outlook.com");
            message.Subject = "testing forgot password";
            message.Body = "https://localhost:7257/Users/ResetPassword?email="+ forgotemail;

            var client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587 /*465*/,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(message.From.ToString(), "yvfqhwixtvavufzj")
            };
            client.Send(message);
            //TempData["forgotemail"] = forgotemail;
            return View("ForgotPasswordConfirmation");
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
            user.Middlename =  collection["gr_register_Middlename"];
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
            
        
        
    }
}
