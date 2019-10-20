using CRUD.Data;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        //GET: /Home
        public ActionResult Index()
        {
            StudentContext studentContext = new StudentContext();

            return View(studentContext.GetAllStudents());
        }

        //GET: /Home/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: /Home/Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StudentContext studentContext = new StudentContext();
                    if (studentContext.CreateStudent(student))
                    {
                        ViewBag.Message = "Student Details Added Successfully";
                        ModelState.Clear();
                    }
                }
            }
            catch
            {
                return View();
            }

            return View();
        }

        //GET: /Home/Delete/id
        
        public ActionResult Delete(int ID)
        {

            StudentContext studentContext = new StudentContext();

            try
            {
                if (studentContext.DeleteStudent(ID))
                {
                    ViewBag.DeleteMessage = "Student Successfully Deleted";
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }


        }

        //GET: Home/Update/id
        public ActionResult Update(int ID)
        {
            StudentContext studentContext = new StudentContext();
            Student student = studentContext.GetStudent(ID);

            return View(student);
        }

        [HttpPost]
        public ActionResult Update(int ID, Student student)
        {
            StudentContext studentContext = new StudentContext();
            bool success = false;

            if (studentContext.UpdateStudent(student))
            {
                success = true;
            }

            if (success)
            {
                return RedirectToAction("index");
            }

            return View();
            
        }
    }
}
