using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Shared.Constants.Common;
using Shared.Constants.Project;
using Shared.Models;

namespace WebApplication.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ProjectValidationMessages.EnterProjectName)]
        [Display(Name = ProjectProperties.ProjectNameDisplay, Prompt = ProjectProperties.ProjectNameDisplay)]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = ProjectValidationMessages.EnterCustomerCompanyName)]
        [Display(Name = ProjectProperties.CustomerCompanyNameDisplay, Prompt = ProjectProperties.CustomerCompanyNameDisplay)]
        public string CustomerCompanyName { get; set; }

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

        [Display(Name = ProjectProperties.CommentDisplay, Prompt = ProjectProperties.CommentDisplay)]
        public string Comment { get; set; }

        public int ManagerId { get; set; }

        [Display(Name = ProjectProperties.ManagerIdDisplay, Prompt = ProjectProperties.ManagerIdPrompt)]
        public string ManagerFullName { get; set; }

        public ICollection<int> AssignedEmployeesIds { get; set; }

        [Display(Name = ViewStringConstants.ProjectsEmployeesTitle)]
        public ICollection<EmployeeViewModel> AssignedEmployees { get; set; }
    }
}