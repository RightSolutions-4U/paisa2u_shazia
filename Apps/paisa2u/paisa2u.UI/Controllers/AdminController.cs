using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using paisa2u.common.Models;
using paisa2u.common.Resources;
using System.Runtime.InteropServices;
using System.Text;

namespace paisa2u.UI.Controllers
{
  
    public class AdminController : Controller
    {
       
        public IActionResult Index()
        {
            return View("Admin");
        }
        //[HttpGet("GetSubsPercentAll")]
        //public async Task<ActionResult<RegUserResource>> GetSubsPercentAll()
        //{
        //    List<RegUserResource> regUserResource = new List<RegUserResource>();
        //    try
        //    {
        //        var client = new HttpClient();
        //        client.DefaultRequestHeaders.Clear();
        //        StringContent content = new StringContent(JsonConvert.SerializeObject(regUserResource), Encoding.UTF8, "application/json");
        //        using (var response = await client.PostAsync("https://localhost:7172/api/Subscriptionpercs/GetSubsPercentAll", content))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            var a = JsonConvert.DeserializeObject<RegUserResource[]>(apiResponse);
        //            ViewBag.Admin = "Registered Users not having subscription%";
        //            return View("SubsPercentUpdDel", a);
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.Admin = "No records exist";
        //        return View("Admin");
        //    }

        //}

        [HttpGet("GetSubsPercentAllSubs")]
        public async Task<ActionResult<SubscriptionResource>> GetSubsPercentAllSubs()
        {
            List<SubscriptionResource> subscriptionResources = new List<SubscriptionResource>();
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                StringContent content = new StringContent(JsonConvert.SerializeObject(subscriptionResources), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync("https://localhost:7172/api/Subscriptionpercs/GetSubsPercentAllSubs", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var a = JsonConvert.DeserializeObject<SubscriptionResource[]>(apiResponse);
                    ViewBag.Admin = "subscription% does not exist";
                    return View("SubsPercentUpdDel", a);
                }

            }
            catch (Exception e)
            {
                ViewBag.Admin = "No records exist";
                return View("Admin");
            }

        }

        //[HttpGet("GetSubsPercentByRegId")]
        //public async Task<ActionResult<SubscriptionResource>> GetSubsPercentByRegId(int RegId, CancellationToken cancellationtoken)
        //{
        //    try
        //    {
        //        var client1 = new HttpClient();
        //        client1.DefaultRequestHeaders.Clear();
        //        TempData["RegId"].ToString();
        //        var queryParams = new Dictionary<string, string>()
        //        {
        //            {"Regid","6" }
        //        };
        //        string url = QueryHelpers.AddQueryString("https://localhost:7172/api/Subscriptionpercs/GetSubsPercent", queryParams);
        //        using (var response = await client1.GetAsync(url))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            var a = JsonConvert.DeserializeObject<SubscriptionResource[]>(apiResponse);
        //            return View("Admin/SubscriptionPercent", a);
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.Admin = "Invalid UserId or Password with ENV";
        //        return View("Admin/SubscriptionPercent");
        //    }

        //}
        //[HttpGet("AddSubsPercentview")]
        //public ActionResult<SubscriptionResource> AddSubsPercentview(int Regid)
        //{
        //    Subscriptionperc subscriptionperc = new Subscriptionperc();
        //    subscriptionperc.Endate = DateTime.Now;
        //    subscriptionperc.Enuser = "Shazia";
        //    subscriptionperc.RegId = 6;
        //    return View("AddSubsPercent", subscriptionperc);
        //}
        //[HttpGet("AddSubsPercent")]
        //public async Task<ActionResult<SubscriptionResource>> AddSubsPercent(IFormCollection collection)
        //{
        //    try
        //    {

        //        Subscriptionperc subscriptionperc = new Subscriptionperc();
        //        subscriptionperc.Endate = DateTime.Now;
        //        subscriptionperc.Enuser = "Shazia";/*TempData["Username"];*/
        //        var Appowner = collection["Appowner"];
        //        var Vendor = collection["Vendor"];
        //        var Customer = collection["Customer"];
        //        var Subvendor = collection["Subvendor"];
        //        subscriptionperc.Appowner = float.Parse(Appowner);
        //        subscriptionperc.Vendor = float.Parse(Vendor);
        //        subscriptionperc.Customer = float.Parse(Customer);
        //        subscriptionperc.Subvendor = float.Parse(Subvendor);

        //        StringContent content = new StringContent(JsonConvert.SerializeObject(subscriptionperc), Encoding.UTF8, "application/json");
        //        var client = new HttpClient();
        //        client.DefaultRequestHeaders.Clear();
        //        using (var response = await client.PostAsync("https://localhost:7172/api/Subscriptionpercs/AddSubsPercent", content))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();

        //            var a = JsonConvert.DeserializeObject<Subscriptionperc>(apiResponse);
        //            if (a.RegId == null)
        //            {
        //                ViewBag.message = "Record was not added successfully";
        //            }
        //            else
        //            {
        //                ViewBag.message = "Record Added Successfully";

        //            }
        //        }
        //        return NoContent();
        //        //return View("../Home/Index");

        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.Admin = "Invalid UserId or Password with ENV";
        //        return View("../Home/Index");
        //    }

        //}

    }//class
}//namespace
