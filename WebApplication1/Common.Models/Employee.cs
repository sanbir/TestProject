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
        [Display(Name = "���", Prompt = "���")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "������� �������")]
        [StringLength(50)]
        [Display(Name = "�������", Prompt = "�������")]
        public string LastName { get; set; }

        [StringLength(50)]
        [Display(Name = "��������", Prompt = "��������")]
        public string MiddleName { get; set; }

        [StringLength(100, ErrorMessage = "����� e-mail �� ����� ��������� 100 ��������")]
        [Display(Name = "E-mail", Prompt = "name@example.com")]
        [Required(ErrorMessage = "������� ����� ����������� �����")]
        [EmailAddress(ErrorMessage = "�������� ����� ����������� �����")]
        public string Email { get; set; }

        [Required(ErrorMessage = "������� �������� ��������-�����������")]
        [Display(Name = "�������� ��������-�����������", Prompt = "�������� ��������-�����������")]
        public string ContractorCompanyName { get; set; }

        [Browsable(false)]
        public virtual ICollection<ProjectsEmployee> ProjectsEmployees { get; set; }

        [Browsable(false)]
        public virtual ICollection<Project> Projects { get; set; }

    }
}
