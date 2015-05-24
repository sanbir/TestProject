using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Shared.Constants.Project;

namespace WebApplication.ViewModels
{
    public class ProjectToPersistViewModel : ProjectPartialViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ProjectValidationMessages.EnterManagerId)]
        [Range(1, int.MaxValue, ErrorMessage = ProjectValidationMessages.EnterManagerId)]
        [Display(Name = ProjectProperties.ManagerIdDisplay, Prompt = ProjectProperties.ManagerIdPrompt)]
        public int ManagerId { get; set; }

        public ICollection<int> AssignedEmployeesIds { get; set; }
    }
}