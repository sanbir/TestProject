using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Shared.Constants.Project;

namespace WebApplication.ViewModels
{
    public class ProjectPartialViewModel
    {
        [Required(ErrorMessage = ProjectValidationMessages.EnterProjectName)]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = ProjectValidationMessages.EnterCustomerCompanyName)]
        public string CustomerCompanyName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = ProjectValidationMessages.EnterStartDate)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = ProjectValidationMessages.EnterEndDate)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = ProjectValidationMessages.EnterPriority)]
        [Range(0, int.MaxValue, ErrorMessage = ProjectValidationMessages.PriorityRange)]
        public int Priority { get; set; }

        public string Comment { get; set; }
    }
}