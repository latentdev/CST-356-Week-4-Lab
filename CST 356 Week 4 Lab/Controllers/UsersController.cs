using CST_356_Week_4_Lab.Data;
using CST_356_Week_4_Lab.Data.Entities;
using CST_356_Week_4_Lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CST_356_Week_4_Lab.Controllers
{
    public class UsersController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View(GetAllUsers());
        }

        private IEnumerable<UserViewModel> GetAllUsers()
        {
            var userViewModels = new List<UserViewModel>();

            var dbContext = new DatabaseContext();

            foreach (var user in dbContext.Users)
            {
                var userViewModel = MapToUserViewModel(user);
                userViewModels.Add(userViewModel);
            }

            return userViewModels;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = MapToUser(userViewModel);
                SaveUser(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        private void SaveUser(User user)
        {
            var dbContext = new DatabaseContext();
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        private User MapToUser(UserViewModel userViewModel)
        {
            return new User
            {
                ID = userViewModel.ID,
                FirstName = userViewModel.FirstName,
                MiddleName = userViewModel.MiddleName,
                LastName = userViewModel.LastName,
                Email = userViewModel.Email,
                YearsInSchool = userViewModel.YearsInSchool
            };
        }
        private UserViewModel MapToUserViewModel(User user)
        {
            return new UserViewModel
            {
                ID = user.ID,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                YearsInSchool = user.YearsInSchool
            };
        }
    }
}