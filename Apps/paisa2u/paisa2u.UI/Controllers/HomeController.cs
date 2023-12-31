﻿using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using NuGet.Protocol;
using paisa2u.common.Models;
using paisa2u.common.Resources;
using paisa2u.UI.Models;
using System;
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

        //Aug 23, withdraw
        public async Task<ActionResult<RegUserResource>> Withdrawals()
        {
            
            var client1 = new HttpClient();
            client1.DefaultRequestHeaders.Clear();
            var queryParams = new Dictionary<string, string>()
                {
                    {"Regid",Request.Cookies["regid"] }
                };
            string url = QueryHelpers.AddQueryString("https://localhost:7172/api/Transactions/GetWallet", queryParams);

            string urlcust = QueryHelpers.AddQueryString("https://localhost:7172/api/Users/GetRegUser", queryParams);
            using (var response1 = await client1.GetAsync(urlcust))
            {
                string apiResponsecust = await response1.Content.ReadAsStringAsync();
                var a = JsonConvert.DeserializeObject<RegUserResource>(apiResponsecust);
                using (var response = await client1.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Amount = apiResponse;

                }
                ViewBag.User = Request.Cookies["username"];
                return View("../Shared/Withdrawal", a);

            }
            
            
        }

        //by Shazia on Aug 2, 2023
        //GetMyReferrals
        public async Task<ActionResult> GetMyReferrals(IFormCollection collection)
        {
                var client1 = new HttpClient();
                client1.DefaultRequestHeaders.Clear();
            //TempData["RegId"].ToString()
                var queryParams = new Dictionary<string, string>()
                {
                    {"Regid",Request.Cookies["regid"] }
                };
                 string url = QueryHelpers.AddQueryString("https://localhost:7172/api/Users/GetAllReferralsByRegid", queryParams);
                 using (var response = await client1.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var a = JsonConvert.DeserializeObject<RegUserRegisterResource[]>(apiResponse);
                    return View("../Shared/Referrals", a);
                }
        }

        //by shazia on aug 14
        //Added by Shazia Aug 14, 2023 for invite
        public ActionResult Invite()
        {
            return View("SendToAFriend");
        }
        //by Shazia on Aug 2, 2023
        //GetTransactionsWithRegId
        public async Task<ActionResult> GetTransactionsWithRegId(IFormCollection collection)
        {
            var client1 = new HttpClient();
            client1.DefaultRequestHeaders.Clear();
            //TempData["RegId"].ToString()
            var queryParams = new Dictionary<string, string>()
                {
                    {"Regid",Request.Cookies["regid"] }
                };
            string url = QueryHelpers.AddQueryString("https://localhost:7172/api/Transactions/GetTransactionsWithRegId", queryParams);
            using (var response = await client1.GetAsync(url))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var a = JsonConvert.DeserializeObject<TransactionsResource[]>(apiResponse);
                return View("../Shared/Transactions", a);
            }
        }
        //by Shazia on Aug 2, 2023
        //GetWallet
        public async Task<ActionResult> GetWallet(IFormCollection collection)
        {
            var client1 = new HttpClient();
            client1.DefaultRequestHeaders.Clear();
            TempData["RegId"].ToString();
            var queryParams = new Dictionary<string, string>()
                {
                    {"Regid",Request.Cookies["regid"] }
                };
               
            string url = QueryHelpers.AddQueryString("https://localhost:7172/api/Transactions/GetWallet", queryParams);
            using (var response = await client1.GetAsync(url))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                ViewBag.Amount = apiResponse;
                return View("../Shared/Wallet");
            }
        }
        //by Shazia on Jul 29-Jul 31, 2023
        public async Task<IActionResult> GetSearch(IFormCollection collection)
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

        public async Task<IActionResult> GetAllproductbyavendor(string vendorid)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            var queryParams = new Dictionary<string, string>()
                {
                    {"vendorid", vendorid }
                };
            string url = QueryHelpers.AddQueryString("https://localhost:7172/api/Vendors/GetAllProductsByVendor", queryParams);
            using (var response = await client.GetAsync(url))

            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                var a = JsonConvert.DeserializeObject<ProductByVendor[]>(apiResponse);
                return View("../Shared/Vendor", a);


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