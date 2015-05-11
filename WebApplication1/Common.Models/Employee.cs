using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Constants.Employee;

namespace Data.Models
{
    public class Employee : EntityBase
    {
        public Employee()
        {
            ProjectsEmployees = new HashSet<ProjectsEmployee>();
            Projects = new HashSet<Project>();
        }

        [Required(ErrorMessage = EmployeeValidationMessages.EnterFirstName)]
        [StringLength(50)]
        [Display(Name = EmployeeProperties.FirstNameDisplay, Prompt = EmployeeProperties.FirstNameDisplay)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = EmployeeValidationMessages.EnterLastName)]
        [StringLength(50)]
        [Display(Name = EmployeeProperties.LastNameDisplay, Prompt = EmployeeProperties.LastNameDisplay)]
        public string LastName { get; set; }

        [StringLength(50)]
        [Display(Name = EmployeeProperties.MiddleNameDisplay, Prompt = EmployeeProperties.MiddleNameDisplay)]
        public string MiddleName { get; set; }

        [StringLength(100, ErrorMessage = EmployeeValidationMessages.ExceedEmailLength)]
        [Display(Name = EmployeeProperties.EmailDisplay, Prompt = EmployeeValidationMessages.EmailPrompt)]
        [Required(ErrorMessage = EmployeeValidationMessages.EnterEmail)]
        [EmailAddress(ErrorMessage = EmployeeValidationMessages.EmailIncorrect)]
        public string Email { get; set; }

        [Required(ErrorMessage = EmployeeValidationMessages.EnterContractorCompanyName)]
        [Display(Name = EmployeeProperties.ContractorCompanyNameDisplay, Prompt = EmployeeProperties.ContractorCompanyNameDisplay)]
        public string ContractorCompanyName { get; set; }

        [Browsable(false)]
        public virtual ICollection<ProjectsEmployee> ProjectsEmployees { get; set; }

        [Browsable(false)]
        public virtual ICollection<Project> Projects { get; set; }

    }
}
