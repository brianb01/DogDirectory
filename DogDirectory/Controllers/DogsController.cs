using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DogDirectory.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DogDirectory.Controllers
{
    public class DogsController : Controller
    {

        private class DogApiSettings {

            public static Uri BaseAddress() {
                return new Uri("https://dog.ceo/");
            }

            public static string BreedListURL()
            {
                return "api/breeds/list";
            }

            public static string BreedRandomImageURL(string breedName)
            {
                return string.Format("api/breed/{0}/images/random", breedName);
            }

            public static string MissingImageURL()
            {
                return "http://cdn.smosh.com/sites/default/files/ftpuploads/bloguploads/1113/lost-pet-doge.jpg";
            }

        } 


        public static async Task<BreedsList> GetBreedsFromApiAsync()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = DogApiSettings.BaseAddress();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage Res = await client.GetAsync(DogApiSettings.BreedListURL());

                    if (Res.IsSuccessStatusCode)
                    {
                        var ApiResponse = await Res.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<BreedsList>(ApiResponse);
                    }
                    else {
                        return new BreedsList { Status = "failed", Breeds = new string[] { "unable to retrieve list" } };
                    }

                }
                catch (Exception ex){
                    Debug.WriteLine(ex.ToString());
                    return new BreedsList { Status = "error", Breeds = new string[] { "an error occurred" } };
                }

            }
        }


        private static async Task<string> GetBreedURLFromAPIAsync(string breedName)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = DogApiSettings.BaseAddress(); 
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage Res = await client.GetAsync(DogApiSettings.BreedRandomImageURL(breedName));

                    BreedImageUrl breeds = new BreedImageUrl();
                    if (Res.IsSuccessStatusCode)
                    {
                        var ApiResponse = await Res.Content.ReadAsStringAsync();
                        breeds = JsonConvert.DeserializeObject<BreedImageUrl>(ApiResponse);
                        return breeds.ImageUrl;
                    } else {
                        return DogApiSettings.MissingImageURL();
                    }
                } 
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return DogApiSettings.MissingImageURL();
                }

            }

        }


        public async Task<ActionResult> Index()
        {
            BreedsList br = new BreedsList();
            br = await GetBreedsFromApiAsync();
            return View(br);
        }


        // GET: Dogs/Details/poodle
        public async Task<ActionResult> Details(string Id)
        {
            ViewBag.BreedName = Id;
            ViewBag.Url = await GetBreedURLFromAPIAsync(Id);
            return View();
        }


        // GET: Dogs/Missing
        public ActionResult Missing()
        {
            ViewBag.Url = "http://cdn.smosh.com/sites/default/files/ftpuploads/bloguploads/1113/lost-pet-doge.jpg";
            return View();
        }




        //////////////////////////// TBD //////////////////////////


        // GET: Dogs/Create
        public ActionResult Create()
        {
            return RedirectToAction("Missing");
        }


        // POST: Dogs/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Missing");
            }
            catch
            {
                return View();
            }
        }


        // GET: Dogs/Edit/5
        public ActionResult Edit(int id)
        {
            return RedirectToAction("Missing");
        }


        // POST: Dogs/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Missing");
            }
            catch
            {
                return View();
            }
        }


        // GET: Dogs/Delete/5
        public ActionResult Delete()
        {
            return RedirectToAction("Missing");
        }


        // POST: Dogs/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Missing");
            }
            catch
            {
                return View();
            }
        }
    }
}
