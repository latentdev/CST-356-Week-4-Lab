using CST_356_Week_4_Lab.Data;
using CST_356_Week_4_Lab.Data.Entities;
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
            //if (Database.Users == null)
            //{
            //    Database.Users = new List<User>();
            //}
            return View(GetAllUsers());
        }

        private IEnumerable<User> GetAllUsers()
        {
            var userViewModels = new List<User>();

            var dbContext = new DatabaseContext();

            foreach (var user in dbContext.Users)
            {
                userViewModels.Add(user);
            }

            return userViewModels;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            SaveUser(user);
            //if (!user.IsValid())
            //    return View(user);
            //if (Database.Users == null)
            //{
            //    Database.Users = new List<User>();
            //}
            //Database.Users.Add(user);
            return RedirectToAction("Index");
        }

        private void SaveUser(User user)
        {
            var dbContext = new DatabaseContext();
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }
    }
}