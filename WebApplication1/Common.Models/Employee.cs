using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using Utils;

namespace Data.Models
{
    public class Employee : ObjectBase
    {
        public Employee()
        {
            ProjectsEmployees = new HashSet<ProjectsEmployee>();
            Projects = new HashSet<Project>();
        }

        [Required(ErrorMessage = Constants.EmployeeFirstNameRequiredErrorMessage)]
        [StringLength(50)]
        [Display(Name = "Имя", Prompt = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [StringLength(50)]
        [Display(Name = "Фамилия", Prompt = "Фамилия")]
        public string LastName { get; set; }

        [StringLength(50)]
        [Display(Name = "Отчество", Prompt = "Отчество")]
        public string MiddleName { get; set; }

        [StringLength(100, ErrorMessage = "Длина e-mail не может превышать 100 символов")]
        [Display(Name = "E-mail", Prompt = "name@example.com")]
        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Неверный адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите название компании-исполнителя")]
        [Display(Name = "Название компании-исполнителя", Prompt = "Название компании-исполнителя")]
        public string ContractorCompanyName { get; set; }

        [Browsable(false)]
        public virtual ICollection<ProjectsEmployee> ProjectsEmployees { get; set; }

        [Browsable(false)]
        public virtual ICollection<Project> Projects { get; set; }

    }
}
