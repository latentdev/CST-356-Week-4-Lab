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
    public class ClassController : Controller
    {
        // GET: Classes
        public ActionResult Index(int userID)
        {
            ViewBag.userID = userID;
            var classes = GetClasses(userID);
            return View(classes);
        }

        public ActionResult Create(int userID)
        {
            ViewBag.userID = userID;
            return View();
        }

        [HttpPost]
        public ActionResult Create(ClassViewModel classViewModel)
        {
            if (ModelState.IsValid)
            {
                Save(classViewModel);
                return RedirectToAction("Index", new { UserId = classViewModel.UserID });
            }
            else
                return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var @class = GetClass(id);

            return View(@class);
        }

        [HttpPost]
        public ActionResult Edit(ClassViewModel classViewModel)
        {
            if (ModelState.IsValid)
            {
                UpdateClass(classViewModel);

                return RedirectToAction("Index", new { userID = classViewModel.UserID });
            }

            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var @class = GetClass(id);
            return View(@class);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            DeleteClass(id);

            return RedirectToAction("Index", new { userID = id });
        }

        #region Helper

        private ClassViewModel GetClass(int id)
        {
            var dbContext = new DatabaseContext();

            var @class = dbContext.Classes.Find(id);

            return MapToClassViewModel(@class);
        }

        private void DeleteClass(int id)
        {
            var dbContext = new DatabaseContext();

            var @class = dbContext.Classes.Find(id);

            if (@class != null)
            {
                dbContext.Classes.Remove(@class);
                dbContext.SaveChanges();
            }
        }

        private void UpdateClass(ClassViewModel classViewModel)
        {
            var dbContext = new DatabaseContext();

            var @class = dbContext.Classes.Find(classViewModel.ID);

            CopyToClass(classViewModel, @class);

            dbContext.SaveChanges();
        }

        private IEnumerable<ClassViewModel> GetClasses(int userID)
        {
            var classViewModels = new List<ClassViewModel>();

            var dbContext = new DatabaseContext();

            var classes = dbContext.Classes.Where(x => x.UserID == userID).ToList(); 

            foreach (var @class in classes)
            {
                var classViewModel = MapToClassViewModel(@class);
                classViewModels.Add(classViewModel);
            }

            return classViewModels;
        }

        private void CopyToClass(ClassViewModel classViewModel, Class @class)
        {
            @class.ID = classViewModel.ID;
            @class.CRN = classViewModel.CRN;
            @class.ClassName = classViewModel.ClassName;
            @class.StartTime = classViewModel.StartTime;
            @class.EndTime = classViewModel.EndTime;
            @class.Instructor = classViewModel.Instructor;
            @class.UserID = classViewModel.UserID;
        }

        private void Save(ClassViewModel classViewModel)
        {
            var dbContext = new DatabaseContext();

            var @class = MapToClass(classViewModel);

            dbContext.Classes.Add(@class);

            dbContext.SaveChanges();
        }

        private ClassViewModel MapToClassViewModel(Class @class)
        {
            return new ClassViewModel
            {
                ID = @class.ID,
                CRN = @class.CRN,
                ClassName = @class.ClassName,
                StartTime = @class.StartTime,
                EndTime = @class.EndTime,
                Instructor = @class.Instructor,
                UserID = @class.UserID
            };
        }

        private Class MapToClass(ClassViewModel classViewModel)
        {
            return new Class
            {
                ID = classViewModel.ID,
                CRN = classViewModel.CRN,
                ClassName = classViewModel.ClassName,
                StartTime = classViewModel.StartTime,
                EndTime = classViewModel.EndTime,
                Instructor = classViewModel.Instructor,
                UserID = classViewModel.UserID
            };
        }
        #endregion
    }
}