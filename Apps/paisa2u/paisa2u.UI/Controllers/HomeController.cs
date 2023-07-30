using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using paisa2u.common.Models;
using paisa2u.common.Resources;
using paisa2u.UI.Models;
using System.Diagnostics;
using System.Security.Policy;
using System.Text;

namespace paisa2u.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public async Task<ActionResult<ProductByVendor>> Index()
        {
            List<ProductByVendor> productByVendor = new List<ProductByVendor>();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            using (var response = await client.GetAsync("https://localhost:7172/api/Vendors/GetSingleProductByVendor"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                var a = JsonConvert.DeserializeObject<ProductByVendor[]>(apiResponse);
                return View("../Home/Index", a);
             

            }
        }
        public async Task<ActionResult> GetSearch(IFormCollection collection)
        {
            var select = collection["list1"]; //ID or All
            var option = collection["options"];
            //products of all vendors
            if (select == "0" && option == "Vendor" )
            {
                var client1 = new HttpClient();
                client1.DefaultRequestHeaders.Clear();
      
                using (var response = await client1.GetAsync("https://localhost:7172/api/Vendors/GetAllProductsOfAllVendor"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var a = JsonConvert.DeserializeObject<ProductByVendor[]>(apiResponse);
                    return View("../Shared/Vendor", a);
                }

            }
            //products of a vendor
            else if (select != "0" && option == "Vendor")
            {
                
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                var queryParams = new Dictionary<string, string>()
                {
                    {"vendorid", select }
                };
                string url = QueryHelpers.AddQueryString("https://localhost:7172/api/Vendors/GetAllProductsByVendor", queryParams);
                using (var response = await client.GetAsync(url))

                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var a = JsonConvert.DeserializeObject<ProductByVendor[]>(apiResponse);
                    return View("../Shared/Vendor", a);


                }
            }

            else if (select == "0" && option == "Product")
            {
                var client1 = new HttpClient();
                client1.DefaultRequestHeaders.Clear();

                using (var response = await client1.GetAsync("https://localhost:7172/api/Products/GetProducts"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var a = JsonConvert.DeserializeObject<ProductResource[]>(apiResponse);
                    return View("../Shared/Product", a);
                }

            }//Get product
            else if (select != "0" && option == "Product")
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                var queryParams = new Dictionary<string, string>()
                    {
                        {"id", select }
                    };
                string url = QueryHelpers.AddQueryString("https://localhost:7172/api/Products/GetProduct", queryParams);
                using (var response = await client.GetAsync(url))

                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var a = JsonConvert.DeserializeObject<ProductResource[]>(apiResponse);
                    return View("../Shared/Product", a);


                }
            }
            //products of all categories
            else if (select == "0" && option == "Category")
            {
                
                var client1 = new HttpClient();
                client1.DefaultRequestHeaders.Clear();
                using (var response = await client1.GetAsync("https://localhost:7172/api/categories/GetProductsForAllCategories"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var a = JsonConvert.DeserializeObject<ProductResource[]>(apiResponse);
                    return View("../Shared/Product", a);
                }
            }
            //products of single category
            else
            {
                
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                var queryParams = new Dictionary<string, string>()
                    {
                        {"catid", select }
                    };
                //https://localhost:7172/api/Categories/GetProductsForSingleCategories?catid=1
                string url = QueryHelpers.AddQueryString("https://localhost:7172/api/Categories/GetProductsForSingleCategory", queryParams);
                using (var response = await client.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var a = JsonConvert.DeserializeObject<ProductResource[]>(apiResponse);
                    return View("../Shared/Product", a);


                }
            }

        }
      

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}