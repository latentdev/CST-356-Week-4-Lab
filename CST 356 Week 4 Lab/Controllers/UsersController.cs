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


        #region Actions
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = GetUser(id);

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                UpdateUser(userViewModel);

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var user = GetUser(id);
            return View(user);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            DeleteUser(id);

            return RedirectToAction("Index");
        }
        #endregion

        #region Helper
        private void DeleteUser(int id)
        {
            var dbContext = new DatabaseContext();

            var user = dbContext.Users.Find(id);

            if (user != null)
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }
        }

        private UserViewModel GetUser(int id)
        {
            var dbContext = new DatabaseContext();

            var user = dbContext.Users.Find(id);

            return MapToUserViewModel(user);
        }

        private void SaveUser(User user)
        {
            var dbContext = new DatabaseContext();
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        private void UpdateUser(UserViewModel userViewModel)
        {
            var dbContext = new DatabaseContext();

            var user = dbContext.Users.Find(userViewModel.ID);

            CopyToUser(userViewModel, user);

            dbContext.SaveChanges();
        }

        private void CopyToUser(UserViewModel userViewModel, User user)
        {
            user.FirstName = userViewModel.FirstName;
            user.MiddleName = userViewModel.MiddleName;
            user.LastName = userViewModel.LastName;
            user.Email = userViewModel.Email;
            user.YearsInSchool = userViewModel.YearsInSchool;
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
        #endregion
    }
}