using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int EmployerId { get; set; }
        public List<SelectListItem> Employers { get; set; }


        public AddJobViewModel()
        {
        }

        public AddJobViewModel(List<Employer> employers)
        {
            Employers = new List<SelectListItem>();

        }
    }
}
