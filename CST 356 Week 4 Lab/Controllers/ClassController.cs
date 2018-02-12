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
            var classes = GetClasses(userID);
            return View();
        }

        public ActionResult Create(int userID)
        {
            return View(userID);
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

        #region Helper
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
        
        private void Save(ClassViewModel classViewModel)
        {
            var dbContext = new DatabaseContext();

            var pet = MapToClass(classViewModel);

            dbContext.Classes.Add(pet);

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