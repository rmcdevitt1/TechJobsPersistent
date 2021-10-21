using System;
using System.ComponentModel.DataAnnotations;

namespace TechJobsPersistent.ViewModels
{
    public class AddEmployerViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        //NOT SURE IF NEEDED:
        public AddEmployerViewModel(string name, string location)
        {
            Name = name;
            Location = location;
        }

        public AddEmployerViewModel()
        {
        }
    }
}
