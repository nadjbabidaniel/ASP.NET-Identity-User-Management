using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Users.Infrastructure;
using Users.Models;

namespace Users.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();
        private string filePath = @"C:\Users\Operations Intern 5\Downloads\File.csv";
        public class CountryClass
        {
            public string Name { get; set; }
        }

        public ActionResult Index()
        {
            List<string> countries = Enum.GetNames(typeof(Country)).ToList();
            List<CountryClass> countryClassList = new List<CountryClass>();

            foreach (var cc in countries)
            {
                countryClassList.Add(new CountryClass() { Name = cc });
            }
            ViewBag.SelectedCountry = new SelectList(countryClassList, "Name", "Name", "");

            TempData["ListOfFilteredUsers"] = UserManager.Users.ToList();

            return View(UserManager.Users);
        }

        public ActionResult IndexCountryFilter(string SelectedCountry, string searchString)
        {           
            List<string> countries = Enum.GetNames(typeof(Country)).ToList();
            List<CountryClass> countryClassList = new List<CountryClass>();

            foreach (var cc in countries)
            {                              
                countryClassList.Add(new CountryClass() { Name = cc });
            }

            ViewBag.SelectedCountry = new SelectList(countryClassList, "Name", "Name", SelectedCountry);

            var allUsers = UserManager.Users.ToList();

            if(!string.IsNullOrEmpty(SelectedCountry))
            {
                allUsers = allUsers.Where(x => x.Country.ToString().Equals(SelectedCountry)).ToList();
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                allUsers = allUsers.Where(s => (s.FirstName != null && s.FirstName.ToLower().Contains(searchString.ToLower()))
                                       || (s.LastName != null && s.LastName.ToLower().Contains(searchString.ToLower()))).ToList();
            }

            TempData["ListOfFilteredUsers"] = allUsers;

            return View( "Index", allUsers);
        }

        public ActionResult SaveData()
        {
            List<ApplicationUser> userFiltered = TempData["ListOfFilteredUsers"] as List<ApplicationUser>;

            if (userFiltered != null)
            {
                try
                {
                    var lines = new List<string>();
                    string data = "UserName, FirstName, LastName, Email";
                    lines.Add(data);

                    foreach (var item in userFiltered)
                    {
                        string oneRow = item?.UserName?.Replace(',', ' ') + "," + item?.FirstName?.Replace(',', ' ') + "," + item?.LastName?.Replace(',', ' ') + "," + item?.Email?.Replace(',', ' ');
                        lines.Add(oneRow);
                    }
                    
                    System.IO.File.WriteAllLines(filePath, lines);
                }
                catch (Exception ex)
                {

                }
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> UploadData()
        {            
            StreamReader sr = new StreamReader(filePath);
            int count = 0;

            while (!sr.EndOfStream)
            {
                if (count == 0) 
                {
                    count++; 
                    continue; 
                }

                string line = sr.ReadLine();
                string[] value = line.Split(',');

                ApplicationUser user = new ApplicationUser
                {
                    UserName = value[0],
                    FirstName = value[1],
                    LastName = value[2],
                    Email = value[3]
                };                

                var result = await UserManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    AddErrors(result);
                }

            }                      

            return RedirectToAction("Index");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
      
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {UserName = model.Name, Email = model.Email};
                //PasswordHash
                IdentityResult result = await UserManager.CreateAsync(user,
                    model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                AddErrorsFromResult(result);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                if (user.UserName=="Admin")
                {
                    return View("Error", new[] { "Do not delete administrator！" });
                }
                                
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View("Error", result.Errors);
            }
            return View("Error", new[] {"User Not Found"});
        }

        public async Task<ActionResult> Edit(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, string email, string password)
        {
            //ApplicationUser
            ApplicationUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                if (user.UserName=="Admin")
                {
                    return View("Error", new[] { "Do not delete administrator！" });
                }
                
                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                   //验证密码是否满足要求
                    validPass = await UserManager.PasswordValidator.ValidateAsync(password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                //验证Email是否满足要求
                user.Email = email;
                IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                
                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded &&  validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Cannot find the user");
            }
            return View(user);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }
    }
}
