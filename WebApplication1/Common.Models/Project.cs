using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Constants.Project;

namespace Data.Models
{
    public class Project : EntityBase
    {
        public Project()
        {
            ProjectsEmployees = new HashSet<ProjectsEmployee>();
        }

        [Required(ErrorMessage = ProjectValidationMessages.EnterProjectName)]
        [Display(Name = ProjectProperties.ProjectName, Prompt = ProjectProperties.ProjectName)]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = ProjectValidationMessages.EnterCustomerCompanyName)]
        [Display(Name = ProjectProperties.CustomerCompanyName, Prompt = ProjectProperties.CustomerCompanyName)]
        public string CustomerCompanyName { get; set; }

        [Browsable(false)]
        public int ManagerId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = ProjectValidationMessages.EnterStartDate)]
        [DisplayFormat(DataFormatString = ProjectValidationMessages.DateFormatString, ApplyFormatInEditMode = true)]
        [Display(Name = ProjectProperties.StartDate, Prompt = ProjectProperties.StartDate)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = ProjectValidationMessages.EnterEndDate)]
        [DisplayFormat(DataFormatString = ProjectValidationMessages.DateFormatString, ApplyFormatInEditMode = true)]
        [Display(Name = ProjectProperties.EndDate, Prompt = ProjectProperties.EndDate)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = ProjectValidationMessages.EnterPriority)]
        [Range(0, int.MaxValue, ErrorMessage = ProjectValidationMessages.PriorityRange)]
        [Display(Name = ProjectProperties.Priority, Prompt = ProjectProperties.Priority)]
        public int Priority { get; set; }

        [Display(Name = ProjectProperties.Comment, Prompt = ProjectProperties.Comment)]
        public string Comment { get; set; }

        [Browsable(false)]
        public virtual Employee Employee { get; set; }

        [Browsable(false)]
        public virtual ICollection<ProjectsEmployee> ProjectsEmployees { get; set; }
    }
}
