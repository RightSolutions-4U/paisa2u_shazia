using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using paisa2u.common.Models;
using System.Collections.Generic;
using System.Text;

namespace paisa2u.UI.Controllers
{
    public class VendorController : Controller
    {
        public async Task<ActionResult<VendorList>> Index()
        {
            try
            {


                /*List<VendorList> Vendorlist = new List<VendorList>();
*/
                    VendorList Vendorlist = new VendorList();

                StringContent content = new StringContent(JsonConvert.SerializeObject(Vendorlist), Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                using (var response = await client.GetAsync("https://localhost:7172/api/Vendors/GetVendor"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var a = JsonConvert.DeserializeObject<VendorList[]>(apiResponse);
                    /*return View("../Home/Index", a);*/
                    return RedirectToAction(nameof(Index), a);
                }
                return View();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return View();
            }
            

            
        }
        public ActionResult Search(IFormCollection collection)
        {
            var a = collection["options"];

            return View("../Home/Index");
        }
    }
}
