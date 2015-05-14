using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Shared.Constants.Project;

namespace Shared.Models
{
    public class Project : EntityBase
    {
        public Project()
        {
            ProjectsEmployees = new HashSet<ProjectsEmployee>();
        }

        [Required(ErrorMessage = ProjectValidationMessages.EnterProjectName)]
        [Display(Name = ProjectProperties.ProjectNameDisplay, Prompt = ProjectProperties.ProjectNameDisplay)]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = ProjectValidationMessages.EnterCustomerCompanyName)]
        [Display(Name = ProjectProperties.CustomerCompanyNameDisplay, Prompt = ProjectProperties.CustomerCompanyNameDisplay)]
        public string CustomerCompanyName { get; set; }

        [Required(ErrorMessage = ProjectValidationMessages.EnterManagerId)]
        [Display(Name = ProjectProperties.ManagerIdDisplay, Prompt = ProjectProperties.ManagerIdDisplay)]
        public int ManagerId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = ProjectValidationMessages.EnterStartDate)]
        [DisplayFormat(DataFormatString = ProjectValidationMessages.DateFormatString, ApplyFormatInEditMode = true)]
        [Display(Name = ProjectProperties.StartDateDisplay, Prompt = ProjectProperties.StartDateDisplay)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = ProjectValidationMessages.EnterEndDate)]
        [DisplayFormat(DataFormatString = ProjectValidationMessages.DateFormatString, ApplyFormatInEditMode = true)]
        [Display(Name = ProjectProperties.EndDateDisplay, Prompt = ProjectProperties.EndDateDisplay)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = ProjectValidationMessages.EnterPriority)]
        [Range(0, int.MaxValue, ErrorMessage = ProjectValidationMessages.PriorityRange)]
        [Display(Name = ProjectProperties.PriorityDisplay, Prompt = ProjectProperties.PriorityDisplay)]
        public int Priority { get; set; }

        [Display(Name = ProjectProperties.Comment, Prompt = ProjectProperties.Comment)]
        public string Comment { get; set; }

        public Employee Manager { get; set; }

        [Browsable(false)]
        public virtual ICollection<ProjectsEmployee> ProjectsEmployees { get; set; }
    }
}
