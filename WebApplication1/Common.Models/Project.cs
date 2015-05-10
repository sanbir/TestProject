using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Data.Models
{
    public class Project : EntityBase
    {
        public Project()
        {
            ProjectsEmployees = new HashSet<ProjectsEmployee>();
        }

        [Required(ErrorMessage = "������� �������� �������")]
        [Display(Name = "�������� �������", Prompt = "�������� �������")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "������� �������� ��������-���������")]
        [Display(Name = "�������� ��������-���������", Prompt = "�������� ��������-���������")]
        public string CustomerCompanyName { get; set; }

        [Browsable(false)]
        public int ManagerId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "������� ���� ������ �������")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "���� ������ �������", Prompt = "���� ������ �������")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "������� ���� ��������� �������")]
        [Display(Name = "���� ��������� �������", Prompt = "���� ��������� �������")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "������� ��������� �������")]
        [Range(0, int.MaxValue, ErrorMessage = "����������� ������� ������ ���� ������������� �������� >= 0")]
        [Display(Name = "��������� �������", Prompt = "��������� �������")]
        public int Priority { get; set; }

        [Display(Name = "��������� �����������", Prompt = "��������� �����������")]
        public string Comment { get; set; }

        [Browsable(false)]
        public virtual Employee Employee { get; set; }

        [Browsable(false)]
        public virtual ICollection<ProjectsEmployee> ProjectsEmployees { get; set; }
    }
}
