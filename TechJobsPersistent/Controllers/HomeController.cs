using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            List<Employer> employers = context.Employers.ToList();
            List<Skill> skills = context.Skills.ToList();
            AddJobViewModel addJobViewModel = new AddJobViewModel(employers, skills);
            return View(addJobViewModel);
        }

        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            //DONT THINK I NEED THIS
            //foreach (Employer employer in context.Employers)
            //{
            //    addJobViewModel.Employers = new List<SelectListItem>();
            //    addJobViewModel.Employers.Add(
            //        new SelectListItem
            //        {
            //            Value = employer.Id.ToString(),
            //            Text = employer.Name
            //        });
            //}

            if (ModelState.IsValid)
            {
                Employer theEmployer = context.Employers.Find(addJobViewModel.EmployerId);
                Job job = new Job
                {
                    Name = addJobViewModel.Name,
                    EmployerId = addJobViewModel.EmployerId,
                    Employer = theEmployer
                };

                for (int i = 0; i < selectedSkills.Length; i++)
                {
                    JobSkill jobSkill = new JobSkill();
                    jobSkill.Job = job;
                    jobSkill.SkillId = int.Parse(selectedSkills[i]);

                    context.JobSkills.Add(jobSkill);
                }

                context.Jobs.Add(job);
                context.SaveChanges();

                return Redirect("/List");
            }

            return View("AddJob", addJobViewModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
