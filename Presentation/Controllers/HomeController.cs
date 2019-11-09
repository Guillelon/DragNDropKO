using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Field2BaseTest.Controllers
{
    public class HomeController : Controller
    {
        private TaskRepository _repo;

        public HomeController()
        {
            _repo = new TaskRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public string GetTasks()
        {
            var tasks = _repo.GetAll();
            return new JavaScriptSerializer().Serialize(tasks);
        }

        public ActionResult AddTask()
        {
            var viewModel = new TaskEntry();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTask(TaskEntry model)
        {
            if (model.Name != null && model.DueDate >= DateTime.Today)
            {
                _repo.Add(model);
                TempData["SuccessMessage"] = "Task Created!";
                return RedirectToAction("ViewTasks");
            }
            else
            {
                ViewBag.ErrorMessage = "All fields mandatory and Due Date can't be in the past";
                return View(model);
            }
        }

        public string AddNewTask(string name)
        {
            var newTask = new TaskEntry
            {
                Name = name,
                DueDate = DateTime.Now.AddDays(1)
            };
            _repo.Add(newTask);
            return "200";
        }

        public string ChangeTaskOrder(int id, int newOrder)
        {
            _repo.ChangeTaskOrder(id, newOrder + 1);
            return "";
        }

        public ActionResult EditTask(int id)
        {
            var viewModel = _repo.Get(id);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTask(TaskEntry model)
        {
            if (model.Name != null && model.DueDate >= DateTime.Today)
            {
                _repo.Edit(model);
                TempData["SuccessMessage"] = "Task Edited!";
                return RedirectToAction("ViewTasks");
            }
            else
            {
                ViewBag.ErrorMessage = "All fields mandatory and Due Date can't be in the past";
                return View(model);
            }
        }

        public ActionResult DeleteTask(int id)
        {
            var result = _repo.Delete(id);
            if(result)
                TempData["SuccessMessage"] = "Task Deleted!";
            return RedirectToAction("ViewTasks");
        }

        public ActionResult ViewTasks()
        {
            var viewModel = _repo.GetAll();
            if (TempData["SuccessMessage"] != null)
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(viewModel);
        }

        public string ChangeTaskStatus(int id)
        {
            _repo.ChangeStatus(id);
            return "200";
        }
    }
}