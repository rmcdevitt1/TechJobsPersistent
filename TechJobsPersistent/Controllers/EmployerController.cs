using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
        private JobDbContext context;

        public EmployerController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        //Complete Index() so that it passes all of the Employer objects in the database to the view.
        public IActionResult Index()
        {
            List<Employer> employers = context.Employers.ToList();
            return View(employers);
        }

        //Create an instance of AddEmployerViewModel inside of the Add() method
        //and pass the instance into the View() return method.
        public IActionResult Add()
        {
            AddEmployerViewModel addEmployerViewModel = new AddEmployerViewModel();
            return View(addEmployerViewModel);
        }

        //Add the appropriate code to ProcessAddEmployerForm() so that it will process form submissions
        //and make sure that only valid Employer objects are being saved to the database.
        [HttpPost]
        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel addEmployerViewModel)
        {
            if (ModelState.IsValid)
            {
                Employer newEmployer = new Employer
                {
                    Name = addEmployerViewModel.Name,
                    Location = addEmployerViewModel.Location
                };
                context.Employers.Add(newEmployer);
                context.SaveChanges();

                //or should it redirect to index?
                return Redirect("/Employer");

            }
            return View("Add", addEmployerViewModel);
        }

        //Make sure that the method is actually passing an Employer object to the view for display.
        public IActionResult About(int id)
        {
            Employer newEmployer = context.Employers.Find(id);
            return View(newEmployer);
        }
    }
}
